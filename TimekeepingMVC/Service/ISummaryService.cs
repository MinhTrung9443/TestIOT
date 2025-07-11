using System.Collections.Generic;
using System.Threading.Tasks;
using TimekeepingMVC.Models;

namespace TimekeepingMVC.Services
{
    public interface ISummaryService
    {
        Task<List<PhieuChamCongTongHop>> GetSummaryAsync();
    }
}
