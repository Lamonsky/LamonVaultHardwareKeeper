using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Ticket
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    public int? Type { get; set; }

    public int? Category { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    public int? Owner { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    public int? User { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Device { get; set; }

    [ForeignKey("Category")]
    [InverseProperty("Tickets")]
    public virtual TicketCategory? CategoryNavigation { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("TicketCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Tickets")]
    public virtual Location? Location { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("TicketModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Owner")]
    [InverseProperty("Tickets")]
    public virtual Technician? OwnerNavigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Tickets")]
    public virtual TicketStatus? Status { get; set; }

    [ForeignKey("Type")]
    [InverseProperty("Tickets")]
    public virtual TicketType? TypeNavigation { get; set; }

    [ForeignKey("User")]
    [InverseProperty("TicketUserNavigations")]
    public virtual User? UserNavigation { get; set; }
}
