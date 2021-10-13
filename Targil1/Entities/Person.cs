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
        public double BodyHeat { get; set; } // Body temperature. For example 36
        public bool IsWearingMask { get; set; }
        public bool IsInIsolation { get; set; }

        public Person(int id, double bodyHeat, bool isWearingMask, bool isInIsolation)
        {
            Id = id;
            BodyHeat = bodyHeat;
            IsWearingMask = isWearingMask;
            IsInIsolation = isInIsolation;
        }

        public override string ToString()
        {
            string str = $"Id: {Id}, Body temperature: {BodyHeat}";
            str += IsWearingMask ? ", wears mask" : ", not wearing mask";
            str += IsInIsolation ? ", should be in isolation" : ", should not be in isolation";
            return str;
        }
    }
}
