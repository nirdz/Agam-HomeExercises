using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Person
    {
        public int Id { get; }
        public double BodyHeat { get; set; }
        public bool IsWearingMask { get; set; }
        public bool IsInIsolation { get; set; }

        public Person(int id, double bodyHeat, bool isWearingMask, bool isInIsolation)
        {
            Id = id;
            BodyHeat = bodyHeat;
            IsWearingMask = isWearingMask;
            IsInIsolation = isInIsolation;
        }

    }
}
