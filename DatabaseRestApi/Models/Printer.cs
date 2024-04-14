using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Printer
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

    [Column("Printer_Type")]
    public int? PrinterType { get; set; }

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

    [Column("IP_Address")]
    [StringLength(255)]
    [Unicode(false)]
    public string? IpAddress { get; set; }

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
    [InverseProperty("PrinterCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Printers")]
    public virtual Location? Location { get; set; }

    [ForeignKey("Manufacturer")]
    [InverseProperty("Printers")]
    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    [ForeignKey("Model")]
    [InverseProperty("Printers")]
    public virtual PrinterModel? ModelNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("PrinterModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("PrinterType")]
    [InverseProperty("Printers")]
    public virtual PrinterType? PrinterTypeNavigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Printers")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("PrinterUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
