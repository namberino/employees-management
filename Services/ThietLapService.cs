using FullProject.Data;
using FullProject.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Globalization;
using OfficeOpenXml;
using System.Data;


namespace FullProject.Services
{
    public class ThietLapService
    {
        private readonly ThietLapContext _dbContext;

        public ThietLapService(ThietLapContext dbContext)
        {
            _dbContext = dbContext;
        }

        /* CÔNG VIỆC CỦA LINH */

        public async Task<List<string>> GetThangTinhCongValuesAsync()
        {
            return await _dbContext.ThietLaps.Select(t => t.ThangTinhCong).ToListAsync();
        }
        public async Task<List<BangPhatDiMuon>> GetAllPhatMuon()
        {
            return await _dbContext.PhatDiMuons.AsNoTracking().ToListAsync();
        }
        public async Task<List<BangPhatDiMuon>> GetPhatByThangTinhCong(string thangTinhCong)
        {
            return await _dbContext.PhatDiMuons
                                    .Where(tl => tl.ThangTinhCong == thangTinhCong)
                                    .AsNoTracking()
                                    .ToListAsync();
        }
        public async Task AddPhat(BangPhatDiMuon thietLap)
        {
            try
            {
                // Thêm thietLap vào DbSet
                _dbContext.PhatDiMuons.Add(thietLap);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                throw new Exception("Error adding ThietLap: " + ex.Message);
            }
        }
        public async Task UpdatePhat(BangPhatDiMuon updatedThietLap)
        {
            // Kiểm tra xem thietLap có tồn tại trong cơ sở dữ liệu hay không
            var existingThietLap = await _dbContext.PhatDiMuons.FirstOrDefaultAsync(tl => tl.ThangTinhCong == updatedThietLap.ThangTinhCong);
            if (existingThietLap != null)
            {
                // Cập nhật thông tin của thietLap từ dữ liệu mới
                existingThietLap.ThangTinhCong = updatedThietLap.ThangTinhCong;
                existingThietLap.SoGioTinhMuon = updatedThietLap.SoGioTinhMuon;
                existingThietLap.SoTienPhatMuon = updatedThietLap.SoTienPhatMuon;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeletePhat(string thangTinhCong)
        {
            try
            {
                // Tìm thietLap cần xóa dựa trên ThangTinhCong
                var thietLapToDelete = await _dbContext.PhatDiMuons.FirstOrDefaultAsync(tl => tl.ThangTinhCong == thangTinhCong);

                if (thietLapToDelete != null)
                {
                    // Xóa thietLap từ DbSet
                    _dbContext.PhatDiMuons.Remove(thietLapToDelete);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                throw new Exception("Lỗi khi xóa thietLap: " + ex.Message);
            }
        }
        public async Task<List<BangPhatDiMuon>> GetPhatMuonPaged(int skip, int take)
        {
            return await _dbContext.PhatDiMuons.Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> GetTotalRecordsPhatMuon()
        {
            return await _dbContext.PhatDiMuons.CountAsync();
        }






        public async Task<List<ThietLap>> GetThietLapPaged(int skip, int take)
        {
            return await _dbContext.ThietLaps.Skip(skip).Take(take).ToListAsync();
        }

        // Lấy tất cả danh sách thiết lập trong csdl
        public async Task<List<ThietLap>> GetAllThietLap()
        {
            return await _dbContext.ThietLaps.AsNoTracking().ToListAsync();
        }

        // Lấy tất cả danh sách thiết lập trong csdl dựa vào tháng tính công ( tìm kiếm )
        public async Task<List<ThietLap>> GetThietLapByThangTinhCong(string thangTinhCong)
        {
            return await _dbContext.ThietLaps
                                    .Where(tl => tl.ThangTinhCong == thangTinhCong)
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        // Thêm phương thức để thêm mới một bản ghi vào cơ sở dữ liệu
        public async Task AddThietLap(ThietLap thietLap)
        {
            try
            {
                // Thêm thietLap vào DbSet
                _dbContext.ThietLaps.Add(thietLap);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                throw new Exception("Error adding ThietLap: " + ex.Message);
            }
        }

        // Cập nhật 
        public async Task UpdateThietLap(ThietLap updatedThietLap)
        {
            // Kiểm tra xem thietLap có tồn tại trong cơ sở dữ liệu hay không
            var existingThietLap = await _dbContext.ThietLaps.FirstOrDefaultAsync(tl => tl.ThangTinhCong == updatedThietLap.ThangTinhCong);
            if (existingThietLap != null)
            {
                // Cập nhật thông tin của thietLap từ dữ liệu mới
                existingThietLap.ThangTinhCong = updatedThietLap.ThangTinhCong;
                existingThietLap.NgayBatDau = updatedThietLap.NgayBatDau;
                existingThietLap.NgayKetThuc = updatedThietLap.NgayKetThuc;
                existingThietLap.NgayCongBatBuoc = updatedThietLap.NgayCongBatBuoc;
                existingThietLap.NgayPhepToiDa = updatedThietLap.NgayPhepToiDa;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();
            }
        }

        // Xóa 
        public async Task Delete(string thangTinhCong)
        {
            try
            {
                // Tìm thietLap cần xóa dựa trên ThangTinhCong
                var thietLapToDelete = await _dbContext.ThietLaps.FirstOrDefaultAsync(tl => tl.ThangTinhCong == thangTinhCong);

                if (thietLapToDelete != null)
                {
                    // Xóa thietLap từ DbSet
                    _dbContext.ThietLaps.Remove(thietLapToDelete);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                throw new Exception("Lỗi khi xóa thietLap: " + ex.Message);
            }
        }

        // Trả về tổng số bản ghi trong csdl
        public async Task<int> GetTotalRecords()
        {
            return await _dbContext.ThietLaps.CountAsync();
        }

        // Trả về tổng số nhân viên đang làm việc 
        public async Task<int> GetTotalEmployees()
        {
            return await _dbContext.NhanViens.CountAsync(nv => nv.TrangThai == "Đang làm việc");
        }


        //Trả về tổng số phòng ban
        public async Task<int> GetTotalPhongBan()
        {
            return await _dbContext.PhongBans.CountAsync();
        }

        //Trả về tổng số nhân viên chính thức đang làm việc
        public async Task<int> GetTotalNhanVienChinhThuc()
        {
            return await _dbContext.NhanViens.CountAsync(nv => nv.TrangThai == "Đang làm việc" && nv.LoaiNhanVien == "Chính thức");
        }

        //Trả về tổng số nhân viên hợp đồng
        public async Task<int> GetTotalNhanVienHopDong()
        {
            return await _dbContext.NhanViens.CountAsync(nv => nv.TrangThai == "Đang làm việc" && nv.LoaiNhanVien == "Hợp đồng");
        }

        public async Task<Dictionary<int, int>> GetTotalEmployeesByYear()
        {
            // Lấy danh sách năm bắt đầu làm việc của tất cả nhân viên có trạng thái là "Đang làm việc"
            var startYears = await _dbContext.NhanViens
                .Where(nv => nv.TrangThai == "Đang làm việc")
                .Select(nv => nv.BatDauLamViec.Year)
                .ToListAsync();


            // Tạo một từ điển để lưu tổng số nhân viên từ năm 2017 đến năm 2024
            var employeeCountsByYear = new Dictionary<int, int>();

            // Tạo danh sách các năm từ 2017 đến 2024
            var allYears = Enumerable.Range(2017, 8); // 8 là số năm từ 2017 đến 2024

            // Tính tổng số nhân viên từ năm 2017 đến năm hiện tại
            int totalEmployees = 0;
            foreach (int year in allYears)
            {
                // Lấy số lượng nhân viên bắt đầu làm việc trong năm hiện tại
                int employeesInYear = startYears.Count(y => y == year);

                // Cập nhật tổng số nhân viên từ năm 2017 đến năm hiện tại
                totalEmployees += employeesInYear;

                // Lưu số lượng nhân viên của năm hiện tại vào từ điển
                employeeCountsByYear[year] = totalEmployees;
            }

            return employeeCountsByYear;
        }


        // Trả về số lượng nhân viên nghỉ việc theo từng năm
        public async Task<int> GetResignedEmployeesCountByYear(int year)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.TrangThai == "Đã nghỉ việc" && nv.NgayNghiViec.HasValue && nv.NgayNghiViec.Value.Year == year)
                .CountAsync();
        }


        public async Task<int> GetNewEmployeesCountByYear(int year)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.BatDauLamViec.Year == year && nv.TrangThai == "Đang làm việc")
                .CountAsync();
        }

        // Số lượng nhân viên từng phòng ban
        public async Task<(int, Dictionary<string, int>)> CountDepartmentsAndEmployees()
        {
            // Đếm số lượng phòng ban
            int departmentCount = await _dbContext.PhongBans.CountAsync();

            // Lấy danh sách phòng ban và số lượng nhân viên của mỗi phòng
            var departmentEmployeesCount = await _dbContext.PhongBans
                .Select(pb => new
                {
                    pb.MaPhongBan,
                    EmployeeCount = pb.NhanViens.Count()
                })
                .ToDictionaryAsync(pb => pb.MaPhongBan, pb => pb.EmployeeCount);

            return (departmentCount, departmentEmployeesCount);
        }

        /* HẾT CÔNG VIỆC CỦA LINH */



        // VU'S WORK : START 
        // Xuất Excel
        public byte[] ExportToExcel(DataTable dataTable)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("LuongNhanVien");
                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                // Tùy chỉnh định dạng nếu cần thiết
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
        public async Task<bool> KiemTraChamCong(string maNhanVien, DateTime ngayChamCong)
        {
            // Kiểm tra nhân viên đã được nhập lương chưa
            var chamCong = await _dbContext.ChamCongs.FirstOrDefaultAsync(cc => cc.MaNV == maNhanVien && cc.NgayChamCong == ngayChamCong);
            return chamCong != null;
        }

        public async Task<bool> KiemTraMaNhanVienCoLuongThangCuThe(string maNhanVien, string thangLinhLuong)
        {
            // Kiểm tra nhân viên đã được nhập lương chưa
            var nhanVienCoLuong = await _dbContext.Luongs.FirstOrDefaultAsync(l => l.MaNV == maNhanVien && l.ThangLinhLuong == thangLinhLuong);
            return nhanVienCoLuong != null;
        }

        public async Task<bool> KiemTraMaNhanVienTonTai(string maNhanVien)
        {
            // Kiểm tra xem mã nhân viên có tồn tại trong cơ sở dữ liệu không
            var nhanVien = await _dbContext.NhanViens.FirstOrDefaultAsync(nv => nv.MaNV == maNhanVien);
            return nhanVien != null;
        }

        public async Task<bool> KiemTraMaNhanVienCoLuong(string maNhanVien)
        {
            // Kiểm tra nhân viên đã được nhập lương chưa
            var nhanVienCoLuong = await _dbContext.Luongs.FirstOrDefaultAsync(l => l.MaNV == maNhanVien);
            return nhanVienCoLuong != null;
        }

        // THÁNG TÍNH CÔNG: START
        public class ThangTinhCongDuocThiepLap { public string ThangTinhCongThietLap { get; set; } }

        public async Task<List<ThangTinhCongDuocThiepLap>> GetThangTinhCong()
        {
            // Thực hiện truy vấn LINQ để lấy danh sách ThangTinhCong từ cơ sở dữ liệu
            var thangTinhCongThietLap = await _dbContext.ThietLaps
                                                .Select(tl => new ThangTinhCongDuocThiepLap { ThangTinhCongThietLap = tl.ThangTinhCong })
                                                .ToListAsync();

            return thangTinhCongThietLap;
        }

        public async Task<List<ThangTinhCongDuocThiepLap>> GetThangTinhCongPaged(int skip, int take)
        {
            // Thực hiện truy vấn LINQ để lấy danh sách ThangTinhCong từ cơ sở dữ liệu
            var thangTinhCongThietLap = await _dbContext.ThietLaps
                                                .Select(tl => new ThangTinhCongDuocThiepLap { ThangTinhCongThietLap = tl.ThangTinhCong })
                                                .Skip(skip).Take(take).ToListAsync();

            return thangTinhCongThietLap;
        }
        public async Task<int> GetTotalRecordsThangTinhCong()
        {
            return await _dbContext.ThietLaps.Select(tl => new ThangTinhCongDuocThiepLap { ThangTinhCongThietLap = tl.ThangTinhCong }).CountAsync();
        }



        // THÁNG TÍNH CÔNG: END

        // Thưởng - phạt: START
        // Hiển thị danh sách thưởng/phạt
        public class ThuongPhatWithNhanVienInfo
        {
            public int MaTP { get; set; }
            public string MaNV { get; set; }
            public string Loai { get; set; }
            public string NguonThuongPhat { get; set; }
            public DateTime Ngay { get; set; }
            public decimal SoTien { get; set; }
            public string HoTen { get; set; }
        }

        public async Task<List<ThuongPhatWithNhanVienInfo>> GetThuong()
        {
            var thuongWithEmployeeInfo = await _dbContext.ThuongPhats
                .Join(_dbContext.NhanViens,
                    tp => tp.MaNV,
                    nv => nv.MaNV,
                    (tp, nv) => new ThuongPhatWithNhanVienInfo
                    {
                        MaTP = tp.MaThuongPhat,
                        MaNV = tp.MaNV,
                        HoTen = nv.HoTen,
                        Ngay = tp.Ngay,
                        NguonThuongPhat = tp.NguonThuongPhat,
                        SoTien = tp.SoTien,
                        Loai = tp.Loai // Thêm cột Loai vào kết quả
                    })
                .Where(tp => tp.Loai == "Thưởng") // Lọc theo Loai = "Thưởng"
                .ToListAsync();

            return thuongWithEmployeeInfo;
        }

        public async Task<List<ThuongPhatWithNhanVienInfo>> GetPhat()
        {
            var phatWithNhanVienInfo = await _dbContext.ThuongPhats
                .Where(tp => tp.Loai == "Phạt") // Lọc theo Loai = "Phạt"
                .Join(_dbContext.NhanViens,
                    tp => tp.MaNV,
                    nv => nv.MaNV,
                    (tp, nv) => new ThuongPhatWithNhanVienInfo
                    {
                        MaTP = tp.MaThuongPhat,
                        MaNV = tp.MaNV,
                        HoTen = nv.HoTen,
                        Ngay = tp.Ngay,
                        NguonThuongPhat = tp.NguonThuongPhat,
                        SoTien = tp.SoTien
                    })
                .ToListAsync();

            return phatWithNhanVienInfo;
        }

        // Thêm thưởng/phạt
        public async Task AddThuongPhat(string maNhanVien, DateTime ngay, string nguon, decimal soTien, string loai)
        {
            // Tạo một đối tượng mới ThuongPhat
            ThuongPhat newThuongPhat = new ThuongPhat
            {
                MaNV = maNhanVien,
                Ngay = ngay,
                NguonThuongPhat = nguon,
                SoTien = soTien,
                Loai = loai
            };

            // Thêm đối tượng mới vào DbSet và lưu thay đổi vào cơ sở dữ liệu
            _dbContext.ThuongPhats.Add(newThuongPhat);
            await _dbContext.SaveChangesAsync();
        }

        // Xóa thưởng/phạt

        public async Task XoaThuongPhat(int maThuongPhat)
        {
            var thuongPhat = await _dbContext.ThuongPhats.FindAsync(maThuongPhat);
            if (thuongPhat != null)
            {
                _dbContext.ThuongPhats.Remove(thuongPhat);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Cập nhật
        public async Task CapNhatThuongPhat(int maTP, string maNhanVien, DateTime ngay, string nguon, decimal soTien, string loai)
        {
            var thuongphat = await _dbContext.ThuongPhats.FirstOrDefaultAsync(t => t.MaThuongPhat == maTP);
            if (thuongphat != null)
            {
                thuongphat.MaNV = maNhanVien;
                thuongphat.Ngay = ngay;
                thuongphat.NguonThuongPhat = nguon;
                thuongphat.SoTien = soTien;
                thuongphat.Loai = loai;
                await _dbContext.SaveChangesAsync();
            }
        }

        // giới hạn bản ghi bảng thưởng/phạt
        public async Task<List<ThuongPhatWithNhanVienInfo>> GetThuongPaged(int skip, int take)
        {
            var thuongPhatWithEmployeeInfo = await _dbContext.ThuongPhats
            .Join(_dbContext.NhanViens,
                tp => tp.MaNV,
                nv => nv.MaNV,
                (tp, nv) => new ThuongPhatWithNhanVienInfo
                {
                    MaTP = tp.MaThuongPhat,
                    MaNV = tp.MaNV,
                    HoTen = nv.HoTen,
                    Ngay = tp.Ngay,
                    NguonThuongPhat = tp.NguonThuongPhat,
                    SoTien = tp.SoTien,
                    Loai = tp.Loai // Thêm cột Loai vào kết quả
                })
            .Where(tp => tp.Loai == "Thưởng") // Lọc theo Loai = "Thưởng"
            .Skip(skip).Take(take).ToListAsync();

            return thuongPhatWithEmployeeInfo;
        }

        public async Task<int> GetTotalRecordsThuong()
        {
            return await _dbContext.ThuongPhats.Where(tp => tp.Loai == "Thưởng").CountAsync();
        }

        public async Task<List<ThuongPhatWithNhanVienInfo>> GetPhatPaged(int skip, int take)
        {
            var thuongPhatWithEmployeeInfo = await _dbContext.ThuongPhats
            .Join(_dbContext.NhanViens,
                tp => tp.MaNV,
                nv => nv.MaNV,
                (tp, nv) => new ThuongPhatWithNhanVienInfo
                {
                    MaTP = tp.MaThuongPhat,
                    MaNV = tp.MaNV,
                    HoTen = nv.HoTen,
                    Ngay = tp.Ngay,
                    NguonThuongPhat = tp.NguonThuongPhat,
                    SoTien = tp.SoTien,
                    Loai = tp.Loai // Thêm cột Loai vào kết quả
                })
            .Where(tp => tp.Loai == "Phạt") // Lọc theo Loai = "Thưởng"
            .Skip(skip).Take(take).ToListAsync();

            return thuongPhatWithEmployeeInfo;
        }

        public async Task<int> GetTotalRecordsPhat()
        {
            return await _dbContext.ThuongPhats.Where(tp => tp.Loai == "Phạt").CountAsync();
        }

        // Thưởng - phạt: END


        // Lương: START
        // Hiển thị danh sách lương
        public class LuongNhanVien
        {
            public string MaNV { get; set; }
            public string HoTen { get; set; }
            public string ChucVu { get; set; }
            public string TenPhongBan { get; set; }
            public string TenChiNhanh { get; set; }
            public string ThangLinhLuong { get; set; }
            public decimal LuongCung { get; set; }
            public decimal TongTienThuong { get; set; }
            public decimal TongTienPhat { get; set; }
            public decimal Thue { get; set; }
            public decimal ThucNhan { get; set; }
        }

        public async Task<List<LuongNhanVien>> GetLuong()
        {
            try
            {
                var query = from nv in _dbContext.NhanViens
                            join pb in _dbContext.PhongBans on nv.MaPhongBan equals pb.MaPhongBan
                            join l in _dbContext.Luongs on nv.MaNV equals l.MaNV
                            into luongGroup
                            from l in luongGroup.DefaultIfEmpty()
                            join tp in _dbContext.ThuongPhats on nv.MaNV equals tp.MaNV
                            into thuongPhatGroup
                            from tp in thuongPhatGroup.DefaultIfEmpty()
                            where _dbContext.Luongs.Select(x => x.MaNV).Contains(nv.MaNV)
                            group new { nv, pb, l, tp } by new { nv.MaNV, nv.HoTen, nv.ChucVu, pb.TenPhongBan,  l.ThangLinhLuong, l.LuongCung, l.Thue } into g
                            select new LuongNhanVien
                            {
                                MaNV = g.Key.MaNV,
                                HoTen = g.Key.HoTen,
                                ChucVu = g.Key.ChucVu,
                                TenPhongBan = g.Key.TenPhongBan,
                                ThangLinhLuong = g.Key.ThangLinhLuong,
                                LuongCung = g.Key.LuongCung,
                                TongTienThuong = g.Sum(x => x.tp != null && x.tp.Loai == "Thưởng" ? x.tp.SoTien : 0),
                                TongTienPhat = g.Sum(x => x.tp != null && x.tp.Loai == "Phạt" ? x.tp.SoTien : 0),
                                Thue = g.Key.Thue,
                                ThucNhan = (g.Key.LuongCung) + g.Sum(x => x.tp != null && x.tp.Loai == "Thưởng" ? x.tp.SoTien : 0)
                                                 - g.Sum(x => x.tp != null && x.tp.Loai == "Phạt" ? x.tp.SoTien : 0)
                                                 - (g.Key.Thue)
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Nhập lương
        public async Task AddLuong(string maNhanVien, string thangLinhLuong, decimal luongCung, decimal thue)
        {
            // Tạo một đối tượng mới ThuongPhat
            Luong newLuong = new Luong
            {
                MaNV = maNhanVien,
                ThangLinhLuong = thangLinhLuong,
                LuongCung = luongCung,
                Thue = thue
            };

            // Thêm đối tượng mới vào DbSet và lưu thay đổi vào cơ sở dữ liệu
            _dbContext.Luongs.Add(newLuong);

            await _dbContext.SaveChangesAsync();
        }

        // Cập nhật lương
        public async Task CapNhatLuong(string maNhanVien, string thangLinhLuong, decimal luongCung, decimal thue)
        {
            var luog = await _dbContext.Luongs.FirstOrDefaultAsync(l => l.MaNV == maNhanVien && l.ThangLinhLuong == thangLinhLuong);
            if (luog != null)
            {
                luog.MaNV = maNhanVien;
                luog.ThangLinhLuong = thangLinhLuong;
                luog.LuongCung = luongCung;
                luog.Thue = thue;

                await _dbContext.SaveChangesAsync();
            }
        }

        // giới hạn bản ghi bảng lương
        public async Task<List<LuongNhanVien>> GetLuongPaged(int skip, int take)
        {
            var query = from nv in _dbContext.NhanViens
                        join pb in _dbContext.PhongBans on nv.MaPhongBan equals pb.MaPhongBan
                        join l in _dbContext.Luongs on nv.MaNV equals l.MaNV
                        into luongGroup
                        from l in luongGroup.DefaultIfEmpty()
                        join tp in _dbContext.ThuongPhats on nv.MaNV equals tp.MaNV
                        into thuongPhatGroup
                        from tp in thuongPhatGroup.DefaultIfEmpty()
                        where _dbContext.Luongs.Select(x => x.MaNV).Contains(nv.MaNV)
                        group new { nv, pb, l, tp } by new { nv.MaNV, nv.HoTen, nv.ChucVu, pb.TenPhongBan, l.ThangLinhLuong, l.LuongCung, l.Thue } into g
                        select new LuongNhanVien
                        {
                            MaNV = g.Key.MaNV,
                            HoTen = g.Key.HoTen,
                            ChucVu = g.Key.ChucVu,
                            TenPhongBan = g.Key.TenPhongBan,
                            ThangLinhLuong = g.Key.ThangLinhLuong,
                            LuongCung = g.Key.LuongCung,
                            TongTienThuong = g.Sum(x => x.tp != null && x.tp.Loai == "Thưởng" ? x.tp.SoTien : 0),
                            TongTienPhat = g.Sum(x => x.tp != null && x.tp.Loai == "Phạt" ? x.tp.SoTien : 0),
                            Thue = g.Key.Thue,
                            ThucNhan = (g.Key.LuongCung) + g.Sum(x => x.tp != null && x.tp.Loai == "Thưởng" ? x.tp.SoTien : 0)
                                             - g.Sum(x => x.tp != null && x.tp.Loai == "Phạt" ? x.tp.SoTien : 0)
                                             - (g.Key.Thue)
                        };
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetTotalRecordsLuong()
        {
            return await _dbContext.Luongs.CountAsync();
        }


        // Lương: END

        // VU'S WORK : END


        // CÔNG VIỆC CỦA MINH
        public class NhanVienInfo
        {
            public string MaNV { get; set; }
            public string HoTen { get; set; }
            public string CCCD { get; set; }
            public DateTime NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string SoDienThoai { get; set; }
            public string MaBaoHiem { get; set; }
            public string DiaChiThuongChu { get; set; }
            public string DiaChiTamChu { get; set; }
            public string TrinhDoHocVan { get; set; }
            public string TenNganHang { get; set; }
            public string STKNganHang { get; set; }
            public string MaSoThue { get; set; }
            public string LoaiNhanVien { get; set; }
            public string ChucVu { get; set; }
            public string PhongBan { get; set; }
            public string ChiNhanh { get; set; }
            public string YeuCauChamCong { get; set; }
            public DateTime NgayBatDauLam { get; set; }
            public DateTime NgayChinhThucLam { get; set; }
            public DateTime? NgayNghiViec { get; set; }
            public string MailLamViec { get; set; }
            public string TrangThai { get; set; }

        }

        public async Task<NhanVienInfo> GetEmployeeInfo(string maNV)
        {
            var employeeInfo = from nv in _dbContext.NhanViens
                               join pb in _dbContext.PhongBans on nv.MaPhongBan equals pb.MaPhongBan
                               where nv.MaNV == maNV
                               select new NhanVienInfo
                               {
                                   MaNV = nv.MaNV,
                                   HoTen = nv.HoTen,
                                   CCCD = nv.CCCD,
                                   NgaySinh = nv.NgaySinh,
                                   GioiTinh = nv.GioiTinh,
                                   SoDienThoai = nv.SoDienThoai,
                                   MaBaoHiem = nv.MaBaoHiem,
                                   DiaChiThuongChu = nv.DiaChiThuongChu,
                                   DiaChiTamChu = nv.DiaChiTamChu,
                                   TrinhDoHocVan = nv.TrinhDoHocVan,
                                   TenNganHang = nv.TenNganHang,
                                   STKNganHang = nv.STKNganHang,
                                   MaSoThue = nv.MaSoThue,
                                   LoaiNhanVien = nv.LoaiNhanVien,
                                   ChucVu = nv.ChucVu,
                                   YeuCauChamCong = nv.YeuCauChamCong,
                                   NgayBatDauLam = nv.BatDauLamViec,
                                   NgayChinhThucLam = nv.ChinhThucLamViec,
                                   NgayNghiViec = nv.NgayNghiViec,
                                   MailLamViec = nv.MailLamViec,
                                   TrangThai = nv.TrangThai,
                                   PhongBan = pb.TenPhongBan,
                               };

            return await employeeInfo.FirstOrDefaultAsync();
        }

        public async Task<string> LayMaPhongBanTuTen(string tenPhongBan)
        {
            try
            {
                // Sử dụng LINQ để truy vấn cơ sở dữ liệu và lấy ra mã phòng ban tương ứng với tên
                var maPhongBan = await _dbContext.PhongBans
                    .Where(pb => pb.TenPhongBan == tenPhongBan)
                    .Select(pb => pb.MaPhongBan)
                    .FirstOrDefaultAsync();

                // Kiểm tra nếu maPhongBan là null
                if (maPhongBan == null)
                {
                    throw new Exception("Không tìm thấy mã phòng ban cho tên phòng ban đã nhập.");
                }

                return maPhongBan;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ như ghi log hoặc thông báo cho người dùng
                Console.WriteLine("Đã xảy ra một ngoại lệ: " + ex.Message);
                return null; // Hoặc trả về một giá trị mặc định khác nếu bạn muốn
            }
        }


        public async Task<string> LayTenPhongBanTuMa(string maPhongBan)
        {
            // Sử dụng LINQ để truy vấn cơ sở dữ liệu và lấy ra mã phòng ban tương ứng với tên
            var tenPhongBan = await _dbContext.PhongBans
                .Where(pb => pb.MaPhongBan == maPhongBan)
                .Select(pb => pb.TenPhongBan)
                .FirstOrDefaultAsync();

            return maPhongBan;
        }

        public async Task ThemMoiNhanVienAsync(NhanVien nhanVien)
        {
            _dbContext.NhanViens.Add(nhanVien);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<string>> LoadDanhSachPhongBan()
        {
            return await _dbContext.PhongBans.Select(pb => pb.TenPhongBan).ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienPhongBanInfo()
        {
            try
            {
                var nhanVienPhongBanInfo = await _dbContext.NhanViens
                    .Join(_dbContext.PhongBans,
                        nv => nv.MaPhongBan,
                        pb => pb.MaPhongBan,
                        (nv, pb) => new
                        {
                            NhanVien = nv,
                            PhongBan = pb
                        })
                    .Select(item => new NhanVienInfo
                    {
                        MaNV = item.NhanVien.MaNV,
                        HoTen = item.NhanVien.HoTen,
                        ChucVu = item.NhanVien.ChucVu,
                        PhongBan = item.PhongBan.TenPhongBan,
                        MailLamViec = item.NhanVien.MailLamViec,
                        TrangThai = item.NhanVien.TrangThai
                    })
                    .ToListAsync();

                return nhanVienPhongBanInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching NhanVienPhongBanInfo: " + ex.Message);
            }
        }
        public async Task<int> GetTotalRecordsNhanVien()
        {
            return await _dbContext.NhanViens.CountAsync();
        }
        public async Task<List<NhanVienInfo>> GetNhanVienPaged(int skip, int take)
        {
            return await _dbContext.NhanViens
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                        (nv, pb) => new
                        {
                            NhanVien = nv,
                            PhongBan = pb
                        })
                .Select(item => new NhanVienInfo
                {
                    MaNV = item.NhanVien.MaNV,
                    HoTen = item.NhanVien.HoTen,
                    ChucVu = item.NhanVien.ChucVu,
                    PhongBan = item.PhongBan.TenPhongBan,
                    MailLamViec = item.NhanVien.MailLamViec,
                    TrangThai = item.NhanVien.TrangThai
                })
                .OrderBy(nv => nv.MaNV)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByMaNV(string maNV)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.MaNV == maNV)
                .Join(_dbContext.PhongBans,
                        nv => nv.MaPhongBan,
                        pb => pb.MaPhongBan,
                            (nv, pb) => new
                            {
                                NhanVien = nv,
                                PhongBan = pb
                            })
                .Select(item => new NhanVienInfo
                {
                    MaNV = item.NhanVien.MaNV,
                    HoTen = item.NhanVien.HoTen,
                    ChucVu = item.NhanVien.ChucVu,
                    PhongBan = item.PhongBan.TenPhongBan,
                    MailLamViec = item.NhanVien.MailLamViec,
                    TrangThai = item.NhanVien.TrangThai
                })
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByHoTen(string hoTen)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.HoTen.Contains(hoTen))
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                    (nv, pb) => new NhanVienInfo
                    {
                        MaNV = nv.MaNV,
                        HoTen = nv.HoTen,
                        ChucVu = nv.ChucVu,
                        PhongBan = pb.TenPhongBan,
                        MailLamViec = nv.MailLamViec,
                        TrangThai = nv.TrangThai
                    })
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByChucVu(string chucVu)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.ChucVu.Contains(chucVu))
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                    (nv, pb) => new NhanVienInfo
                    {
                        MaNV = nv.MaNV,
                        HoTen = nv.HoTen,
                        ChucVu = nv.ChucVu,
                        PhongBan = pb.TenPhongBan,
                        MailLamViec = nv.MailLamViec,
                        TrangThai = nv.TrangThai
                    })
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByPhongBan(string phongBan)
        {
            return await _dbContext.NhanViens
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                    (nv, pb) => new { NhanVien = nv, PhongBan = pb })
                .Where(item => item.PhongBan.TenPhongBan.Contains(phongBan))
                .Select(item => new NhanVienInfo
                {
                    MaNV = item.NhanVien.MaNV,
                    HoTen = item.NhanVien.HoTen,
                    ChucVu = item.NhanVien.ChucVu,
                    PhongBan = item.PhongBan.TenPhongBan,
                    MailLamViec = item.NhanVien.MailLamViec,
                    TrangThai = item.NhanVien.TrangThai
                })
                .ToListAsync();
        }

        public async Task<List<NhanVienInfo>> GetNhanVienByTrangThai(string trangThai)
        {
            return await _dbContext.NhanViens
                .Where(nv => nv.TrangThai.Contains(trangThai))
                .Join(_dbContext.PhongBans,
                    nv => nv.MaPhongBan,
                    pb => pb.MaPhongBan,
                    (nv, pb) => new NhanVienInfo
                    {
                        MaNV = nv.MaNV,
                        HoTen = nv.HoTen,
                        ChucVu = nv.ChucVu,
                        PhongBan = pb.TenPhongBan,
                        MailLamViec = nv.MailLamViec,
                        TrangThai = nv.TrangThai
                    })
                .ToListAsync();
        }


        public async Task DeleteNhanVien(string MaNV)
        {
            try
            {
                // Tìm thietLap cần xóa dựa trên ThangTinhCong
                var nhanvienToDelete = await _dbContext.NhanViens.FirstOrDefaultAsync(tl => tl.MaNV == MaNV);

                if (nhanvienToDelete != null)
                {
                    // Xóa thietLap từ DbSet
                    _dbContext.NhanViens.Remove(nhanvienToDelete);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                throw new Exception("Lỗi khi xóa nhan vien: " + ex.Message);
            }
        }

        public async Task CapNhatThongTinNhanVienAsync(NhanVien nhanVien)
        {
            try
            {
                // Log trước khi tìm kiếm nhân viên
                Console.WriteLine($"Đang tìm kiếm nhân viên với mã: {nhanVien.MaNV}");
                var existingEmployee = await _dbContext.NhanViens.FirstOrDefaultAsync(e => e.MaNV == nhanVien.MaNV);

                if (existingEmployee != null)
                {
                    // Log trước khi cập nhật thông tin
                    Console.WriteLine($"Đang cập nhật thông tin cho nhân viên: {existingEmployee.MaNV}");

                    existingEmployee.HoTen = nhanVien.HoTen;
                    existingEmployee.CCCD = nhanVien.CCCD;
                    existingEmployee.NgaySinh = nhanVien.NgaySinh;
                    existingEmployee.GioiTinh = nhanVien.GioiTinh;
                    existingEmployee.SoDienThoai = nhanVien.SoDienThoai;
                    existingEmployee.MaBaoHiem = nhanVien.MaBaoHiem;
                    existingEmployee.DiaChiThuongChu = nhanVien.DiaChiThuongChu;
                    existingEmployee.DiaChiTamChu = nhanVien.DiaChiTamChu;
                    existingEmployee.TrinhDoHocVan = nhanVien.TrinhDoHocVan;
                    existingEmployee.TenNganHang = nhanVien.TenNganHang;
                    existingEmployee.STKNganHang = nhanVien.STKNganHang;
                    existingEmployee.MaSoThue = nhanVien.MaSoThue;
                    existingEmployee.LoaiNhanVien = nhanVien.LoaiNhanVien;
                    existingEmployee.ChucVu = nhanVien.ChucVu;
                    existingEmployee.MaPhongBan = nhanVien.MaPhongBan;
                    existingEmployee.YeuCauChamCong = nhanVien.YeuCauChamCong;
                    existingEmployee.BatDauLamViec = nhanVien.BatDauLamViec;
                    existingEmployee.ChinhThucLamViec = nhanVien.ChinhThucLamViec;
                    existingEmployee.NgayNghiViec = nhanVien.NgayNghiViec;
                    existingEmployee.MailLamViec = nhanVien.MailLamViec;
                    existingEmployee.TrangThai = nhanVien.TrangThai;

                    _dbContext.NhanViens.Update(existingEmployee);
                    await _dbContext.SaveChangesAsync();

                    // Log sau khi cập nhật thành công
                    Console.WriteLine($"Cập nhật thông tin nhân viên thành công: {existingEmployee.MaNV}");
                }
                else
                {
                    // Log khi không tìm thấy nhân viên
                    Console.WriteLine($"Không tìm thấy nhân viên với mã: {nhanVien.MaNV}");
                    throw new Exception($"Không tìm thấy nhân viên với mã: {nhanVien.MaNV}");
                }
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết
                Console.WriteLine($"Lỗi khi cập nhật thông tin nhân viên: {ex.Message}");
                throw new Exception("Lỗi khi cập nhật thông tin nhân viên: " + ex.Message);
            }
        }



        //public async Task CapNhatThongTinNhanVienAsync(NhanVien nhanVien)
        //{
        //    try
        //    {
        //        var existingEmployee = await _dbContext.NhanViens.FirstOrDefaultAsync(e => e.MaNV == nhanVien.MaNV);

        //        if (existingEmployee != null)
        //        {
        //            existingEmployee.HoTen = nhanVien.HoTen;
        //            existingEmployee.CCCD = nhanVien.CCCD;
        //            existingEmployee.NgaySinh = nhanVien.NgaySinh;
        //            existingEmployee.GioiTinh = nhanVien.GioiTinh;
        //            existingEmployee.SoDienThoai = nhanVien.SoDienThoai;
        //            existingEmployee.MaBaoHiem = nhanVien.MaBaoHiem;
        //            existingEmployee.DiaChiThuongChu = nhanVien.DiaChiThuongChu;
        //            existingEmployee.DiaChiTamChu = nhanVien.DiaChiTamChu;
        //            existingEmployee.TrinhDoHocVan = nhanVien.TrinhDoHocVan;
        //            existingEmployee.TenNganHang = nhanVien.TenNganHang;
        //            existingEmployee.STKNganHang = nhanVien.STKNganHang;
        //            existingEmployee.MaSoThue = nhanVien.MaSoThue;
        //            existingEmployee.LoaiNhanVien = nhanVien.LoaiNhanVien;
        //            existingEmployee.ChucVu = nhanVien.ChucVu;
        //            existingEmployee.MaPhongBan = nhanVien.MaPhongBan;
        //            existingEmployee.YeuCauChamCong = nhanVien.YeuCauChamCong;
        //            existingEmployee.BatDauLamViec = nhanVien.BatDauLamViec;
        //            existingEmployee.ChinhThucLamViec = nhanVien.ChinhThucLamViec;
        //            existingEmployee.NgayNghiViec = nhanVien.NgayNghiViec;
        //            existingEmployee.MailLamViec = nhanVien.MailLamViec;
        //            existingEmployee.TrangThai = nhanVien.TrangThai;

        //            _dbContext.NhanViens.Update(existingEmployee);
        //            await _dbContext.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            throw new Exception($"Không tìm thấy nhân viên với mã: {nhanVien.MaNV}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Lỗi khi cập nhật thông tin nhân viên: " + ex.Message);
        //    }
        //}

    }
}
