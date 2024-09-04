using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Status
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

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<ComputerModel> ComputerModels { get; set; } = new List<ComputerModel>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<ComputerType> ComputerTypes { get; set; } = new List<ComputerType>();

    [InverseProperty("Status")]
    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    [ForeignKey("CreatedBy")]
    [InverseProperty("StatusCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<DeviceType> DeviceTypes { get; set; } = new List<DeviceType>();

    [InverseProperty("Status")]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<HardDriveModel> HardDriveModels { get; set; } = new List<HardDriveModel>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<HardDrife> HardDrives { get; set; } = new List<HardDrife>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<LicenseType> LicenseTypes { get; set; } = new List<LicenseType>();

    [InverseProperty("Status")]
    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();

    [ForeignKey("ModifiedBy")]
    [InverseProperty("StatusModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<MonitorModel> MonitorModels { get; set; } = new List<MonitorModel>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<MonitorType> MonitorTypes { get; set; } = new List<MonitorType>();

    [InverseProperty("Status")]
    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<NetworkDeviceModel> NetworkDeviceModels { get; set; } = new List<NetworkDeviceModel>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<NetworkDeviceType> NetworkDeviceTypes { get; set; } = new List<NetworkDeviceType>();

    [InverseProperty("Status")]
    public virtual ICollection<NetworkDevice> NetworkDevices { get; set; } = new List<NetworkDevice>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<OperatingSystem> OperatingSystems { get; set; } = new List<OperatingSystem>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<PhoneModel> PhoneModels { get; set; } = new List<PhoneModel>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<PhoneType> PhoneTypes { get; set; } = new List<PhoneType>();

    [InverseProperty("Status")]
    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<PrinterModel> PrinterModels { get; set; } = new List<PrinterModel>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<PrinterType> PrinterTypes { get; set; } = new List<PrinterType>();

    [InverseProperty("Status")]
    public virtual ICollection<Printer> Printers { get; set; } = new List<Printer>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<RackCabinetModel> RackCabinetModels { get; set; } = new List<RackCabinetModel>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<RackCabinetType> RackCabinetTypes { get; set; } = new List<RackCabinetType>();

    [InverseProperty("Status")]
    public virtual ICollection<RackCabinet> RackCabinets { get; set; } = new List<RackCabinet>();

    [InverseProperty("Status")]
    public virtual ICollection<Server> Servers { get; set; } = new List<Server>();

    [InverseProperty("Status")]
    public virtual ICollection<SimCard> SimCards { get; set; } = new List<SimCard>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<SimComponentType> SimComponentTypes { get; set; } = new List<SimComponentType>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<SimComponent> SimComponents { get; set; } = new List<SimComponent>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<Software> Softwares { get; set; } = new List<Software>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<Technician> Technicians { get; set; } = new List<Technician>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<TicketStatus> TicketStatuses { get; set; } = new List<TicketStatus>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
}
