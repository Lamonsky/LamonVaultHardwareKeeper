using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("Hard_Drives")]
public partial class HardDrife
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? Manufacturer { get; set; }

    public int? Model { get; set; }

    public int? Capacity { get; set; }

    public int? Server { get; set; }

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
    [InverseProperty("HardDrifeCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("Manufacturer")]
    [InverseProperty("HardDrives")]
    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    [ForeignKey("Model")]
    [InverseProperty("HardDrives")]
    public virtual HardDriveModel? ModelNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("HardDrifeModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("Server")]
    [InverseProperty("HardDrives")]
    public virtual Server? ServerNavigation { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("HardDrives")]
    public virtual Status? StatusNavigation { get; set; }
}
