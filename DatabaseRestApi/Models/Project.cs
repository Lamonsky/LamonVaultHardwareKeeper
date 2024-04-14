using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Project
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Priority { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Code { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column("Project_Type")]
    public int? ProjectType { get; set; }

    public int? Users { get; set; }

    [Column("Planned_Start_Date", TypeName = "datetime")]
    public DateTime? PlannedStartDate { get; set; }

    [Column("Planned_End_Date", TypeName = "datetime")]
    public DateTime? PlannedEndDate { get; set; }

    [Column("Actual_Start_Date", TypeName = "datetime")]
    public DateTime? ActualStartDate { get; set; }

    [Column("Actual_End_Date", TypeName = "datetime")]
    public DateTime? ActualEndDate { get; set; }

    [Unicode(false)]
    public string? Description { get; set; }

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

    [ForeignKey("CreatedBy")]
    [InverseProperty("ProjectCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("ProjectModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("ProjectType")]
    [InverseProperty("Projects")]
    public virtual ProjectType? ProjectTypeNavigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Projects")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("ProjectUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
