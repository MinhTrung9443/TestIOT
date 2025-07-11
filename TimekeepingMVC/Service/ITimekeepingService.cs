using TimekeepingMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimekeepingMVC.Services
{
    public interface ITimekeepingService
    {
        Task<List<PhieuChamCong>> GetAllAsync();
        Task<PhieuChamCong> GetByIdAsync(string id);
        Task CreateAsync(PhieuChamCong phieuChamCong);
        Task UpdateAsync(string id, PhieuChamCong phieuChamCong);
        Task DeleteAsync(string id);
    }
}