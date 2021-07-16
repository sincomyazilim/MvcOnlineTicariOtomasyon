using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class FaturaSatis
    {
        [Key]
        public int FaturaId { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string FaturaSeriNo { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string FaturaSiraNo { get; set; }

        public DateTime Tarih { get; set; }


        [Column(TypeName = "char")]
        [StringLength(5)]
        public string Saat { get; set; }


        [Column(TypeName = "char")]
        [StringLength(10)]
        public string VergiNo { get; set; }

        [Column(TypeName = "char")]
        [StringLength(11)]
        public string TcNo { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(150)]
        public string Aciklama { get; set; }

        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
        public bool Durum { get; set; }


        public int SatisId { get; set; }

        //urun
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; } //1 satış hareketinin  ürünleri olabılır
        //cari
        public int CariId { get; set; }
        public virtual Cari Cari { get; set; }//1 satış hareketinin  carisi olabılır
        //Personel
        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }     //1 satış hareketinin  persoenlı olabılır

        //vergiDairesi
        public int Id { get; set; }
        public virtual VergiDaireleri VergiDaireleri { get; set; }
    }
}