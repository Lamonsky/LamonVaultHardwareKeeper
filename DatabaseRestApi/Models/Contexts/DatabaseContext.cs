using System;
using System.Collections.Generic;
using DatabaseRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models.Contexts;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Computer> Computers { get; set; }

    public virtual DbSet<ComputerModel> ComputerModels { get; set; }

    public virtual DbSet<ComputerType> ComputerTypes { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractType> ContractTypes { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceModel> DeviceModels { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupsType> GroupsTypes { get; set; }

    public virtual DbSet<HardDrife> HardDrives { get; set; }

    public virtual DbSet<HardDriveModel> HardDriveModels { get; set; }

    public virtual DbSet<KnowledgeBase> KnowledgeBases { get; set; }

    public virtual DbSet<KnowledgeBaseCategory> KnowledgeBaseCategories { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<LicenseType> LicenseTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<MonitorModel> MonitorModels { get; set; }

    public virtual DbSet<MonitorType> MonitorTypes { get; set; }

    public virtual DbSet<NetworkDevice> NetworkDevices { get; set; }

    public virtual DbSet<NetworkDeviceModel> NetworkDeviceModels { get; set; }

    public virtual DbSet<NetworkDeviceType> NetworkDeviceTypes { get; set; }

    public virtual DbSet<OperatingSystem> OperatingSystems { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PhoneModel> PhoneModels { get; set; }

    public virtual DbSet<PhoneType> PhoneTypes { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Printer> Printers { get; set; }

    public virtual DbSet<PrinterModel> PrinterModels { get; set; }

    public virtual DbSet<PrinterType> PrinterTypes { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectType> ProjectTypes { get; set; }

    public virtual DbSet<RackCabinet> RackCabinets { get; set; }

    public virtual DbSet<RackCabinetModel> RackCabinetModels { get; set; }

    public virtual DbSet<RackCabinetType> RackCabinetTypes { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<SimCard> SimCards { get; set; }

    public virtual DbSet<SimComponent> SimComponents { get; set; }

    public virtual DbSet<SimComponentType> SimComponentTypes { get; set; }

    public virtual DbSet<Software> Softwares { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=LamonVault_Hardware_Keeper;User ID=lamonvaulthardwarekeeperapiuser;Password=restapi123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Computer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Computer__3214EC271A5CD8D4");

            entity.HasOne(d => d.ComputerTypeNavigation).WithMany(p => p.Computers).HasConstraintName("FK__Computers__Compu__5E3FF0B0");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ComputerCreatedByNavigations).HasConstraintName("FK__Computers__Creat__6304A5CD");

            entity.HasOne(d => d.Location).WithMany(p => p.Computers).HasConstraintName("FK__Computers__Locat__5C57A83E");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Computers).HasConstraintName("FK__Computers__Manuf__5F3414E9");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.Computers).HasConstraintName("FK__Computers__Model__60283922");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ComputerModifiedByNavigations).HasConstraintName("FK__Computers__Modif__63F8CA06");

            entity.HasOne(d => d.OperatingSystemNavigation).WithMany(p => p.Computers).HasConstraintName("FK__Computers__Opera__611C5D5B");

            entity.HasOne(d => d.Status).WithMany(p => p.Computers).HasConstraintName("FK__Computers__Statu__5D4BCC77");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.ComputerUsersNavigations).HasConstraintName("FK__Computers__Users__62108194");
        });

        modelBuilder.Entity<ComputerModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Computer__3214EC27173D3E65");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ComputerModelCreatedByNavigations).HasConstraintName("FK__Computer___Creat__1A54DAB7");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ComputerModelModifiedByNavigations).HasConstraintName("FK__Computer___Modif__1B48FEF0");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.ComputerModels).HasConstraintName("FK_Computer_Models_Statuses");
        });

        modelBuilder.Entity<ComputerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Computer__3214EC27DFA1ABA2");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ComputerTypeCreatedByNavigations).HasConstraintName("FK__Computer___Creat__168449D3");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ComputerTypeModifiedByNavigations).HasConstraintName("FK__Computer___Modif__17786E0C");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.ComputerTypes).HasConstraintName("FK_Computer_Types_Statuses");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contract__3214EC27185C66C6");

            entity.HasOne(d => d.ContractTypeNavigation).WithMany(p => p.Contracts).HasConstraintName("FK__Contracts__Contr__75235608");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ContractCreatedByNavigations).HasConstraintName("FK__Contracts__Creat__76177A41");

            entity.HasOne(d => d.Location).WithMany(p => p.Contracts).HasConstraintName("FK__Contracts__Locat__733B0D96");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ContractModifiedByNavigations).HasConstraintName("FK__Contracts__Modif__770B9E7A");

            entity.HasOne(d => d.Status).WithMany(p => p.Contracts).HasConstraintName("FK__Contracts__Statu__742F31CF");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.ContractUsersNavigations).HasConstraintName("FK_Contracts_Users");
        });

        modelBuilder.Entity<ContractType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contract__3214EC2773A97630");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ContractTypeCreatedByNavigations).HasConstraintName("FK__Contract___Creat__3CA9F2BB");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ContractTypeModifiedByNavigations).HasConstraintName("FK__Contract___Modif__3D9E16F4");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.ContractTypes).HasConstraintName("FK_Contract_Types_Contract_Types");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devices__3214EC2745DF9BE0");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DeviceCreatedByNavigations).HasConstraintName("FK__Devices__Created__569ECEE8");

            entity.HasOne(d => d.DeviceTypeNavigation).WithMany(p => p.Devices).HasConstraintName("FK__Devices__Device___52CE3E04");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Devices).HasConstraintName("FK__Devices__Manufac__53C2623D");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.Devices).HasConstraintName("FK__Devices__Model__54B68676");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.DeviceModifiedByNavigations).HasConstraintName("FK__Devices__Modifie__5792F321");

            entity.HasOne(d => d.Status).WithMany(p => p.Devices).HasConstraintName("FK__Devices__StatusI__51DA19CB");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.DeviceUsersNavigations).HasConstraintName("FK__Devices__Users__55AAAAAF");
        });

        modelBuilder.Entity<DeviceModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Device_M__3214EC27C2A5A62E");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DeviceModelCreatedByNavigations).HasConstraintName("FK__Device_Mo__Creat__12B3B8EF");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.DeviceModelModifiedByNavigations).HasConstraintName("FK__Device_Mo__Modif__13A7DD28");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.DeviceModels).HasConstraintName("FK_Device_Models_Statuses");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Device_T__3214EC2761AAFB29");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DeviceTypeCreatedByNavigations).HasConstraintName("FK__Device_Ty__Creat__149C0161");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.DeviceTypeModifiedByNavigations).HasConstraintName("FK__Device_Ty__Modif__1590259A");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.DeviceTypes).HasConstraintName("FK_Device_Types_Statuses");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Groups__3214EC270AA90A3E");

            entity.HasOne(d => d.GroupType).WithMany(p => p.Groups).HasConstraintName("FK__Groups__Group_Ty__5B638405");

            entity.HasOne(d => d.Users).WithMany(p => p.Groups).HasConstraintName("FK__Groups__Users_ID__5A6F5FCC");
        });

        modelBuilder.Entity<GroupsType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Groups_T__3214EC27EEB7E0AD");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.GroupsTypes).HasConstraintName("FK_Groups_Types_Statuses");
        });

        modelBuilder.Entity<HardDrife>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hard_Dri__3214EC27CCC931FC");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.HardDrifeCreatedByNavigations).HasConstraintName("FK__Hard_Driv__Creat__027D5126");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.HardDrives).HasConstraintName("FK__Hard_Driv__Manuf__7FA0E47B");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.HardDrives).HasConstraintName("FK__Hard_Driv__Model__009508B4");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.HardDrifeModifiedByNavigations).HasConstraintName("FK__Hard_Driv__Modif__0371755F");

            entity.HasOne(d => d.ServerNavigation).WithMany(p => p.HardDrives).HasConstraintName("FK__Hard_Driv__Serve__01892CED");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.HardDrives).HasConstraintName("FK_Hard_Drives_Statuses");
        });

        modelBuilder.Entity<HardDriveModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hard_Dri__3214EC27096A8F9C");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.HardDriveModelCreatedByNavigations).HasConstraintName("FK__Hard_Driv__Creat__3E923B2D");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.HardDriveModelModifiedByNavigations).HasConstraintName("FK__Hard_Driv__Modif__3F865F66");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.HardDriveModels).HasConstraintName("FK_Hard_Drive_Models_Statuses");
        });

        modelBuilder.Entity<KnowledgeBase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Knowledg__3214EC27DEEE68B6");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.KnowledgeBases).HasConstraintName("FK__Knowledge__Categ__6E765879");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.KnowledgeBaseCreatedByNavigations).HasConstraintName("FK__Knowledge__Creat__6F6A7CB2");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.KnowledgeBaseModifiedByNavigations).HasConstraintName("FK__Knowledge__Modif__705EA0EB");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.KnowledgeBases).HasConstraintName("FK_Knowledge_Base_Statuses");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.KnowledgeBaseUsersNavigations).HasConstraintName("FK_Knowledge_Base_Users");
        });

        modelBuilder.Entity<KnowledgeBaseCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Knowledg__3214EC276D6A0312");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.KnowledgeBaseCategoryCreatedByNavigations).HasConstraintName("FK__Knowledge__Creat__7152C524");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.KnowledgeBaseCategoryModifiedByNavigations).HasConstraintName("FK__Knowledge__Modif__7246E95D");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.KnowledgeBaseCategories).HasConstraintName("FK_Knowledge_Base_Categories_Statuses");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Licenses__3214EC2788A5DE63");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LicenseCreatedByNavigations).HasConstraintName("FK__Licenses__Create__095F58DF");

            entity.HasOne(d => d.LicenseTypeNavigation).WithMany(p => p.Licenses).HasConstraintName("FK__Licenses__Licens__049AA3C2");

            entity.HasOne(d => d.Location).WithMany(p => p.Licenses).HasConstraintName("FK__Licenses__Locati__0777106D");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.LicenseModifiedByNavigations).HasConstraintName("FK__Licenses__Modifi__0A537D18");

            entity.HasOne(d => d.PublisherNavigation).WithMany(p => p.Licenses).HasConstraintName("FK__Licenses__Publis__058EC7FB");

            entity.HasOne(d => d.SoftwareNavigation).WithMany(p => p.Licenses).HasConstraintName("FK__Licenses__Softwa__03A67F89");

            entity.HasOne(d => d.Status).WithMany(p => p.Licenses).HasConstraintName("FK__Licenses__Status__0682EC34");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.LicenseUsersNavigations).HasConstraintName("FK__Licenses__Users__086B34A6");
        });

        modelBuilder.Entity<LicenseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__License___3214EC27F7748A1C");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LicenseTypeCreatedByNavigations).HasConstraintName("FK__License_T__Creat__33208881");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.LicenseTypeModifiedByNavigations).HasConstraintName("FK__License_T__Modif__3414ACBA");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.LicenseTypes).HasConstraintName("FK_License_Types_Statuses");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC2720FAF47E");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LocationCreatedByNavigations).HasConstraintName("FK__Locations__Creat__1E256B9B");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.LocationModifiedByNavigations).HasConstraintName("FK__Locations__Modif__1F198FD4");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Locations).HasConstraintName("FK_Locations_Statuses");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Logs__3214EC272C12E6BC");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LogCreatedByNavigations).HasConstraintName("FK__Logs__Created_By__10CB707D");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.LogModifiedByNavigations).HasConstraintName("FK__Logs__Modified_B__11BF94B6");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.LogUsersNavigations).HasConstraintName("FK__Logs__Users__0FD74C44");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC2747CFD553");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ManufacturerCreatedByNavigations).HasConstraintName("FK__Manufactu__Creat__186C9245");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ManufacturerModifiedByNavigations).HasConstraintName("FK__Manufactu__Modif__1960B67E");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Manufacturers).HasConstraintName("FK_Manufacturers_Manufacturers");
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Monitors__3214EC278F77A5CC");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MonitorCreatedByNavigations).HasConstraintName("FK__Monitors__Create__17E28260");

            entity.HasOne(d => d.Location).WithMany(p => p.Monitors).HasConstraintName("FK__Monitors__Locati__1229A90A");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Monitors).HasConstraintName("FK__Monitors__Manufa__150615B5");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.Monitors).HasConstraintName("FK__Monitors__Model__15FA39EE");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MonitorModifiedByNavigations).HasConstraintName("FK__Monitors__Modifi__18D6A699");

            entity.HasOne(d => d.MonitorTypeNavigation).WithMany(p => p.Monitors).HasConstraintName("FK__Monitors__Monito__1411F17C");

            entity.HasOne(d => d.Status).WithMany(p => p.Monitors).HasConstraintName("FK__Monitors__Status__131DCD43");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.MonitorUsersNavigations).HasConstraintName("FK__Monitors__Users__16EE5E27");
        });

        modelBuilder.Entity<MonitorModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Monitor___3214EC27006503B3");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MonitorModelCreatedByNavigations).HasConstraintName("FK__Monitor_M__Creat__21F5FC7F");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MonitorModelModifiedByNavigations).HasConstraintName("FK__Monitor_M__Modif__22EA20B8");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.MonitorModels).HasConstraintName("FK_Monitor_Models_Statuses");
        });

        modelBuilder.Entity<MonitorType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Monitor___3214EC27BF0489BB");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MonitorTypeCreatedByNavigations).HasConstraintName("FK__Monitor_T__Creat__200DB40D");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MonitorTypeModifiedByNavigations).HasConstraintName("FK__Monitor_T__Modif__2101D846");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.MonitorTypes).HasConstraintName("FK_Monitor_Types_Statuses");
        });

        modelBuilder.Entity<NetworkDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Network___3214EC279C5D94B7");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.NetworkDeviceCreatedByNavigations).HasConstraintName("FK__Network_D__Creat__253C7D7E");

            entity.HasOne(d => d.DeviceTypeNavigation).WithMany(p => p.NetworkDevices).HasConstraintName("FK__Network_D__Devic__226010D3");

            entity.HasOne(d => d.Location).WithMany(p => p.NetworkDevices).HasConstraintName("FK__Network_D__Locat__1E8F7FEF");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.NetworkDevices).HasConstraintName("FK__Network_D__Manuf__2077C861");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.NetworkDevices).HasConstraintName("FK__Network_D__Model__216BEC9A");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.NetworkDeviceModifiedByNavigations).HasConstraintName("FK__Network_D__Modif__2630A1B7");

            entity.HasOne(d => d.RackNavigation).WithMany(p => p.NetworkDevices).HasConstraintName("FK__Network_De__Rack__2354350C");

            entity.HasOne(d => d.Status).WithMany(p => p.NetworkDevices).HasConstraintName("FK__Network_D__Statu__1F83A428");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.NetworkDeviceUsersNavigations).HasConstraintName("FK__Network_D__Users__24485945");
        });

        modelBuilder.Entity<NetworkDeviceModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Network___3214EC276DCB3955");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.NetworkDeviceModelCreatedByNavigations).HasConstraintName("FK__Network_D__Creat__23DE44F1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.NetworkDeviceModelModifiedByNavigations).HasConstraintName("FK__Network_D__Modif__24D2692A");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.NetworkDeviceModels).HasConstraintName("FK_Network_Device_Models_Statuses");
        });

        modelBuilder.Entity<NetworkDeviceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Network___3214EC27EA6C2450");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.NetworkDeviceTypeCreatedByNavigations).HasConstraintName("FK__Network_D__Creat__25C68D63");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.NetworkDeviceTypeModifiedByNavigations).HasConstraintName("FK__Network_D__Modif__26BAB19C");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.NetworkDeviceTypes).HasConstraintName("FK_Network_Device_Types_Statuses");
        });

        modelBuilder.Entity<OperatingSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatin__3214EC2746C949F7");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OperatingSystemCreatedByNavigations).HasConstraintName("FK__Operating__Creat__407A839F");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.OperatingSystemModifiedByNavigations).HasConstraintName("FK__Operating__Modif__416EA7D8");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.OperatingSystems).HasConstraintName("FK_Operating_Systems_Statuses");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Phones__3214EC2740E8775B");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PhoneCreatedByNavigations).HasConstraintName("FK__Phones__Created___36670980");

            entity.HasOne(d => d.Location).WithMany(p => p.Phones).HasConstraintName("FK__Phones__Location__2EC5E7B8");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Phones).HasConstraintName("FK__Phones__Manufact__31A25463");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.Phones).HasConstraintName("FK__Phones__Model__3296789C");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PhoneModifiedByNavigations).HasConstraintName("FK__Phones__Modified__375B2DB9");

            entity.HasOne(d => d.PhoneTypeNavigation).WithMany(p => p.Phones).HasConstraintName("FK__Phones__Phone_Ty__30AE302A");

            entity.HasOne(d => d.SimCard1Navigation).WithMany(p => p.PhoneSimCard1Navigations).HasConstraintName("FK__Phones__SIM_Card__338A9CD5");

            entity.HasOne(d => d.SimCard2Navigation).WithMany(p => p.PhoneSimCard2Navigations).HasConstraintName("FK__Phones__SIM_Card__347EC10E");

            entity.HasOne(d => d.Status).WithMany(p => p.Phones).HasConstraintName("FK__Phones__StatusID__2FBA0BF1");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.PhoneUsersNavigations).HasConstraintName("FK__Phones__Users__3572E547");
        });

        modelBuilder.Entity<PhoneModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Phone_Mo__3214EC27CB0B2DE7");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PhoneModelCreatedByNavigations).HasConstraintName("FK__Phone_Mod__Creat__2D67AF2B");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PhoneModelModifiedByNavigations).HasConstraintName("FK__Phone_Mod__Modif__2E5BD364");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.PhoneModels).HasConstraintName("FK_Phone_Models_Statuses");
        });

        modelBuilder.Entity<PhoneType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Phone_Ty__3214EC27D86F2526");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PhoneTypeCreatedByNavigations).HasConstraintName("FK__Phone_Typ__Creat__2B7F66B9");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PhoneTypeModifiedByNavigations).HasConstraintName("FK__Phone_Typ__Modif__2C738AF2");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.PhoneTypes).HasConstraintName("FK_Phone_Types_Statuses");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC2719AB0892");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PositionCreatedByNavigations).HasConstraintName("FK__Positions__Creat__481BA567");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PositionModifiedByNavigations).HasConstraintName("FK__Positions__Modif__490FC9A0");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Positions).HasConstraintName("FK_Positions_Statuses");
        });

        modelBuilder.Entity<Printer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Printers__3214EC27DEF7F699");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PrinterCreatedByNavigations).HasConstraintName("FK__Printers__Create__2CDD9F46");

            entity.HasOne(d => d.Location).WithMany(p => p.Printers).HasConstraintName("FK__Printers__Locati__2724C5F0");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Printers).HasConstraintName("FK__Printers__Manufa__2A01329B");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.Printers).HasConstraintName("FK__Printers__Model__2AF556D4");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PrinterModifiedByNavigations).HasConstraintName("FK__Printers__Modifi__2DD1C37F");

            entity.HasOne(d => d.PrinterTypeNavigation).WithMany(p => p.Printers).HasConstraintName("FK__Printers__Printe__290D0E62");

            entity.HasOne(d => d.Status).WithMany(p => p.Printers).HasConstraintName("FK__Printers__Status__2818EA29");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.PrinterUsersNavigations).HasConstraintName("FK__Printers__Users__2BE97B0D");
        });

        modelBuilder.Entity<PrinterModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Printer___3214EC27F3998071");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PrinterModelCreatedByNavigations).HasConstraintName("FK__Printer_M__Creat__29971E47");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PrinterModelModifiedByNavigations).HasConstraintName("FK__Printer_M__Modif__2A8B4280");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.PrinterModels).HasConstraintName("FK_Printer_Models_Statuses");
        });

        modelBuilder.Entity<PrinterType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Printer___3214EC27CA3F21DE");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PrinterTypeCreatedByNavigations).HasConstraintName("FK__Printer_T__Creat__27AED5D5");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PrinterTypeModifiedByNavigations).HasConstraintName("FK__Printer_T__Modif__28A2FA0E");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.PrinterTypes).HasConstraintName("FK_Printer_Types_Statuses");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC271F1E24AD");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProjectCreatedByNavigations).HasConstraintName("FK__Projects__Create__0DEF03D2");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ProjectModifiedByNavigations).HasConstraintName("FK__Projects__Modifi__0EE3280B");

            entity.HasOne(d => d.ProjectTypeNavigation).WithMany(p => p.Projects).HasConstraintName("FK__Projects__Projec__0CFADF99");

            entity.HasOne(d => d.Status).WithMany(p => p.Projects).HasConstraintName("FK_Projects_Statuses");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.ProjectUsersNavigations).HasConstraintName("FK__Projects__Users__0C06BB60");
        });

        modelBuilder.Entity<ProjectType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Project___3214EC276F56CFD2");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProjectTypeCreatedByNavigations).HasConstraintName("FK__Project_T__Creat__46335CF5");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ProjectTypeModifiedByNavigations).HasConstraintName("FK__Project_T__Modif__4727812E");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.ProjectTypes).HasConstraintName("FK_Project_Types_Statuses");
        });

        modelBuilder.Entity<RackCabinet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rack_Cab__3214EC27C13ACA73");

            entity.HasOne(d => d.CabinetTypeNavigation).WithMany(p => p.RackCabinets).HasConstraintName("FK__Rack_Cabi__Cabin__064DE20A");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RackCabinetCreatedByNavigations).HasConstraintName("FK__Rack_Cabi__Creat__0A1E72EE");

            entity.HasOne(d => d.Location).WithMany(p => p.RackCabinets).HasConstraintName("FK__Rack_Cabi__Locat__0559BDD1");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.RackCabinets).HasConstraintName("FK__Rack_Cabi__Manuf__07420643");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.RackCabinets).HasConstraintName("FK__Rack_Cabi__Model__08362A7C");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.RackCabinetModifiedByNavigations).HasConstraintName("FK__Rack_Cabi__Modif__0B129727");

            entity.HasOne(d => d.Status).WithMany(p => p.RackCabinets).HasConstraintName("FK__Rack_Cabi__Statu__04659998");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.RackCabinetUsersNavigations).HasConstraintName("FK__Rack_Cabi__Users__092A4EB5");
        });

        modelBuilder.Entity<RackCabinetModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rack_Cab__3214EC27A2E0E690");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RackCabinetModelCreatedByNavigations).HasConstraintName("FK__Rack_Cabi__Creat__444B1483");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.RackCabinetModelModifiedByNavigations).HasConstraintName("FK__Rack_Cabi__Modif__453F38BC");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.RackCabinetModels).HasConstraintName("FK_Rack_Cabinet_Models_Statuses");
        });

        modelBuilder.Entity<RackCabinetType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rack_Cab__3214EC27A14DA50A");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RackCabinetTypeCreatedByNavigations).HasConstraintName("FK__Rack_Cabi__Creat__4262CC11");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.RackCabinetTypeModifiedByNavigations).HasConstraintName("FK__Rack_Cabi__Modif__4356F04A");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.RackCabinetTypes).HasConstraintName("FK_Rack_Cabinet_Types_Statuses");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servers__3214EC27593357EA");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ServerCreatedByNavigations).HasConstraintName("FK__Servers__Created__7DB89C09");

            entity.HasOne(d => d.Location).WithMany(p => p.Servers).HasConstraintName("FK__Servers__Locatio__77FFC2B3");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Servers).HasConstraintName("FK__Servers__Manufac__79E80B25");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.Servers).HasConstraintName("FK__Servers__Model__7ADC2F5E");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ServerModifiedByNavigations).HasConstraintName("FK__Servers__Modifie__7EACC042");

            entity.HasOne(d => d.OperatingSystemNavigation).WithMany(p => p.Servers).HasConstraintName("FK__Servers__Operati__7BD05397");

            entity.HasOne(d => d.Status).WithMany(p => p.Servers).HasConstraintName("FK__Servers__StatusI__78F3E6EC");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.ServerUsersNavigations).HasConstraintName("FK__Servers__Users__7CC477D0");
        });

        modelBuilder.Entity<SimCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SIM_Card__3214EC272B0297DC");

            entity.HasOne(d => d.ComponentNavigation).WithMany(p => p.SimCards).HasConstraintName("FK__SIM_Cards__Compo__384F51F2");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SimCardCreatedByNavigations).HasConstraintName("FK__SIM_Cards__Creat__01BE3717");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SimCardModifiedByNavigations).HasConstraintName("FK__SIM_Cards__Modif__02B25B50");

            entity.HasOne(d => d.Status).WithMany(p => p.SimCards).HasConstraintName("FK__SIM_Cards__Statu__3943762B");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.SimCardUsersNavigations).HasConstraintName("FK__SIM_Cards__Users__00CA12DE");
        });

        modelBuilder.Entity<SimComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SIM_Comp__3214EC27EC43013C");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SimComponentCreatedByNavigations).HasConstraintName("FK__SIM_Compo__Creat__2F4FF79D");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.SimComponents).HasConstraintName("FK_SIM_Components_Manufacturers");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SimComponentModifiedByNavigations).HasConstraintName("FK__SIM_Compo__Modif__30441BD6");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.SimComponents).HasConstraintName("FK_SIM_Components_Statuses");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.SimComponents).HasConstraintName("FK_SIM_Components_SIM_Component_Types");
        });

        modelBuilder.Entity<SimComponentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SIM_Comp__3214EC27ECB241F5");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SimComponentTypeCreatedByNavigations).HasConstraintName("FK__SIM_Compo__Creat__3138400F");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SimComponentTypeModifiedByNavigations).HasConstraintName("FK__SIM_Compo__Modif__322C6448");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.SimComponentTypes).HasConstraintName("FK_SIM_Component_Types_Statuses");
        });

        modelBuilder.Entity<Software>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Software__3214EC27FDB6A9EB");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SoftwareCreatedByNavigations).HasConstraintName("FK__Software__Create__1CA7377D");

            entity.HasOne(d => d.Location).WithMany(p => p.Softwares).HasConstraintName("FK__Software__Locati__19CACAD2");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SoftwareModifiedByNavigations).HasConstraintName("FK__Software__Modifi__1D9B5BB6");

            entity.HasOne(d => d.PublisherNavigation).WithMany(p => p.Softwares).HasConstraintName("FK__Software__Publis__1ABEEF0B");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Softwares).HasConstraintName("FK_Software_Statuses");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.SoftwareUsersNavigations).HasConstraintName("FK__Software__Users__1BB31344");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Statuses__3214EC2747E298F4");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.StatusCreatedByNavigations).HasConstraintName("FK__Statuses__Create__1C3D2329");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.StatusModifiedByNavigations).HasConstraintName("FK__Statuses__Modifi__1D314762");
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technici__3214EC27485BD107");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TechnicianCreatedByNavigations).HasConstraintName("FK__Technicia__Creat__6C8E1007");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TechnicianModifiedByNavigations).HasConstraintName("FK__Technicia__Modif__6D823440");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Technicians).HasConstraintName("FK_Technicians_Statuses");

            entity.HasOne(d => d.Users).WithMany(p => p.TechnicianUsers).HasConstraintName("FK__Technicia__Users__6B99EBCE");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC27742023CB");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Tickets).HasConstraintName("FK__Tickets__Categor__66D536B1");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketCreatedByNavigations).HasConstraintName("FK__Tickets__Created__69B1A35C");

            entity.HasOne(d => d.Location).WithMany(p => p.Tickets).HasConstraintName("FK__Tickets__Locatio__64ECEE3F");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TicketModifiedByNavigations).HasConstraintName("FK__Tickets__Modifie__6AA5C795");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Tickets).HasConstraintName("FK__Tickets__Owner__68BD7F23");

            entity.HasOne(d => d.Status).WithMany(p => p.Tickets).HasConstraintName("FK__Tickets__StatusI__67C95AEA");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Tickets).HasConstraintName("FK__Tickets__Type__65E11278");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.TicketUserNavigations).HasConstraintName("FK_Tickets_Users");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket_C__3214EC277231A41E");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketCategoryCreatedByNavigations).HasConstraintName("FK__Ticket_Ca__Creat__36F11965");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TicketCategoryModifiedByNavigations).HasConstraintName("FK__Ticket_Ca__Modif__37E53D9E");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.TicketCategories).HasConstraintName("FK_Ticket_Categories_Statuses");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket_S__3214EC27F1EBACC1");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketStatusCreatedByNavigations).HasConstraintName("FK__Ticket_St__Creat__38D961D7");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TicketStatusModifiedByNavigations).HasConstraintName("FK__Ticket_St__Modif__39CD8610");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.TicketStatuses).HasConstraintName("FK_Ticket_Statuses_Statuses");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket_T__3214EC27B834B6BC");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketTypeCreatedByNavigations).HasConstraintName("FK__Ticket_Ty__Creat__3508D0F3");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TicketTypeModifiedByNavigations).HasConstraintName("FK__Ticket_Ty__Modif__35FCF52C");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.TicketTypes).HasConstraintName("FK_Ticket_Types_Statuses");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC278FD1F6FC");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation).HasConstraintName("FK_Users_Users");

            entity.HasOne(d => d.Location).WithMany(p => p.Users).HasConstraintName("FK__Users__LocationI__11F49EE0");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InverseModifiedByNavigation).HasConstraintName("FK_Users_Users1");

            entity.HasOne(d => d.PositionNavigation).WithMany(p => p.Users).HasConstraintName("FK__Users__Position__12E8C319");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vendors__3214EC275F8F5A94");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VendorCreatedByNavigations).HasConstraintName("FK__Vendors__Created__3AC1AA49");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.VendorModifiedByNavigations).HasConstraintName("FK__Vendors__Modifie__3BB5CE82");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.VendorUsersNavigations).HasConstraintName("FK_Vendors_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
