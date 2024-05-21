using FullProject.Context;
using FullProject.Data;
using FullProject.Model;
using Microsoft.EntityFrameworkCore;

namespace FullProject.Repo
{
    public class AccountRepository
    {
        private readonly ThietLapContext _context;

        public AccountRepository(ThietLapContext context)  
        {
            _context = context;
        }

        public async Task<bool> TenTaiKhoanExistsAsync(string tenTaiKhoan)
        {
            // Replace this with your actual database query logic
            return await _context.TaiKhoans.AnyAsync(tk => tk.TenTaiKhoan == tenTaiKhoan);
        }

        public async Task<TaiKhoan> GetAccountByUsernameAsync(string username)
        {
            return await _context.TaiKhoans.SingleOrDefaultAsync(a => a.TenTaiKhoan == username);
        }

        public async Task RegisterAccountAsync(TaiKhoan account)
        {
            _context.TaiKhoans.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ManvExistsAsync(string manv)
        {
            bool exists = await _context.NhanViens.AnyAsync(nv => nv.MaNV == manv);
            return exists;
        }

        public async Task<List<TaiKhoan>> GetAllTaiKhoanAsync()
        {
            return await _context.TaiKhoans.ToListAsync();
        }

        public async Task DeleteAccountAsync(string maNV)
        {
            // Find the account by MaNV
            var accountToDelete = await _context.TaiKhoans.FirstOrDefaultAsync(a => a.MaNV == maNV);
            if (accountToDelete != null)
            {
                // Remove the account from the DbSet
                _context.TaiKhoans.Remove(accountToDelete);
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAccountAsync(TaiKhoan updatedAccount)
        {
            // Find the existing account in the database
            var existingAccount = await _context.TaiKhoans.FirstOrDefaultAsync(a => a.TenTaiKhoan == updatedAccount.TenTaiKhoan);

            if (existingAccount != null)
            {
                // Update the properties of the existing account
                existingAccount.MatKhau = updatedAccount.MatKhau;
                existingAccount.MaNV = updatedAccount.MaNV;
                existingAccount.Role = updatedAccount.Role;

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
        }
    }

}
