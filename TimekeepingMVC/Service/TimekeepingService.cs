using MongoDB.Driver;
using TimekeepingMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System;

namespace TimekeepingMVC.Services
{
    public class TimekeepingService : ITimekeepingService
    {
        private readonly IMongoCollection<PhieuChamCong> _phieuChamCongCollection;

        public TimekeepingService(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _phieuChamCongCollection = database.GetCollection<PhieuChamCong>(settings.Value.CollectionName);
        }

        public async Task<List<PhieuChamCong>> GetAllAsync()
        {
            var records = await _phieuChamCongCollection.Find(_ => true).ToListAsync();
            for (int i = 0; i < records.Count; i++)
            {
                records[i].STT = i + 1;
            }
            return records;
        }

        public async Task<PhieuChamCong> GetByIdAsync(string id)
        {
            return await _phieuChamCongCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(PhieuChamCong phieuChamCong)
        {
            var timeIn = TimeSpan.Parse(phieuChamCong.TDVao);
            var timeOut = TimeSpan.Parse(phieuChamCong.TDRa);
            phieuChamCong.TongGioLam = (timeOut - timeIn).TotalHours;
            await _phieuChamCongCollection.InsertOneAsync(phieuChamCong);
        }

        public async Task UpdateAsync(string id, PhieuChamCong phieuChamCong)
        {
            var timeIn = TimeSpan.Parse(phieuChamCong.TDVao);
            var timeOut = TimeSpan.Parse(phieuChamCong.TDRa);
            phieuChamCong.TongGioLam = (timeOut - timeIn).TotalHours;
            await _phieuChamCongCollection.ReplaceOneAsync(x => x.Id == id, phieuChamCong);
        }

        public async Task DeleteAsync(string id)
        {
            await _phieuChamCongCollection.DeleteOneAsync(x => x.Id == id);
            var records = await GetAllAsync(); 
            for (int i = 0; i < records.Count; i++)
            {
                var updatedRecord = new PhieuChamCong
                {
                    Id = records[i].Id,
                    STT = i + 1,
                    Ngay = records[i].Ngay,
                    Nguoi = records[i].Nguoi,
                    TDVao = records[i].TDVao,
                    TDRa = records[i].TDRa,
                    TongGioLam = records[i].TongGioLam
                };
                await UpdateAsync(records[i].Id, updatedRecord); 
            }
        }
    }
}