using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[Table("WebUser")]
public partial class WebUser
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Username { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? RealName { get; set; }

    [Column("sdt")]
    public int Sdt { get; set; }

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("password")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Password { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Image { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Role { get; set; }

    public bool? Status { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AccessHistory> AccessHistories { get; set; } = new List<AccessHistory>();

    [InverseProperty("User")]
    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
