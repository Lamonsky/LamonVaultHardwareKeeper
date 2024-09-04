using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

[Table("PageContent")]
public partial class PageContent
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }
}
