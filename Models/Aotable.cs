﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Angularassessment.Models;

public partial class Aotable
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public string? Comment { get; set; }

    public int? History { get; set; }

    public int? Boundary { get; set; }

    public int? Log { get; set; }

    public int? Cache { get; set; }

    public int? Notify { get; set; }

    public int? Identifier { get; set; }

    public int Premium { get; set; }

    [JsonIgnore]
    public virtual ICollection<Aocolumn> Aocolumns { get; set; } = new List<Aocolumn>();
    [JsonIgnore]
    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
