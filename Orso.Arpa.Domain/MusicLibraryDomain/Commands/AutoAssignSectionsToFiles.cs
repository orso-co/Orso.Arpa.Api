using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class AutoAssignSectionsToFiles
    {
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// Optional: Only process files of this MusicPiece. If null, process all files.
            /// </summary>
            public Guid? MusicPieceId { get; set; }

            /// <summary>
            /// If true, only analyze without making changes (dry run)
            /// </summary>
            public bool DryRun { get; set; } = false;
        }

        public class Result
        {
            public int FilesProcessed { get; set; }
            public int FilesMatched { get; set; }
            public int SectionsAssigned { get; set; }
            public List<FileAssignment> Assignments { get; set; } = new();
        }

        public class FileAssignment
        {
            public Guid FileId { get; set; }
            public string FileName { get; set; }
            public List<string> AssignedSections { get; set; } = new();
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicPieceId)
                    .EntityExists<Command, MusicPiece>(arpaContext)
                    .When(c => c.MusicPieceId.HasValue);
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IArpaContext _arpaContext;

            // Mapping: lowercase filename pattern -> Section name (as stored in DB)
            private static readonly Dictionary<string, string> FilenameSectionMapping = new(StringComparer.OrdinalIgnoreCase)
            {
                // Saxophone variants (German)
                { "altsaxophon", "Saxophone" },
                { "sopransaxophon", "Saxophone" },
                { "tenorsaxophon", "Saxophone" },
                { "baritonsaxophon", "Saxophone" },
                { "saxophon", "Saxophone" },

                // Brass (German -> English)
                { "trompete", "Trumpet" },
                { "trumpet", "Trumpet" },
                { "posaune", "Trombone" },
                { "trombone", "Trombone" },
                { "horn in f", "Horn" },
                { "tenorhorn", "Euphonium" },
                { "baritonhorn", "Euphonium" },
                { "euphonium", "Euphonium" },
                { "tenortuba", "Tuba" },
                { "tuba in c", "Tuba" },
                { "tuba", "Tuba" },

                // Strings (German -> English)
                { "violine 1", "Violin" },
                { "violine 2", "Violin" },
                { "violine", "Violin" },
                { "violin", "Violin" },
                { "viola", "Viola" },
                { "bratsche", "Viola" },
                { "violoncello", "Violoncello" },
                { "cello", "Violoncello" },
                { "kontrabass", "Double Bass" },
                { "double bass", "Double Bass" },
                { "bass (string)", "Double Bass" },

                // Woodwinds (German -> English)
                { "piccolo", "Flute" },
                { "flöte", "Flute" },
                { "flute", "Flute" },
                { "querflöte", "Flute" },
                { "klarinette", "Clarinet" },
                { "clarinet", "Clarinet" },
                { "oboe", "Oboe" },
                { "fagott", "Bassoon" },
                { "bassoon", "Bassoon" },

                // Percussion (German -> English)
                { "kleine trommel", "Drum Set (Orchestra)" },
                { "große trommel", "Drum Set (Orchestra)" },
                { "marschtrommel", "Drum Set (Orchestra)" },
                { "schlagzeug", "Drum Set (Orchestra)" },
                { "percussion", "Drum Set (Orchestra)" },
                { "becken", "Drum Set (Orchestra)" },
                { "timpani", "Timpani" },
                { "pauken", "Timpani" },
                { "mallets", "Mallets" },
                { "glockenspiel", "Mallets" },
                { "xylophon", "Mallets" },
                { "vibraphon", "Mallets" },

                // Keyboards
                { "piano", "Keyboards" },
                { "klavier", "Keyboards" },
                { "keyboard", "Keyboards" },
                { "organ", "Keyboards" },
                { "orgel", "Keyboards" },
                { "harmonium", "Keyboards" },
                { "celesta", "Keyboards" },
                { "akkordeon", "Accordion" },
                { "accordion", "Accordion" },

                // Choir voices (German -> English) - handled separately to avoid conflicts
                { "sopran bibelformat", "Soprano" },
                { "soprano", "Soprano" },
                { "alt bibelformat", "Alto" },
                { "alto bibelformat", "Alto" },
                { "tenor bibelformat", "Tenor" },
                { "bass bibelformat", "Bass" },
                { "baß", "Bass" },
                { "bariton", "Baritone" },
                { "baritone", "Baritone" },

                // Others
                { "harfe", "Harp" },
                { "harp", "Harp" },
                { "gitarre", "Guitars" },
                { "guitar", "Guitars" },
                { "e-bass", "Electric Bass (Band)" },
                { "electric bass", "Electric Bass (Band)" },
                { "e-gitarre", "Electric Guitar (Band)" },
                { "electric guitar", "Electric Guitar (Band)" },
            };

            // Patterns that indicate Conductor assignment
            private static readonly string[] ConductorPatterns =
            {
                "partitur", "score", "full score", "klavierauszug", "piano reduction",
                "textblatt", "text", "transliteration", "übersetzung"
            };

            // Special patterns for SATB (assign to all choir voices)
            private static readonly string[] SatbPatterns = { "satb", "chor", "choir", "mixed chorus" };

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                // Load all sections
                var sections = await _arpaContext.Sections
                    .Where(s => !s.Deleted && s.IsInstrument)
                    .ToListAsync(cancellationToken);

                var sectionByName = sections.ToDictionary(s => s.Name, s => s, StringComparer.OrdinalIgnoreCase);

                // Get Conductor section (always exists based on analysis)
                var conductorSection = sections.FirstOrDefault(s => s.Name == "Conductor");

                // Load files to process
                var filesQuery = _arpaContext.Set<MusicPieceFile>()
                    .Include(f => f.Sections)
                    .Where(f => !f.Deleted);

                if (request.MusicPieceId.HasValue)
                {
                    filesQuery = filesQuery.Where(f => f.MusicPieceId == request.MusicPieceId.Value);
                }

                var files = await filesQuery.ToListAsync(cancellationToken);

                var result = new Result { FilesProcessed = files.Count };

                foreach (var file in files)
                {
                    var assignment = new FileAssignment
                    {
                        FileId = file.Id,
                        FileName = file.FileName
                    };

                    var fileNameLower = file.FileName.ToLowerInvariant();
                    var matchedSections = new HashSet<Section>();

                    // Check for SATB patterns first (assign to all choir voices)
                    if (SatbPatterns.Any(p => fileNameLower.Contains(p)))
                    {
                        var choirSections = new[] { "Soprano", "Alto", "Tenor", "Bass" };
                        foreach (var choirSection in choirSections)
                        {
                            if (sectionByName.TryGetValue(choirSection, out var section))
                            {
                                matchedSections.Add(section);
                            }
                        }
                    }

                    // Check for conductor patterns
                    if (ConductorPatterns.Any(p => fileNameLower.Contains(p)))
                    {
                        if (conductorSection != null)
                        {
                            matchedSections.Add(conductorSection);
                        }
                    }

                    // Check for instrument patterns
                    foreach (var mapping in FilenameSectionMapping)
                    {
                        if (fileNameLower.Contains(mapping.Key))
                        {
                            if (sectionByName.TryGetValue(mapping.Value, out var section))
                            {
                                matchedSections.Add(section);
                            }
                        }
                    }

                    // Handle "Horn" - only match when NOT part of "Tenorhorn" or "Baritonhorn"
                    if (fileNameLower.Contains("horn") &&
                        !fileNameLower.Contains("tenorhorn") &&
                        !fileNameLower.Contains("baritonhorn"))
                    {
                        if (sectionByName.TryGetValue("Horn", out var hornSection))
                        {
                            matchedSections.Add(hornSection);
                        }
                    }

                    // Handle voice parts - Sopran, Alt, Tenor, Bass (standalone, not in instrument names)
                    // Sopran - check for standalone "sopran" (not "sopransaxophon")
                    if ((fileNameLower.Contains(" sopran") || fileNameLower.Contains("-sopran") ||
                         fileNameLower.Contains("_sopran") || fileNameLower.StartsWith("sopran")) &&
                        !fileNameLower.Contains("saxophon"))
                    {
                        if (sectionByName.TryGetValue("Soprano", out var sopranoSection))
                        {
                            matchedSections.Add(sopranoSection);
                        }
                    }

                    // Alt - check for standalone "alt" (not "altsaxophon")
                    if ((fileNameLower.Contains(" alt") || fileNameLower.Contains("-alt") ||
                         fileNameLower.Contains("_alt") || fileNameLower.EndsWith(" alt.pdf")) &&
                        !fileNameLower.Contains("saxophon"))
                    {
                        if (sectionByName.TryGetValue("Alto", out var altoSection))
                        {
                            matchedSections.Add(altoSection);
                        }
                    }

                    // Tenor - check for standalone "tenor" (not "tenorhorn", "tenortuba", "tenorsaxophon")
                    if ((fileNameLower.Contains(" tenor") || fileNameLower.Contains("-tenor") ||
                         fileNameLower.Contains("_tenor") || fileNameLower.EndsWith(" tenor.pdf")) &&
                        !fileNameLower.Contains("tenorhorn") &&
                        !fileNameLower.Contains("tenortuba") &&
                        !fileNameLower.Contains("tenorsaxophon"))
                    {
                        if (sectionByName.TryGetValue("Tenor", out var tenorSection))
                        {
                            matchedSections.Add(tenorSection);
                        }
                    }

                    // Bass - check for standalone "bass" (not instrument variants)
                    if (fileNameLower.Contains("bass") &&
                        !fileNameLower.Contains("kontrabass") &&
                        !fileNameLower.Contains("double bass") &&
                        !fileNameLower.Contains("e-bass") &&
                        !fileNameLower.Contains("electric bass"))
                    {
                        // Only match if it looks like a voice part
                        if (fileNameLower.Contains("bibelformat") || fileNameLower.Contains("chor") ||
                            fileNameLower.Contains("satb") || fileNameLower.Contains("voice") ||
                            fileNameLower.Contains("stimme") ||
                            fileNameLower.Contains(" bass") || fileNameLower.Contains("-bass") ||
                            fileNameLower.Contains("_bass") || fileNameLower.EndsWith(" bass.pdf"))
                        {
                            if (sectionByName.TryGetValue("Bass", out var bassSection))
                            {
                                matchedSections.Add(bassSection);
                            }
                        }
                    }

                    // If no matches found, assign to Conductor (unclear files)
                    if (matchedSections.Count == 0 && conductorSection != null)
                    {
                        matchedSections.Add(conductorSection);
                    }

                    // Record assignments
                    assignment.AssignedSections = matchedSections.Select(s => s.Name).ToList();

                    if (matchedSections.Count > 0)
                    {
                        result.FilesMatched++;

                        if (!request.DryRun)
                        {
                            // Add sections that aren't already assigned
                            foreach (var section in matchedSections)
                            {
                                if (!file.Sections.Any(fs => fs.SectionId == section.Id))
                                {
                                    var fileSection = new MusicPieceFileSection(file, section);
                                    _arpaContext.Add(fileSection);
                                    result.SectionsAssigned++;
                                }
                            }
                        }
                        else
                        {
                            result.SectionsAssigned += matchedSections.Count;
                        }
                    }

                    result.Assignments.Add(assignment);
                }

                if (!request.DryRun)
                {
                    await _arpaContext.SaveChangesAsync(cancellationToken);
                }

                return result;
            }
        }
    }
}
