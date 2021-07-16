using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class FaturaKalem
    {
        [Key]
        public int FaturaKalemId { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(150)]
        public string Aciklama { get; set; }
        public string UrunAdi { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }

        public int FaturaId { get; set; }
        public virtual Fatura Fatura { get; set; } //1 fatura kalemin 1 faturası olur
    }
}
