using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cari
    {
        [Key]
        public int CariId { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30,ErrorMessage ="30 karekteri geçemezsiniz")]
     
        public string CariAd { get; set; }



 
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "30 karekteri geçemezsiniz")]
        public string CariSoyad { get; set; }



        
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "30 karekteri geçemezsiniz")]
        public string CariSehir { get; set; }




        
        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "50 karekteri geçemezsiniz")]

        public string CariMail { get; set; }



        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string CariSifre { get; set; }


        public bool Durum { get; set; } = true;

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string VergiNo { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(160)]
        public string Adres { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(11)]
        public string TcNo { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; } //1 cari satışhareketinde öok olabılır 1 çok


        public int ilId { get; set; }
        public virtual Il Il { get; set; }

        public ICollection<KargoDetay> KargoDetays { get; set; }
    }
}
