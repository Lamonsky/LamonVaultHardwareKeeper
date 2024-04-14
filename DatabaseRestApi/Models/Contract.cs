using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Contract
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    [Column("Contract_Type")]
    public int? ContractType { get; set; }

    [Column("Start_Date", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("Contract_Duration")]
    public int? ContractDuration { get; set; }

    [Column("Account_Number")]
    [StringLength(255)]
    [Unicode(false)]
    public string? AccountNumber { get; set; }

    [Column("Payment_Terms")]
    public int? PaymentTerms { get; set; }

    [Column("Is_Renewable")]
    public bool? IsRenewable { get; set; }

    public int? Users { get; set; }

    [Column("Created_At", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("Created_By")]
    public int? CreatedBy { get; set; }

    [Column("Modified_At", TypeName = "datetime")]
    public DateTime? ModifiedAt { get; set; }

    [Column("Modified_By")]
    public int? ModifiedBy { get; set; }

    [ForeignKey("ContractType")]
    [InverseProperty("Contracts")]
    public virtual ContractType? ContractTypeNavigation { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("ContractCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Contracts")]
    public virtual Location? Location { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("ContractModifiedByNavigations")]
    public virtual User? ModifiedByNavigation { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Contracts")]
    public virtual Status? Status { get; set; }

    [ForeignKey("Users")]
    [InverseProperty("ContractUsersNavigations")]
    public virtual User? UsersNavigation { get; set; }
}
