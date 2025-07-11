using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TimekeepingMVC.Models
{
    public class PhieuChamCong
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int STT { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Ngay { get; set; }

        public string Nguoi { get; set; }

        public string TDVao { get; set; } 

        public string TDRa { get; set; }

        public double TongGioLam { get; set; }
    }
}