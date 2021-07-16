using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{ 
    public class Departman
    {
        [Key]
        public int DepartmanId { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string DepartmanAd { get; set; }
        public bool Durum { get; set; } = true;
        public ICollection<Personel> Personels { get; set; } //1 departmanda cok personel olabılır
    }
}
