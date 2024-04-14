using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Computer
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

    [Column("Computer_Type")]
    public int? ComputerType { get; set; }

    public int? Manufacturer { get; set; }

    public int? Model { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Processor { get; set; }

    [Column("RAM")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Ram { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Disk { get; set; }

    [Column("Graphics_Card")]
    [StringLength(255)]
    [Unicode(false)]
    public string? GraphicsCard { get; set; }

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

    [ForeignKey("ComputerType")]
    [InverseProperty("Computers")]
    public virtual ComputerType? ComputerTypeNavigation { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("ComputerCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Computers")]
    public virtual Location? Location { get; set; }

    [ForeignKey("Manufacturer")]
    [InverseProperty("Computers")]
    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    [ForeignKey("Model")]
    [InverseProperty("Computers")]
    public virtual ComputerModel? ModelNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("ComputerModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("OperatingSystem")]
    [InverseProperty("Computers")]
    public virtual OperatingSystem? OperatingSystemNavigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Computers")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("ComputerUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
