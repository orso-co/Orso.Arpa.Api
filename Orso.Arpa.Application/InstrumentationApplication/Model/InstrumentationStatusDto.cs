using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.InstrumentationApplication.Model
{
    public class InstrumentationStatusDto
    {
        public Guid InstrumentationId { get; set; }
        public IList<InstrumentationPositionStatusDto> Positions { get; set; } = [];
        public int TotalRequired { get; set; }
        public int TotalFilled { get; set; }
    }

    public class InstrumentationPositionStatusDto
    {
        public Guid PositionId { get; set; }
        public Guid SectionId { get; set; }
        public string SectionName { get; set; }
        public string Label { get; set; }
        public int Required { get; set; }
        public int Filled { get; set; }
        public string RequiredQualification { get; set; }
        public IList<string> ActualQualifications { get; set; } = [];
        public bool QualificationMet { get; set; }
    }
}
