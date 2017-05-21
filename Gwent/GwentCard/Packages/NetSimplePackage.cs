using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class NetSimplePackage : Package, ISimple
    {
        public bool IsRemoved { get; set; }    
        public int CardID { get; set; }
        public int AffectedCardPos { get; set; }
        public bool IsToUsed { get; set; }
        public int CardLine { get; set; }
    }
}
