using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterResourceData.WaterQuality.Ammonia
{
    public class AmmoniaLookupTable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<LookupCell> Cells { get; set; }
    }
}
