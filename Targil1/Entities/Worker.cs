using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Worker : Person
    {
        WorkerRole Role { get; }

        public Worker(int id, double bodyHeat, bool isWearingMask, bool isInQuarantine, WorkerRole role)
           : base(id, bodyHeat, isWearingMask, isInQuarantine)
        {
            Role = role;
        }
    }
}
