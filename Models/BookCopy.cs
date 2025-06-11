using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[PrimaryKey("BookCopyId", "BookId")]
[Table("book_copy")]
public partial class BookCopy
{
    [Key]
    public int BookCopyId { get; set; }

    [Key]
    public long BookId { get; set; }

    [Column("status")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Status { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BookCopies")]
    public virtual Book Book { get; set; } = null!;
}
