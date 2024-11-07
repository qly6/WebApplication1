using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication1.Models;

public partial class HangHoa
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public double? Price { get; set; }

    public int? TaiKhoanId { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? TaiKhoanUpdateId { get; set; }

    public DateTime? NgayCapNhap { get; set; }

    [JsonIgnore]
    public virtual TaiKhoan? TaiKhoan { get; set; }
}
