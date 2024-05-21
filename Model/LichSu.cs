using System.ComponentModel.DataAnnotations;

namespace FullProject.Model
{
    public class LichSu
    {
        [Key]
        public string ID { get; set; }

        public string TenTaiKhoan { get; set; }

        public DateTime NgayGio { get; set; }

        [Required]
        [MaxLength(200)]
        public string HoatDong { get; set; }

        public TaiKhoan TaiKhoan { get; set; }
    }
}
