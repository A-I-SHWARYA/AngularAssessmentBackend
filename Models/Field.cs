using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Angularassessment.Models;

public partial class Field
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

    [JsonIgnore]
    public virtual Aocolumn? Column { get; set; }
    [JsonIgnore]
    public virtual DomainTable? DomainTable { get; set; }
    [JsonIgnore]
    public virtual Form? Form { get; set; }
}
