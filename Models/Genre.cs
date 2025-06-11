using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[PrimaryKey("BookId", "Genre1")]
[Table("genre")]
public partial class Genre
{
    [Key]
    public long BookId { get; set; }

    [Key]
    [Column("genre")]
    [StringLength(100)]
    [Unicode(false)]
    public string Genre1 { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("GenresNavigation")]
    public virtual Book Book { get; set; } = null!;
}
