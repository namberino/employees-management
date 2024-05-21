using System.ComponentModel.DataAnnotations;

namespace FullProject.Model
{
    public class PhongBan
    {
        [Key]
        public string MaPhongBan { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenPhongBan { get; set; }

        public List<NhanVien> NhanViens { get; set; }
    }
}
