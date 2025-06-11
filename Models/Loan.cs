using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[Table("loans")]
public partial class Loan
{
    [Key]
    [Column("loan_id")]
    public long LoanId { get; set; }

    [Column("name")]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("price")]
    public long? Price { get; set; }
}
