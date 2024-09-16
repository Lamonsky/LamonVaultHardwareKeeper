using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Models;

public partial class Template
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [Column("HTMLContent")]
    public string? Htmlcontent { get; set; }
}
