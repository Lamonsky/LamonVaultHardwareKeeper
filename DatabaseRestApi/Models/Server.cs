using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Server
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

    [StringLength(255)]
    [Unicode(false)]
    public string? Processor { get; set; }

    [Column("RAM")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Ram { get; set; }

    [Column("Operating_System")]
    public int? OperatingSystem { get; set; }

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
    [InverseProperty("ServerCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("ServerNavigation")]
    public virtual ICollection<HardDrife> HardDrives { get; set; } = new List<HardDrife>();

    [ForeignKey("LocationId")]
    [InverseProperty("Servers")]
    public virtual Location? Location { get; set; }

    [ForeignKey("Manufacturer")]
    [InverseProperty("Servers")]
    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    [ForeignKey("Model")]
    [InverseProperty("Servers")]
    public virtual ComputerModel? ModelNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("ServerModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("OperatingSystem")]
    [InverseProperty("Servers")]
    public virtual OperatingSystem? OperatingSystemNavigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Servers")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("ServerUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
