using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Angularassessment.Models;

public partial class DomainTable
{
    public Guid Id { get; set; }

    public Guid? RatebookId { get; set; }

    public Guid? TableId { get; set; }

    public string? Title { get; set; }

    public string? Type { get; set; }

    public string? Comment { get; set; }

    public string? AssemblyName { get; set; }

    public string? AssemblyType { get; set; }

    public string? AssemblyMethod { get; set; }

    public string? XmlType { get; set; }

    [JsonIgnore]
    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();
}
