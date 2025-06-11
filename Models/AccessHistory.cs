using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PBL3_DUTLibrary.Models;

[PrimaryKey("AccessId", "UserId")]
[Table("AccessHistory")]
public partial class AccessHistory
{
    [Key]
    public long AccessId { get; set; }

    [Key]
    public int UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LoginTime { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AccessHistories")]
    public virtual WebUser User { get; set; } = null!;
}
