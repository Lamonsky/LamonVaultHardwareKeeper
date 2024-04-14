using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Manufacturer
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

    public int? Status { get; set; }

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    [ForeignKey("CreatedBy")]
    [InverseProperty("ManufacturerCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<HardDrife> HardDrives { get; set; } = new List<HardDrife>();

    [InverseProperty("PublisherNavigation")]
    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    [ForeignKey("ModifiedBy")]
    [InverseProperty("ManufacturerModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<NetworkDevice> NetworkDevices { get; set; } = new List<NetworkDevice>();

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<Printer> Printers { get; set; } = new List<Printer>();

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<RackCabinet> RackCabinets { get; set; } = new List<RackCabinet>();

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<Server> Servers { get; set; } = new List<Server>();

    [InverseProperty("ManufacturerNavigation")]
    public virtual ICollection<SimComponent> SimComponents { get; set; } = new List<SimComponent>();

    [InverseProperty("PublisherNavigation")]
    public virtual ICollection<Software> Softwares { get; set; } = new List<Software>();

    [ForeignKey("Status")]
    [InverseProperty("Manufacturers")]
    public virtual Status? StatusNavigation { get; set; }
}
