using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Ticket_Statuses")]
public partial class TicketStatus
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

    public bool? CountToClosed { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("TicketStatusCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("TicketStatusModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("TicketStatuses")]
    public virtual Status? StatusNavigation { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
