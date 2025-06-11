using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[Table("BookGenre")]
public partial class BookGenre
{
    [Key]
    public int GenreId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [ForeignKey("GenreId")]
    [InverseProperty("Genres")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
