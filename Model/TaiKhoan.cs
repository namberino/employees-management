using System.ComponentModel.DataAnnotations;

namespace FullProject.Model
{
    public class TaiKhoan
    {
        [Key]
        public string TenTaiKhoan { get; set; }

        [Required]
        [MaxLength(50)]
        public string MatKhau { get; set; }

        public string MaNV { get; set; }

        [Required]
        [MaxLength(100)]
        public string Role { get; set; }

        public NhanVien NhanVien { get; set; }
        // Thuộc tính LichSus
        public List<LichSu> LichSus { get; set; }
    }
}
