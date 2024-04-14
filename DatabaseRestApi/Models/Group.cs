using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Group
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Users_ID")]
    public int? UsersId { get; set; }

    [Column("Group_Type_ID")]
    public int? GroupTypeId { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    [ForeignKey("GroupTypeId")]
    [InverseProperty("Groups")]
    public virtual GroupsType? GroupType { get; set; }

    [ForeignKey("UsersId")]
    [InverseProperty("Groups")]
    public virtual User? Users { get; set; }
}
