using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using TimekeepingMVC.Models;
using TimekeepingMVC.Services;

namespace TimekeepingMVC.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IMongoCollection<PhieuChamCong> _phieuChamCongCollection;

        public SummaryService(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _phieuChamCongCollection = database.GetCollection<PhieuChamCong>(settings.Value.CollectionName);
        }

        public async Task<List<PhieuChamCongTongHop>> GetSummaryAsync()
        {
            var records = await _phieuChamCongCollection.Find(_ => true).ToListAsync();

            for (int i = 0; i < records.Count; i++)
            {
                records[i].STT = i + 1;
            }

            var summary = records
                .GroupBy(r => new
                {
                    r.STT,
                    Tuan = ISOWeek.GetWeekOfYear(r.Ngay),
                    Thang = r.Ngay.Month
                })
                .Select(g => new PhieuChamCongTongHop
                {
                    STT = g.Key.STT,
                    Tuan = g.Key.Tuan,
                    Thang = g.Key.Thang,
                    TongGioLam = g.Sum(x => x.TongGioLam)
                })
                .OrderBy(x => x.STT)
                .ThenBy(x => x.Thang)
                .ThenBy(x => x.Tuan)
                .ToList();

            return summary;
        }
    }
}
