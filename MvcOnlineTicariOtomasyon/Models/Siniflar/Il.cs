using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Il
    {
        public int ilId { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string Adi { get; set; }


        public ICollection<Cari> Caris { get; set; }

    }
}