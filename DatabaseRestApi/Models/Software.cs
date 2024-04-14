using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Software")]
public partial class Software
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    public int? Publisher { get; set; }

    public int? Users { get; set; }

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
    [InverseProperty("SoftwareCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("SoftwareNavigation")]
    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    [ForeignKey("LocationId")]
    [InverseProperty("Softwares")]
    public virtual Location? Location { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("SoftwareModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Publisher")]
    [InverseProperty("Softwares")]
    public virtual Manufacturer? PublisherNavigation { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("Softwares")]
    public virtual Status? StatusNavigation { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("SoftwareUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
