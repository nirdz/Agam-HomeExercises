using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public enum RejectionReason
    {
        BodyHeatHigh,
        NoMask,
        InIsolation
    }

    public enum AttendanceTrackerAction
    {
        Entrance,
        Exit
    }

    public enum WorkerRole
    {
        GeneralWorker,
        Cashier
    }

    /*public enum Products
    {
        Banana,
        Milk,
        Bread,
        Egg,
        Yogurt,
        Oil
    }*/

    public enum OrderStatus
    {
        NoOrder,
        OrderCreated,
        OrderRecievedInFactory,
        DuringDelivery,
        InBranchWarehouse
    }

}
