using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Rack_Cabinets")]
public partial class RackCabinet
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    [Column("Cabinet_Type")]
    public int? CabinetType { get; set; }

    public int? Manufacturer { get; set; }

    public int? Model { get; set; }

    public int? Height { get; set; }

    [Column("Serial_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SerialNumber { get; set; }

    [Column("Inventory_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InventoryNumber { get; set; }

    public int? Users { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    [ForeignKey("CabinetType")]
    [InverseProperty("RackCabinets")]
    public virtual RackCabinetType? CabinetTypeNavigation { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("RackCabinetCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("RackCabinets")]
    public virtual Location? Location { get; set; }

    [ForeignKey("Manufacturer")]
    [InverseProperty("RackCabinets")]
    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    [ForeignKey("Model")]
    [InverseProperty("RackCabinets")]
    public virtual RackCabinetModel? ModelNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("RackCabinetModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("RackNavigation")]
    public virtual ICollection<NetworkDevice> NetworkDevices { get; set; } = new List<NetworkDevice>();

    [ForeignKey("StatusId")]
    [InverseProperty("RackCabinets")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("RackCabinetUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
