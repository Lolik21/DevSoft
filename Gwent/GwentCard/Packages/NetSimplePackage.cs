using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class NetSimplePackage : Package, ISimple
    {
        public int EffectedCardID { get; set; }
        public int SelectedCardID { get; set; }
    }
}
