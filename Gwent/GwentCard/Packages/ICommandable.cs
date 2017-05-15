using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGwentCard
{
    public interface ICommandable
    {
        string Command { get; set; }
    }
}
