using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class AttendanceTrackingRecord
    {
        public AttendanceTrackerAction Action { get; set; }
        public DateTime Date { get; set; }

        public AttendanceTrackingRecord(AttendanceTrackerAction action, DateTime date)
        {
            Action = action;
            Date = date;
        }
    }
}
