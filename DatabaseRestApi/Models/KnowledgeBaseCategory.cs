using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Knowledge_Base_Categories")]
public partial class KnowledgeBaseCategory
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

    [ForeignKey("CreatedBy")]
    [InverseProperty("KnowledgeBaseCategoryCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("CategoryNavigation")]
    public virtual ICollection<KnowledgeBase> KnowledgeBases { get; set; } = new List<KnowledgeBase>();

    [ForeignKey("ModifiedBy")]
    [InverseProperty("KnowledgeBaseCategoryModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("KnowledgeBaseCategories")]
    public virtual Status? StatusNavigation { get; set; }
}
