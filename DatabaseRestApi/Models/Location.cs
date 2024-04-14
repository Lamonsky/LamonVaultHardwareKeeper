using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Location
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("Postal_Code")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Country { get; set; }

    [Column("Building_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? BuildingNumber { get; set; }

    [Column("Room_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? RoomNumber { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    public int? Status { get; set; }

    [InverseProperty("Location")]
    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    [InverseProperty("Location")]
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    [ForeignKey("CreatedBy")]
    [InverseProperty("LocationCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("Location")]
    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    [ForeignKey("ModifiedBy")]
    [InverseProperty("LocationModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [InverseProperty("Location")]
    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();

    [InverseProperty("Location")]
    public virtual ICollection<NetworkDevice> NetworkDevices { get; set; } = new List<NetworkDevice>();

    [InverseProperty("Location")]
    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    [InverseProperty("Location")]
    public virtual ICollection<Printer> Printers { get; set; } = new List<Printer>();

    [InverseProperty("Location")]
    public virtual ICollection<RackCabinet> RackCabinets { get; set; } = new List<RackCabinet>();

    [InverseProperty("Location")]
    public virtual ICollection<Server> Servers { get; set; } = new List<Server>();

    [InverseProperty("Location")]
    public virtual ICollection<Software> Softwares { get; set; } = new List<Software>();

    [ForeignKey("Status")]
    [InverseProperty("Locations")]
    public virtual Status? StatusNavigation { get; set; }

    [InverseProperty("Location")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [InverseProperty("Location")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
