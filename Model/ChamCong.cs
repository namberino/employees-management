namespace FullProject.Model
{
    public class ChamCong
    {
        public string MaNV { get; set; }

        public DateTime NgayChamCong { get; set; }

        public TimeSpan GioVao { get; set; }

        public TimeSpan GioRa { get; set; }

        public NhanVien NhanVien { get; set; }
    }
}
