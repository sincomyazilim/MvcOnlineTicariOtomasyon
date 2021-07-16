using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class CariUrun
    {
        public List<Cari> Cari { get; set; }
        public List<Urun> Urun { get; set; }
        public List<SatisHareket> SatisHareket{ get; set; }
        public SatisHareket satisHareket { get; set; }
    }
}