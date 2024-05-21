using System.ComponentModel.DataAnnotations;

namespace FullProject.Model
{
    public class ThuongPhat
    {
        [Key]
        public int MaThuongPhat { get; set; }

        public string MaNV { get; set; }

        [Required]
        [MaxLength(50)]
        public string Loai { get; set; }

        [Required]
        public string NguonThuongPhat { get; set; }

        public DateTime Ngay { get; set; }

        public decimal SoTien { get; set; }

        public NhanVien NhanVien { get; set; }
    }
}
