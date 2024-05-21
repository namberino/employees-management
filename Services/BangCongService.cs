using FullProject.Data;
using FullProject.Model;
using FullProject.Pages;
using Microsoft.EntityFrameworkCore;

namespace FullProject.Services
{
    public class BangCongService
    {
        BangCongContext dbContext = new BangCongContext();
        private readonly BangCongContext _dbContext;
        public BangCongService()
        {
            _dbContext = dbContext;
        }
        public async Task<List<NhanVien>> GetAllNhanViens()
        {
            return await _dbContext.NhanViens
                             .Select(nv => new NhanVien
                             {
                                 MaNV = nv.MaNV ?? string.Empty,
                                 HoTen = nv.HoTen ?? string.Empty,
                                 ChucVu = nv.ChucVu ?? string.Empty,
                                 MaPhongBan = nv.MaPhongBan ?? string.Empty
                             })
                             .ToListAsync();
        }
        public async Task<List<BangCong>> GetAllBangCongs()
        {
            return await _dbContext.BangCongs.AsNoTracking().ToListAsync();
        }
        public async Task<List<ChamCong>> GetAllChamCongs()
        {
            return await _dbContext.ChamCongs.AsNoTracking().ToListAsync();
        }
        public async Task<List<PhongBan>> GetAllPhongBans()
        {
            return await _dbContext.PhongBans.AsNoTracking().ToListAsync();
        }
        public async Task<List<ThietLap>> GetAllThietLaps()
        {
            return await _dbContext.ThietLaps.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetTotalRecords()
        {
            return await _dbContext.NhanViens.CountAsync();
        }

        public async Task<List<NhanVien>> GetBangCongPaged(int skip, int take)
        {
            return await _dbContext.NhanViens
                                   .Skip(skip)
                                   .Take(take)
                                   .Select(nv => new NhanVien
                                   {
                                       MaNV = nv.MaNV ?? string.Empty,
                                       HoTen = nv.HoTen ?? string.Empty,
                                       ChucVu = nv.ChucVu ?? string.Empty,
                                       MaPhongBan = nv.MaPhongBan ?? string.Empty
                                   })
                                   .ToListAsync();
        }

    }
}
