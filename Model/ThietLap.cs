using System.ComponentModel.DataAnnotations;

namespace FullProject.Model
{
    public class ThietLap
    {
        [Key]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Điền đúng định dạng mm/yyyy")]
        public string ThangTinhCong { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public int NgayCongBatBuoc { get; set; }

        public int NgayPhepToiDa { get; set; }
    }
}
