using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{ 
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string KategoriAd { get; set; }
        public bool Durum { get; set; } = true;
        public ICollection<Urun> Uruns { get; set; }//bir kategorının cok urunu olabılır
    }
}
