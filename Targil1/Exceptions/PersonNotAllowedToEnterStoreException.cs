using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    
    public class PersonNotAllowedToEnterStoreException : Exception
    {
        public RejectionReason RejectionReason { get; }
        
        public PersonNotAllowedToEnterStoreException(RejectionReason reason, string msg = "")
            :base(msg)
        {
            RejectionReason = reason;
        }

    }
}
