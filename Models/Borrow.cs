using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[Table("Borrow")]
public partial class Borrow
{
    [Key]
    public int BorrowId { get; set; }

    public int? UserId { get; set; }

    public long? BookId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Time { get; set; }

    [Column("deadline")]
    public int Deadline { get; set; }

    [Column("status")]
    public int Status { get; set; }

    public long? Fee { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReturnedTime { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Borrows")]
    public virtual Book? Book { get; set; }

    [InverseProperty("Borrow")]
    public virtual ICollection<ProlongRequest> ProlongRequests { get; set; } = new List<ProlongRequest>();

    [ForeignKey("UserId")]
    [InverseProperty("Borrows")]
    public virtual WebUser? User { get; set; }
}
