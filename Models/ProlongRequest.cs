using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[Table("ProlongRequest")]
public partial class ProlongRequest
{
    [Key]
    public int RequestId { get; set; }

    public int? BorrowId { get; set; }

    public int? Days { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? Reason { get; set; }

    public int? Status { get; set; }

    [ForeignKey("BorrowId")]
    [InverseProperty("ProlongRequests")]
    public virtual Borrow? Borrow { get; set; }
}
