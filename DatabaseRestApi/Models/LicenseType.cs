using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("License_Types")]
public partial class LicenseType
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
    [InverseProperty("LicenseTypeCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("LicenseTypeNavigation")]
    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    [ForeignKey("ModifiedBy")]
    [InverseProperty("LicenseTypeModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("LicenseTypes")]
    public virtual Status? StatusNavigation { get; set; }
}
