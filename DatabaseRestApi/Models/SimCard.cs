using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("SIM_Cards")]
public partial class SimCard
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("PIN_Code")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PinCode { get; set; }

    [Column("PUK_Code")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PukCode { get; set; }

    public int? Component { get; set; }

    [Column("Serial_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SerialNumber { get; set; }

    [Column("Inventory_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InventoryNumber { get; set; }

    [Column("Phone_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    public int? Users { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    [ForeignKey("Component")]
    [InverseProperty("SimCards")]
    public virtual SimComponent? ComponentNavigation { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("SimCardCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("SimCardModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("SimCard1Navigation")]
    public virtual ICollection<Phone> PhoneSimCard1Navigations { get; set; } = new List<Phone>();

    [InverseProperty("SimCard2Navigation")]
    public virtual ICollection<Phone> PhoneSimCard2Navigations { get; set; } = new List<Phone>();

    [ForeignKey("StatusId")]
    [InverseProperty("SimCards")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("SimCardUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
