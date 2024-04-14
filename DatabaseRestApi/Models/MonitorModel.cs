﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Monitor_Models")]
public partial class MonitorModel
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Comment { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    public int? Status { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("MonitorModelCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("MonitorModelModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("ModelNavigation")]
    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();

    [ForeignKey("Status")]
    [InverseProperty("MonitorModels")]
    public virtual Status? StatusNavigation { get; set; }
}
