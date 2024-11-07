using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TaiKhoan
{
    public int UserId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UserPassword { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
}
