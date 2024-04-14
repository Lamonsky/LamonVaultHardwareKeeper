using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Network_Devices")]
public partial class NetworkDevice
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

    public int? Manufacturer { get; set; }

    public int? Model { get; set; }

    [Column("Device_Type")]
    public int? DeviceType { get; set; }

    public int? Rack { get; set; }

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

    [ForeignKey("CreatedBy")]
    [InverseProperty("NetworkDeviceCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("DeviceType")]
    [InverseProperty("NetworkDevices")]
    public virtual NetworkDeviceType? DeviceTypeNavigation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("NetworkDevices")]
    public virtual Location? Location { get; set; }

    [ForeignKey("Manufacturer")]
    [InverseProperty("NetworkDevices")]
    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    [ForeignKey("Model")]
    [InverseProperty("NetworkDevices")]
    public virtual NetworkDeviceModel? ModelNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("NetworkDeviceModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Rack")]
    [InverseProperty("NetworkDevices")]
    public virtual RackCabinet? RackNavigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("NetworkDevices")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("NetworkDeviceUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
