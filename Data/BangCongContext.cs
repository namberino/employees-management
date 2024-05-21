using FullProject.Model;
using Microsoft.EntityFrameworkCore;

namespace FullProject.Data
{
    public class BangCongContext : DbContext
    {
        // Tùy chỉnh đường dẫn từng máy
        public string _connectionString = "Server=PhuongLinh\\SQLEXPRESS;Database=Projectweb;User Id=sa;Password=123;Trusted_Connection=true;TrustServerCertificate=true;";
        //public string _connectionString = "Data Source=SQL9001.site4now.net;Initial Catalog=db_aa7b06_hoagnvu20;User Id=db_aa7b06_hoagnvu20_admin;Password=hoangvu21;";

        public BangCongContext()
        {
        }

        public BangCongContext(DbContextOptions<BangCongContext> options) : base(options)
        {
        }

        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<ThietLap> ThietLaps { get; set; }
        public DbSet<BangCong> BangCongs { get; set; }
        public DbSet<ChamCong> ChamCongs { get; set; }
        public DbSet<Luong> Luongs { get; set; }
        public DbSet<ThuongPhat> ThuongPhats { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<LichSu> LichSus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThuongPhat>()
                .HasOne(tp => tp.NhanVien)
                .WithMany(nv => nv.ThuongPhats)
                .HasForeignKey(tp => tp.MaNV);

            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.PhongBan)
                .WithMany(pb => pb.NhanViens)
                .HasForeignKey(nv => nv.MaPhongBan);

            modelBuilder.Entity<BangCong>()
                .HasKey(bc => new { bc.MaNV, bc.ThangTinhCong }); // Khóa chính gồm MaNV và ThangTinhCong

            modelBuilder.Entity<BangCong>()
                .HasOne(bc => bc.NhanVien)
                .WithMany(nv => nv.BangCongs)
                .HasForeignKey(bc => bc.MaNV);

            modelBuilder.Entity<ChamCong>()
                .HasKey(cc => new { cc.MaNV, cc.NgayChamCong }); // Khóa chính gồm MaNV và NgayChamCong

            modelBuilder.Entity<ChamCong>()
                .HasOne(cc => cc.NhanVien)
                .WithMany(nv => nv.ChamCongs)
                .HasForeignKey(cc => cc.MaNV);

            modelBuilder.Entity<Luong>()
                .HasKey(l => new { l.MaNV, l.ThangLinhLuong }); // Khóa chính gồm MaNV và ThangLinhLuong

            modelBuilder.Entity<Luong>()
                .HasOne(l => l.NhanVien)
                .WithMany(nv => nv.Luongs)
                .HasForeignKey(l => l.MaNV);

            modelBuilder.Entity<TaiKhoan>()
                .HasKey(tk => tk.TenTaiKhoan); // Khóa chính chỉ gồm TenTaiKhoan

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(tk => tk.NhanVien)
                .WithOne(nv => nv.TaiKhoan)
                .HasForeignKey<TaiKhoan>(tk => tk.MaNV);

            modelBuilder.Entity<LichSu>()
                .HasKey(ls => ls.ID); // Khóa chính chỉ gồm ID

            modelBuilder.Entity<LichSu>()
                .HasOne(ls => ls.TaiKhoan)
                .WithMany(tk => tk.LichSus)
                .HasForeignKey(ls => ls.TenTaiKhoan);


            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
