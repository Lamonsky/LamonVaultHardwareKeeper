using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Rack_Cabinet_Types")]
public partial class RackCabinetType
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
    [InverseProperty("RackCabinetTypeCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("RackCabinetTypeModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("CabinetTypeNavigation")]
    public virtual ICollection<RackCabinet> RackCabinets { get; set; } = new List<RackCabinet>();

    [ForeignKey("Status")]
    [InverseProperty("RackCabinetTypes")]
    public virtual Status? StatusNavigation { get; set; }
}
