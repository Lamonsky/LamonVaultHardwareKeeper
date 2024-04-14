using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Vendor
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("Administrative_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? AdministrativeNumber { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Fax { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Website { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Address { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? City { get; set; }

    [Column("Postal_Code")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Country { get; set; }

    [Column("Is_Active")]
    public bool? IsActive { get; set; }

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
    [InverseProperty("VendorCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("VendorModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("VendorUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
