using FullProject.Data;
using FullProject.Model;
using Microsoft.EntityFrameworkCore;

namespace FullProject.Services
{
    public class ChamCongService
    {
        ChamCongDbContext dbContext = new ChamCongDbContext();
        private readonly ChamCongDbContext _dbContext;
        public ChamCongService()
        {
            _dbContext = dbContext;
        }
        public async Task<List<NhanVien>> GetAllNhanViens()
        {
            try
            {
                return await _dbContext.NhanViens
                    .Select(nv => new NhanVien
                    {
                        MaNV = nv.MaNV ?? string.Empty,
                        HoTen = nv.HoTen ?? string.Empty,
                        // Thêm các trường khác nếu cần, xử lý NULL tương ứng
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi
                throw new Exception("Đã xảy ra lỗi khi lấy danh sách nhân viên.", ex);
            }
        }
        public async Task<List<ChamCong>> GetAllChamCongs()
        {
            return await dbContext.ChamCongs.AsNoTracking().ToListAsync();
        }
        public async Task<List<PhongBan>> GetAllPhongBan()
        {
            return await dbContext.PhongBans.AsNoTracking().ToListAsync();
        }
        public async Task<int> GetTotalRecords()
        {
            return await _dbContext.ChamCongs.CountAsync();
        }

        public async Task<List<ChamCong>> GetChamCongPaged(int skip, int take)
        {
            return await _dbContext.ChamCongs.Skip(skip).Take(take).ToListAsync();
        }
    }
}
