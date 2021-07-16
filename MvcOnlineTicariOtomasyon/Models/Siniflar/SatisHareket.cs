using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class SatisHareket
    {
        [Key]
        public int SatisId { get; set; }

        public DateTime Tarih { get; set; } 
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }
        public bool Durum { get; set; }
        public bool KargoDurum { get; set; }
        

        //urun
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; } //1 satış hareketinin  ürünleri olabılır
        //cari
        public int CariId { get; set; }
        public virtual Cari Cari { get; set; }//1 satış hareketinin  carisi olabılır
        //Personel
        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }     //1 satış hareketinin  persoenlı olabılır
        public ICollection<KargoDetay> KargoDetays { get; set; }

    }
}
