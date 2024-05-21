using System.ComponentModel.DataAnnotations;

namespace FullProject.Model
{
    public class BangPhatDiMuon
    {
        [Key]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Điền đúng định dạng mm/yyyy")]
        public string ThangTinhCong { get; set; }

        public int SoGioTinhMuon { get; set; }

        public decimal SoTienPhatMuon { get; set; }

        public decimal SoTienPhatNghiLam { get; set; }

        public List<Luong> Luong { get; set; }


    }
}
