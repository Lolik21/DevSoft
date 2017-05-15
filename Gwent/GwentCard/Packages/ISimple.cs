using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public interface ISimple
    {
        int EffectedCardID { get; set; }
        int SelectedCardID { get; set; }
    }
}
