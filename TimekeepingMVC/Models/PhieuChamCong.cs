namespace TimekeepingMVC.Models
{
    public class PhieuChamCong
    {
        public int Id { get; set; }
        public int STT { get; set; }
        public DateTime Ngay { get; set; }
        public string Nguoi { get; set; }
        public string TDVao { get; set; } 
        public string TDRa { get; set; } 
        public double TongGioLam { get; set; }
    }
}