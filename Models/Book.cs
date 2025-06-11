using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[Table("books")]
public partial class Book
{
    [Key]
    public long BookId { get; set; }

    [Column("title")]
    [Unicode(false)]
    public string? Title { get; set; }

    [Column("author")]
    [Unicode(false)]
    public string? Author { get; set; }

    [Column("available")]
    public long? Available { get; set; }

    [Column("description")]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("amount")]
    public long? Amount { get; set; }

    [Column("image")]
    [Unicode(false)]
    public string? Image { get; set; }

    public long? TotalAmount { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

    [InverseProperty("Book")]
    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    [InverseProperty("Book")]
    public virtual ICollection<Genre> GenresNavigation { get; set; } = new List<Genre>();

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    public virtual ICollection<BookGenre> Genres { get; set; } = new List<BookGenre>();

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    public virtual ICollection<WebUser> Users { get; set; } = new List<WebUser>();
}
