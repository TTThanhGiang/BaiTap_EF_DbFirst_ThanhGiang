using System.ComponentModel;

namespace BaiTap_EF_DbFirst_ThanhGiang.Models
{
    public class NhanVienViewModel
    {
        [DisplayName("Mã Nhân Viên")]
        public int NhanVienId { get; set; }

        [DisplayName("Họ Tên")]
        public string Ten { get; set; }

        [DisplayName("Ngày sinh")]
        public DateOnly NgaySinh { get; set; }

        // Khóa ngoại
        [DisplayName("Giới Tính")]
        public String GioiTinh { get; set; }

        [DisplayName("Tên Phòng Ban")]
        public string tenphong { get; set; }
    }
}
