namespace Angularassessment.Dto
{
    public class Fielddto
    {
        public Guid Id { get; set; }

     
        public Guid? FormId { get; set; }

        public Guid? ColumnId { get; set; }

        public Guid? DomainTableId { get; set; }

        public string? Default { get; set; }
        public int? Sequence { get; set; }
        public string? Label { get; set; }

        public string? Type { get; set; }
        public string? Condition { get; set; }
        public int? QuoteReadOnly { get; set; }

        public int? QuoteRequired { get; set; }

        public int? QuoteDisplay { get; set; }
    }
}
