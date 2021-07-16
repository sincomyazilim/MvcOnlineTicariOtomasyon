using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int PersonelId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelSoyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string PersonelGorsel { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(11)]
        public string Gsm { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MAil { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Sifre { get; set; }


        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string Yetki { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; } //1 personel satışhareketinde çok satış yapabılır 1 çok
        public int DepartmanId { get; set; }
        public virtual Departman Departman { get; set; }//1 Persoenl 1 departmanda olabılır
        public ICollection<KargoDetay> KargoDetays { get; set; }
    }
}
