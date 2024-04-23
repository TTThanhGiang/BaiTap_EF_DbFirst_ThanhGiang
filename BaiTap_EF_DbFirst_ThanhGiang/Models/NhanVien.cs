using System;
using System.Collections.Generic;

namespace BaiTap_EF_DbFirst_ThanhGiang.Models;

public partial class NhanVien
{
    public int MaNhanVien { get; set; }

    public string? TenNhanVien { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public int? MaPhongBan { get; set; }

    public virtual PhongBan? MaPhongBanNavigation { get; set; }
}
