namespace FullProject.Model
{
    public class Luong
    {
        public string MaNV { get; set; }

        public string ThangLinhLuong { get; set; }

        public decimal LuongCung { get; set; }

        public decimal Thue { get; set; }

        public NhanVien NhanVien { get; set; }
        public BangPhatDiMuon BangPhatDiMuon { get; set; }
    }
}
