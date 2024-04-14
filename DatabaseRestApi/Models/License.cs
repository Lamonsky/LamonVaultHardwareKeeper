using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class License
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? Software { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("License_Type")]
    public int? LicenseType { get; set; }

    public int? Publisher { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    [Column("Serial_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SerialNumber { get; set; }

    [Column("Inventory_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InventoryNumber { get; set; }

    [Column("Expiry_Date", TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    public int? Users { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("LicenseCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("LicenseType")]
    [InverseProperty("Licenses")]
    public virtual LicenseType? LicenseTypeNavigation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Licenses")]
    public virtual Location? Location { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("LicenseModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Publisher")]
    [InverseProperty("Licenses")]
    public virtual Manufacturer? PublisherNavigation { get; set; }

    [ForeignKey("Software")]
    [InverseProperty("Licenses")]
    public virtual Software? SoftwareNavigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Licenses")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("LicenseUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
