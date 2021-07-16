using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(300, ErrorMessage = "300 karekteri geçemezsiniz")]
        public string Aciklama { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10, ErrorMessage = "10 karekteri geçemezsiniz")]
        public string TakipKodu { get; set; }
    
        public DateTime Tarih { get; set; }
        public bool Durum { get; set; }

        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }
        public int CariId { get; set; }
        public virtual Cari Cari { get; set; }
        public int SatisId { get; set; }
        public virtual SatisHareket SatisHareket { get; set; }
    }
}