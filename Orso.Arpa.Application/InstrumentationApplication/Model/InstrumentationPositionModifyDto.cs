using System;

namespace Orso.Arpa.Application.InstrumentationApplication.Model
{
    public class InstrumentationPositionModifyDto
    {
        public Guid SectionId { get; set; }
        public int Quantity { get; set; }
        public Guid? QualificationId { get; set; }
        public string Label { get; set; }
        public string Comment { get; set; }
    }
}
