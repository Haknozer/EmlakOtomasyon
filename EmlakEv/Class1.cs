namespace EmlakEv
{

    using System;
    using System.IO;

    public enum tur
    {
        Daire,
        Bahceli,
        Dubleks,
        Mustakil
    }

    public class Ev
    {
        private int odaSayisi;
        private int katNumarasi;
        private string il;
        private string semt;
        private double alani;
        private int yapimTarihi;
        private tur turu;
        private bool aktif;
        private int emlakNumarasi;
        private string emlakFile = "emlak_no.txt";
        public Ev(int odaSayisi, int katNumarasi,string il, string semt, double alani, int yapimTarihi, tur turu,bool aktif,int emlakNumarasi)
        {
            this.OdaSayisi = odaSayisi;
            this.KatNumarasi = katNumarasi;
            this.Il = il;
            this.Semt = semt;
            this.Alani = alani;
            this.YapimTarihi = yapimTarihi;
            this.Turu = turu;
            this.emlakNumarasi = emlakNumarasi;
            this.aktif = true;
        }
       
        public int OdaSayisi
        {
            get { return odaSayisi; }
            set
            {
                if (value < 0)
                {
                    LogKayit("OdaSayisi", value);
                    odaSayisi = 0;
                }
                else
                {
                    LogKayit("OdaSayisi", value);
                    odaSayisi = value;
                }
            }
        }

        public int KatNumarasi
        {
            get { return katNumarasi; }
            set { katNumarasi = value; }
        }

        public string Il
        {
            get { return il;}
            set { il = value; }
        }
        public string Semt
        {
            get { return semt; }
            set { semt = value; }
        }

        public double Alani
        {
            get { return alani; }
            set { alani = value; }
        }

        public int YapimTarihi
        {
            get { return yapimTarihi; }
            set { yapimTarihi = value; }
        }

        public tur Turu
        {
            get { return turu; }
            set { turu = value; }
        }

        public bool Aktif
        {
            get { return aktif; }
            set { aktif = value; }
        }

        public int EmlakNumarasi
        {
            get { return emlakNumarasi; }
        }

        public int Yas
        {
            get { return DateTime.Now.Year - yapimTarihi; }
        }


        protected void LogKayit(string odaSayisi, object value)
        {
            string logMessage = $"{odaSayisi} : {value}";
            File.AppendAllText("ev_log.txt", $"{DateTime.Now}: {logMessage}{Environment.NewLine}");
        }

        public virtual string EvBilgiGetir()
        {
            return string.Format("Emlak Numarası: {0}, Türü: {1}, Oda Sayısı: {2}, Kat Numarası: {3}, Semt: {4}, Alanı: {5}, Yapım Tarihi: {6}, Aktif: {7}, Yaş: {8}",
                EmlakNumarasi, Turu, OdaSayisi, KatNumarasi, Semt, Alani, YapimTarihi, Aktif, Yas);
        }

        public double FiyatHesapla()
        {
            double katsayi = 200;
            string filePath = "room_cost.txt";

            if (File.Exists(filePath))
            {
                string katsayiStr = File.ReadAllText(filePath);
                if (double.TryParse(katsayiStr, out double result))
                {
                    katsayi = result;
                }
            }
            else
            {
                File.WriteAllText(filePath, katsayi.ToString());
            }

            return odaSayisi * katsayi;
        }
    }

    public class KiralikEv : Ev
    {
        public double Depozito { get; set; }
        public double Kirasi { get; set; }

        public KiralikEv(int odaSayisi, int katNumarasi,string il ,string semt, double alani, int yapimTarihi, tur turu, bool aktif, double depozito, double kirasi, int emlakNumarasi)
            : base(odaSayisi, katNumarasi,il, semt, alani, yapimTarihi, turu, aktif, emlakNumarasi)
        {
            this.Depozito = depozito;
            this.Kirasi = FiyatHesapla();
        }



        public override string EvBilgiGetir()
        {
            return base.EvBilgiGetir() + string.Format(", Depozito: {0}, Kirası: {1}", Depozito, Kirasi);
        }
    }
    public class SatilikEv : Ev
    {
        public double Fiyati { get; set; }

        public SatilikEv(int odaSayisi, int katNumarasi, string il, string semt, double alani, int yapimTarihi, tur turu, bool aktif, double fiyati, int emlakNumarasi)
            : base(odaSayisi, katNumarasi, il,semt, alani, yapimTarihi, turu, aktif, emlakNumarasi)
        {
            this.Fiyati = fiyati;
        }

        public override string EvBilgiGetir()
        {
            return base.EvBilgiGetir() + string.Format(", Fiyatı: {0}", Fiyati);
        }
    }


}
