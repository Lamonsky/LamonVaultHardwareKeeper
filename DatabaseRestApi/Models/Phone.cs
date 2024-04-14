using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Phone
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column("Phone_Type")]
    public int? PhoneType { get; set; }

    public int? Manufacturer { get; set; }

    public int? Model { get; set; }

    [Column("Serial_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SerialNumber { get; set; }

    [Column("Inventory_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InventoryNumber { get; set; }

    [Column("SIM_Card1")]
    public int? SimCard1 { get; set; }

    [Column("SIM_Card2")]
    public int? SimCard2 { get; set; }

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
    [InverseProperty("PhoneCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Phones")]
    public virtual Location? Location { get; set; }

    [ForeignKey("Manufacturer")]
    [InverseProperty("Phones")]
    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    [ForeignKey("Model")]
    [InverseProperty("Phones")]
    public virtual PhoneModel? ModelNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("PhoneModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("PhoneType")]
    [InverseProperty("Phones")]
    public virtual PhoneType? PhoneTypeNavigation { get; set; }

    [ForeignKey("SimCard1")]
    [InverseProperty("PhoneSimCard1Navigations")]
    public virtual SimCard? SimCard1Navigation { get; set; }

    [ForeignKey("SimCard2")]
    [InverseProperty("PhoneSimCard2Navigations")]
    public virtual SimCard? SimCard2Navigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Phones")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("PhoneUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
