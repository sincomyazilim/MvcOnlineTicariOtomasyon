using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class KargoTakip
    {
        [Key]
        public int KargoTakipId { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(10, ErrorMessage = "10 karekteri geçemezsiniz")]
        public string TakipKodu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100, ErrorMessage = "100 karekteri geçemezsiniz")]
        public string Aciklama { get; set; }

        public DateTime TarihZaman { get; set; }

    }
}