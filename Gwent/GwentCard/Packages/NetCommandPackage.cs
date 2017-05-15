using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public class NetCommandPackage : Package , ICommandable
    {
        public string Command { get; set; }
      
    }
}
