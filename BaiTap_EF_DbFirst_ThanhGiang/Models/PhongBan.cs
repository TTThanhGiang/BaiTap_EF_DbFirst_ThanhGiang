using System;
using System.Collections.Generic;

namespace BaiTap_EF_DbFirst_ThanhGiang.Models;

public partial class PhongBan
{
    public int MaPhongBan { get; set; }

    public string? TenPhongBan { get; set; }

    public int? MaCongTy { get; set; }

    public virtual CongTy? MaCongTyNavigation { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
