using System;
using System.Collections.Generic;

namespace BaiTap_EF_DbFirst_ThanhGiang.Models;

public partial class CongTy
{
    public int MaCongTy { get; set; }

    public string? TenCongTy { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<PhongBan> PhongBans { get; set; } = new List<PhongBan>();
}
