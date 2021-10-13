using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Worker : Person
    {
        public WorkerRole Role { get; }

        public Worker(int id, double bodyHeat, bool isWearingMask, bool isInQuarantine, WorkerRole role)
           : base(id, bodyHeat, isWearingMask, isInQuarantine)
        {
            Role = role;
        }

        public override string ToString()
        {
            string str = $"Id: {Id}, Role: {Role}, Body temperature: {BodyHeat}";
            str += IsWearingMask ? ", wears mask" : ", not wearing mask";
            str += IsInIsolation ? ", should be in isolation" : ", should not be in isolation";
            return str;
        }
    }
}
