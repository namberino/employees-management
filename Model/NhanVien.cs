using FullProject.Pages;
using System;
using System.ComponentModel.DataAnnotations;

namespace FullProject.Model
{
    public class NhanVien
    {
        [Key]
        [StringLength(10)]
        public string MaNV { get; set; }

        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(12), MinLength(12, ErrorMessage = "CCCD gồm 12 kí tự")]
        public string CCCD { get; set; }

        [Required]
        [MaxLength(11)]
        public string SoDienThoai { get; set; }

        [Required]
        [MaxLength(100)]
        public string MailLamViec { get; set; }

        [Required]
        [MaxLength(100)]
        public string ChucVu { get; set; }

        public string MaPhongBan { get; set; }

        [Required]
        [MaxLength(100)]
        public string TrangThai { get; set; }

        [Required]
        [MaxLength(100)]
        public string LoaiNhanVien { get; set; }

        public DateTime BatDauLamViec { get; set; }

        public DateTime ChinhThucLamViec { get; set; }

        [Required]
        [MaxLength(10)]
        public string YeuCauChamCong { get; set; }

        public DateTime? NgayNghiViec { get; set; }

        [Required]
        [MaxLength(10)]
        public string GioiTinh { get; set; }

        [Required]
        public string DiaChiTamChu { get; set; }

        [Required]
        public string DiaChiThuongChu { get; set; }

        [Required]
        [MaxLength(100)]
        public string TrinhDoHocVan { get; set; }

        [Required]
        [MaxLength(50)]
        public string STKNganHang { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenNganHang { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaSoThue { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaBaoHiem { get; set; }


        public List<ThuongPhat> ThuongPhats { get; set; }
        public PhongBan PhongBan { get; set; }
        public List<BangCong> BangCongs { get; set; }

        // Thuộc tính ChamCongs
        public List<ChamCong> ChamCongs { get; set; }

        // Thuộc tính Luongs
        public List<Luong> Luongs { get; set; }

        // Thuộc tính TaiKhoan
        public TaiKhoan TaiKhoan { get; set; }
    }
}
