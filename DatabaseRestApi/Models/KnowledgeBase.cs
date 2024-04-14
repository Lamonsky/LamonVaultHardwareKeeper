using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Knowledge_Base")]
public partial class KnowledgeBase
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? Category { get; set; }

    [Unicode(false)]
    public string? Subject { get; set; }

    [Unicode(false)]
    public string? Content { get; set; }

    public int? Users { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    public int? Status { get; set; }

    [ForeignKey("Category")]
    [InverseProperty("KnowledgeBases")]
    public virtual KnowledgeBaseCategory? CategoryNavigation { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("KnowledgeBaseCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("KnowledgeBaseModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("KnowledgeBases")]
    public virtual Status? StatusNavigation { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("KnowledgeBaseUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
