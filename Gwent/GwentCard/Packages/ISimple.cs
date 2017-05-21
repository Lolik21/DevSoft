using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public interface ISimple
    {
        bool IsRemoved { get; set; }
        bool IsToUsed { get; set; }
        int CardID { get; set; }
        int CardLine { get; set; }
        int AffectedCardPos { get; set; }
    }
}
