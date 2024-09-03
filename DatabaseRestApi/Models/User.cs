using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class User
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Usersname { get; set; }

    [Column("Last_Name")]
    [StringLength(255)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("First_Name")]
    [StringLength(255)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    [Column("Is_Active")]
    public bool? IsActive { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Phone1 { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Phone2 { get; set; }

    [Column("Internal_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InternalNumber { get; set; }

    public int? Position { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Computer> ComputerCreatedByNavigations { get; set; } = new List<Computer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<ComputerModel> ComputerModelCreatedByNavigations { get; set; } = new List<ComputerModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<ComputerModel> ComputerModelModifiedByNavigations { get; set; } = new List<ComputerModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Computer> ComputerModifiedByNavigations { get; set; } = new List<Computer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<ComputerType> ComputerTypeCreatedByNavigations { get; set; } = new List<ComputerType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<ComputerType> ComputerTypeModifiedByNavigations { get; set; } = new List<ComputerType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Computer> ComputerUsersNavigations { get; set; } = new List<Computer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Contract> ContractCreatedByNavigations { get; set; } = new List<Contract>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Contract> ContractModifiedByNavigations { get; set; } = new List<Contract>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<ContractType> ContractTypeCreatedByNavigations { get; set; } = new List<ContractType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<ContractType> ContractTypeModifiedByNavigations { get; set; } = new List<ContractType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Contract> ContractUsersNavigations { get; set; } = new List<Contract>();

    [ForeignKey("CreatedBy")]
    [InverseProperty("InverseCreatedByNavigation")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Device> DeviceCreatedByNavigations { get; set; } = new List<Device>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<DeviceModel> DeviceModelCreatedByNavigations { get; set; } = new List<DeviceModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<DeviceModel> DeviceModelModifiedByNavigations { get; set; } = new List<DeviceModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Device> DeviceModifiedByNavigations { get; set; } = new List<Device>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<DeviceType> DeviceTypeCreatedByNavigations { get; set; } = new List<DeviceType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<DeviceType> DeviceTypeModifiedByNavigations { get; set; } = new List<DeviceType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Device> DeviceUsersNavigations { get; set; } = new List<Device>();

    [InverseProperty("Users")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<HardDrife> HardDrifeCreatedByNavigations { get; set; } = new List<HardDrife>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<HardDrife> HardDrifeModifiedByNavigations { get; set; } = new List<HardDrife>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<HardDriveModel> HardDriveModelCreatedByNavigations { get; set; } = new List<HardDriveModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<HardDriveModel> HardDriveModelModifiedByNavigations { get; set; } = new List<HardDriveModel>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<User> InverseModifiedByNavigation { get; set; } = new List<User>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<KnowledgeBaseCategory> KnowledgeBaseCategoryCreatedByNavigations { get; set; } = new List<KnowledgeBaseCategory>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<KnowledgeBaseCategory> KnowledgeBaseCategoryModifiedByNavigations { get; set; } = new List<KnowledgeBaseCategory>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<KnowledgeBase> KnowledgeBaseCreatedByNavigations { get; set; } = new List<KnowledgeBase>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<KnowledgeBase> KnowledgeBaseModifiedByNavigations { get; set; } = new List<KnowledgeBase>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<KnowledgeBase> KnowledgeBaseUsersNavigations { get; set; } = new List<KnowledgeBase>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<License> LicenseCreatedByNavigations { get; set; } = new List<License>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<License> LicenseModifiedByNavigations { get; set; } = new List<License>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<LicenseType> LicenseTypeCreatedByNavigations { get; set; } = new List<LicenseType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<LicenseType> LicenseTypeModifiedByNavigations { get; set; } = new List<LicenseType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<License> LicenseUsersNavigations { get; set; } = new List<License>();

    [ForeignKey("LocationId")]
    [InverseProperty("Users")]
    public virtual Location? Location { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Location> LocationCreatedByNavigations { get; set; } = new List<Location>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Location> LocationModifiedByNavigations { get; set; } = new List<Location>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Log> LogCreatedByNavigations { get; set; } = new List<Log>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Log> LogModifiedByNavigations { get; set; } = new List<Log>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Log> LogUsersNavigations { get; set; } = new List<Log>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Manufacturer> ManufacturerCreatedByNavigations { get; set; } = new List<Manufacturer>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Manufacturer> ManufacturerModifiedByNavigations { get; set; } = new List<Manufacturer>();

    [ForeignKey("ModifiedBy")]
    [InverseProperty("InverseModifiedByNavigation")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Monitor> MonitorCreatedByNavigations { get; set; } = new List<Monitor>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MonitorModel> MonitorModelCreatedByNavigations { get; set; } = new List<MonitorModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MonitorModel> MonitorModelModifiedByNavigations { get; set; } = new List<MonitorModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Monitor> MonitorModifiedByNavigations { get; set; } = new List<Monitor>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<MonitorType> MonitorTypeCreatedByNavigations { get; set; } = new List<MonitorType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<MonitorType> MonitorTypeModifiedByNavigations { get; set; } = new List<MonitorType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Monitor> MonitorUsersNavigations { get; set; } = new List<Monitor>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<NetworkDevice> NetworkDeviceCreatedByNavigations { get; set; } = new List<NetworkDevice>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<NetworkDeviceModel> NetworkDeviceModelCreatedByNavigations { get; set; } = new List<NetworkDeviceModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<NetworkDeviceModel> NetworkDeviceModelModifiedByNavigations { get; set; } = new List<NetworkDeviceModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<NetworkDevice> NetworkDeviceModifiedByNavigations { get; set; } = new List<NetworkDevice>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<NetworkDeviceType> NetworkDeviceTypeCreatedByNavigations { get; set; } = new List<NetworkDeviceType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<NetworkDeviceType> NetworkDeviceTypeModifiedByNavigations { get; set; } = new List<NetworkDeviceType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<NetworkDevice> NetworkDeviceUsersNavigations { get; set; } = new List<NetworkDevice>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<OperatingSystem> OperatingSystemCreatedByNavigations { get; set; } = new List<OperatingSystem>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<OperatingSystem> OperatingSystemModifiedByNavigations { get; set; } = new List<OperatingSystem>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Phone> PhoneCreatedByNavigations { get; set; } = new List<Phone>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<PhoneModel> PhoneModelCreatedByNavigations { get; set; } = new List<PhoneModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<PhoneModel> PhoneModelModifiedByNavigations { get; set; } = new List<PhoneModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Phone> PhoneModifiedByNavigations { get; set; } = new List<Phone>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<PhoneType> PhoneTypeCreatedByNavigations { get; set; } = new List<PhoneType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<PhoneType> PhoneTypeModifiedByNavigations { get; set; } = new List<PhoneType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Phone> PhoneUsersNavigations { get; set; } = new List<Phone>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Position> PositionCreatedByNavigations { get; set; } = new List<Position>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Position> PositionModifiedByNavigations { get; set; } = new List<Position>();

    [ForeignKey("Position")]
    [InverseProperty("Users")]
    public virtual Position? PositionNavigation { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Printer> PrinterCreatedByNavigations { get; set; } = new List<Printer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<PrinterModel> PrinterModelCreatedByNavigations { get; set; } = new List<PrinterModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<PrinterModel> PrinterModelModifiedByNavigations { get; set; } = new List<PrinterModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Printer> PrinterModifiedByNavigations { get; set; } = new List<Printer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<PrinterType> PrinterTypeCreatedByNavigations { get; set; } = new List<PrinterType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<PrinterType> PrinterTypeModifiedByNavigations { get; set; } = new List<PrinterType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Printer> PrinterUsersNavigations { get; set; } = new List<Printer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Project> ProjectCreatedByNavigations { get; set; } = new List<Project>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Project> ProjectModifiedByNavigations { get; set; } = new List<Project>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<ProjectType> ProjectTypeCreatedByNavigations { get; set; } = new List<ProjectType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<ProjectType> ProjectTypeModifiedByNavigations { get; set; } = new List<ProjectType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Project> ProjectUsersNavigations { get; set; } = new List<Project>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<RackCabinet> RackCabinetCreatedByNavigations { get; set; } = new List<RackCabinet>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<RackCabinetModel> RackCabinetModelCreatedByNavigations { get; set; } = new List<RackCabinetModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<RackCabinetModel> RackCabinetModelModifiedByNavigations { get; set; } = new List<RackCabinetModel>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<RackCabinet> RackCabinetModifiedByNavigations { get; set; } = new List<RackCabinet>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<RackCabinetType> RackCabinetTypeCreatedByNavigations { get; set; } = new List<RackCabinetType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<RackCabinetType> RackCabinetTypeModifiedByNavigations { get; set; } = new List<RackCabinetType>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<RackCabinet> RackCabinetUsersNavigations { get; set; } = new List<RackCabinet>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Server> ServerCreatedByNavigations { get; set; } = new List<Server>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Server> ServerModifiedByNavigations { get; set; } = new List<Server>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Server> ServerUsersNavigations { get; set; } = new List<Server>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<SimCard> SimCardCreatedByNavigations { get; set; } = new List<SimCard>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<SimCard> SimCardModifiedByNavigations { get; set; } = new List<SimCard>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<SimCard> SimCardUsersNavigations { get; set; } = new List<SimCard>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<SimComponent> SimComponentCreatedByNavigations { get; set; } = new List<SimComponent>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<SimComponent> SimComponentModifiedByNavigations { get; set; } = new List<SimComponent>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<SimComponentType> SimComponentTypeCreatedByNavigations { get; set; } = new List<SimComponentType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<SimComponentType> SimComponentTypeModifiedByNavigations { get; set; } = new List<SimComponentType>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Software> SoftwareCreatedByNavigations { get; set; } = new List<Software>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Software> SoftwareModifiedByNavigations { get; set; } = new List<Software>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Software> SoftwareUsersNavigations { get; set; } = new List<Software>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Status> StatusCreatedByNavigations { get; set; } = new List<Status>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Status> StatusModifiedByNavigations { get; set; } = new List<Status>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Technician> TechnicianCreatedByNavigations { get; set; } = new List<Technician>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Technician> TechnicianModifiedByNavigations { get; set; } = new List<Technician>();

    [InverseProperty("Users")]
    public virtual ICollection<Technician> TechnicianUsers { get; set; } = new List<Technician>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TicketCategory> TicketCategoryCreatedByNavigations { get; set; } = new List<TicketCategory>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<TicketCategory> TicketCategoryModifiedByNavigations { get; set; } = new List<TicketCategory>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Ticket> TicketCreatedByNavigations { get; set; } = new List<Ticket>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Ticket> TicketModifiedByNavigations { get; set; } = new List<Ticket>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TicketStatus> TicketStatusCreatedByNavigations { get; set; } = new List<TicketStatus>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<TicketStatus> TicketStatusModifiedByNavigations { get; set; } = new List<TicketStatus>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TicketType> TicketTypeCreatedByNavigations { get; set; } = new List<TicketType>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<TicketType> TicketTypeModifiedByNavigations { get; set; } = new List<TicketType>();

    [InverseProperty("UserNavigation")]
    public virtual ICollection<Ticket> TicketUserNavigations { get; set; } = new List<Ticket>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Vendor> VendorCreatedByNavigations { get; set; } = new List<Vendor>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Vendor> VendorModifiedByNavigations { get; set; } = new List<Vendor>();

    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Vendor> VendorUsersNavigations { get; set; } = new List<Vendor>();
}
