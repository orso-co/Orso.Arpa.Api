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
            // Includes German and English variants for all instruments
            private static readonly Dictionary<string, string> FilenameSectionMapping = new(StringComparer.OrdinalIgnoreCase)
            {
                // Saxophone (German + English)
                { "altsaxophon", "Saxophone" },
                { "alto saxophone", "Saxophone" },
                { "alto sax", "Saxophone" },
                { "sopransaxophon", "Saxophone" },
                { "soprano saxophone", "Saxophone" },
                { "soprano sax", "Saxophone" },
                { "tenorsaxophon", "Saxophone" },
                { "tenor saxophone", "Saxophone" },
                { "tenor sax", "Saxophone" },
                { "baritonsaxophon", "Saxophone" },
                { "baritone saxophone", "Saxophone" },
                { "bari sax", "Saxophone" },
                { "saxophon", "Saxophone" },
                { "saxophone", "Saxophone" },

                // Brass (German + English)
                { "trompete", "Trumpet" },
                { "trumpet", "Trumpet" },
                { "posaune", "Trombone" },
                { "trombone", "Trombone" },
                { "horn in f", "Horn" },
                { "french horn", "Horn" },
                { "tenorhorn", "Euphonium" },
                { "baritonhorn", "Euphonium" },
                { "euphonium", "Euphonium" },
                { "baritone horn", "Euphonium" },
                { "tenortuba", "Tuba" },
                { "tuba in c", "Tuba" },
                { "tuba in b", "Tuba" },
                { "tuba", "Tuba" },
                { "bass tuba", "Tuba" },
                { "basstuba", "Tuba" },

                // Strings (German + English)
                { "violine i", "Violin" },
                { "violine ii", "Violin" },
                { "violine 1", "Violin" },
                { "violine 2", "Violin" },
                { "violine", "Violin" },
                { "violin", "Violin" },
                { "1st violin", "Violin" },
                { "2nd violin", "Violin" },
                { "viola", "Viola" },
                { "bratsche", "Viola" },
                { "violoncello", "Violoncello" },
                { "cello", "Violoncello" },
                { "vcl", "Violoncello" },
                { "vc", "Violoncello" },
                { "kontrabass", "Double Bass" },
                { "contrabass", "Double Bass" },
                { "double bass", "Double Bass" },
                { "string bass", "Double Bass" },
                { "bass (string)", "Double Bass" },
                { "kb", "Double Bass" },
                { "cb", "Double Bass" },

                // Woodwinds (German + English)
                { "piccolo", "Flute" },
                { "pikkoloflöte", "Flute" },
                { "piccoloflöte", "Flute" },
                { "flöte", "Flute" },
                { "floete", "Flute" },
                { "flute", "Flute" },
                { "querflöte", "Flute" },
                { "querfloete", "Flute" },
                { "klarinette", "Clarinet" },
                { "clarinet", "Clarinet" },
                { "bassklarinette", "Clarinet" },
                { "bass clarinet", "Clarinet" },
                { "baßklarinette", "Clarinet" },
                { "klarinette in es", "Clarinet" },
                { "eb clarinet", "Clarinet" },
                { "oboe", "Oboe" },
                { "cor anglais", "Oboe" },
                { "english horn", "Oboe" },
                { "englisch horn", "Oboe" },
                { "englischhorn", "Oboe" },
                { "fagott", "Bassoon" },
                { "bassoon", "Bassoon" },
                { "kontrafagott", "Bassoon" },
                { "contrabassoon", "Bassoon" },
                { "contra bassoon", "Bassoon" },

                // Percussion (German + English)
                { "kleine trommel", "Drum Set (Orchestra)" },
                { "große trommel", "Drum Set (Orchestra)" },
                { "grosse trommel", "Drum Set (Orchestra)" },
                { "snare drum", "Drum Set (Orchestra)" },
                { "bass drum", "Drum Set (Orchestra)" },
                { "marschtrommel", "Drum Set (Orchestra)" },
                { "schlagzeug", "Drum Set (Orchestra)" },
                { "drums", "Drum Set (Orchestra)" },
                { "drum set", "Drum Set (Orchestra)" },
                { "percussion", "Drum Set (Orchestra)" },
                { "becken", "Drum Set (Orchestra)" },
                { "cymbals", "Drum Set (Orchestra)" },
                { "cymbal", "Drum Set (Orchestra)" },
                { "suspended cymbal", "Drum Set (Orchestra)" },
                { "piatti", "Drum Set (Orchestra)" },
                { "tam-tam", "Drum Set (Orchestra)" },
                { "tam tam", "Drum Set (Orchestra)" },
                { "anvil", "Drum Set (Orchestra)" },
                { "timpani", "Timpani" },
                { "pauken", "Timpani" },
                { "kettledrums", "Timpani" },
                { "mallets", "Mallets" },
                { "glockenspiel", "Mallets" },
                { "bells", "Mallets" },
                { "tubular bells", "Mallets" },
                { "chimes", "Mallets" },
                { "xylophon", "Mallets" },
                { "xylophone", "Mallets" },
                { "vibraphon", "Mallets" },
                { "vibraphone", "Mallets" },
                { "marimba", "Mallets" },

                // Keyboards (German + English)
                { "piano", "Keyboards" },
                { "klavier", "Keyboards" },
                { "keyboard", "Keyboards" },
                { "keyboards", "Keyboards" },
                { "organ", "Keyboards" },
                { "orgel", "Keyboards" },
                { "harmonium", "Keyboards" },
                { "celesta", "Keyboards" },
                { "celeste", "Keyboards" },
                { "akkordeon", "Accordion" },
                { "accordion", "Accordion" },

                // Choir voices - only explicit patterns to avoid conflicts
                { "sopran bibelformat", "Soprano" },
                { "soprano", "Soprano" },
                { "alt bibelformat", "Alto" },
                { "alto bibelformat", "Alto" },
                { "alto voice", "Alto" },
                { "tenor bibelformat", "Tenor" },
                { "tenor voice", "Tenor" },
                { "bass bibelformat", "Bass" },
                { "bass voice", "Bass" },
                { "baß", "Bass" },
                { "bariton", "Baritone" },
                { "baritone", "Baritone" },
                { "baritone voice", "Baritone" },

                // Other instruments (German + English)
                { "harfe", "Harp" },
                { "harp", "Harp" },
                { "gitarre", "Guitars" },
                { "guitar", "Guitars" },
                { "acoustic guitar", "Guitars" },
                { "e-bass", "Electric Bass (Band)" },
                { "electric bass", "Electric Bass (Band)" },
                { "bass guitar", "Electric Bass (Band)" },
                { "e-gitarre", "Electric Guitar (Band)" },
                { "electric guitar", "Electric Guitar (Band)" },
            };

            // Patterns that should be excluded from Bass (voice) matching
            private static readonly string[] BassInstrumentPatterns =
            {
                "kontrabass", "contrabass", "double bass", "string bass",
                "e-bass", "electric bass", "bass guitar", "bass tuba", "basstuba",
                "bassklarinette", "bass clarinet", "baßklarinette", "bass drum",
                "bass trombone", "bassposaune", "bass flute", "bassflöte"
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
                    // Use BassInstrumentPatterns to exclude all bass instruments
                    if (fileNameLower.Contains("bass") &&
                        !BassInstrumentPatterns.Any(p => fileNameLower.Contains(p)))
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
