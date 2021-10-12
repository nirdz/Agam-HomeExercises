using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    
    public class CustomerNotAllowedToEnterQueueException : Exception
    {
        public QueueRejectionReason RejectionReason { get; }
        
        public CustomerNotAllowedToEnterQueueException(QueueRejectionReason reason)
        {
            RejectionReason = reason;
        }

    }
}
