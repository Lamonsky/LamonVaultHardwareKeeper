using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("SIM_Components")]
public partial class SimComponent
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    public int? Manufacturer { get; set; }

    public int? Type { get; set; }

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
    [InverseProperty("SimComponentCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("Manufacturer")]
    [InverseProperty("SimComponents")]
    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("SimComponentModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("ComponentNavigation")]
    public virtual ICollection<SimCard> SimCards { get; set; } = new List<SimCard>();

    [ForeignKey("Status")]
    [InverseProperty("SimComponents")]
    public virtual Status? StatusNavigation { get; set; }

    [ForeignKey("Type")]
    [InverseProperty("SimComponents")]
    public virtual SimComponentType? TypeNavigation { get; set; }
}
