using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class WorkersManager
    {
        public Dictionary<int, List<AttendanceTrackingRecord>> AttendanceTrackingLog { get; }

        public WorkersManager()
        {
            AttendanceTrackingLog = new Dictionary<int, List<AttendanceTrackingRecord>>();
        }

        public void ClockInWorker(Worker worker, DateTime date)
        {
            // Throw exception if worker not allowed to clock in
            if (worker.BodyHeat > 38)
            {
                throw new CustomerNotAllowedToEnterQueueException(QueueRejectionReason.BodyHeatHigh);
            }
            if (!worker.IsWearingMask)
            {
                throw new CustomerNotAllowedToEnterQueueException(QueueRejectionReason.NoMask);
            }
            if (worker.IsInIsolation)
            {
                throw new CustomerNotAllowedToEnterQueueException(QueueRejectionReason.InIsolation);
            }

            RegisterAttendanceToLog(worker.Id, AttendanceTrackerAction.Entrance, date);
        }

        public void ClockOutWorker(Worker worker, DateTime date)
        {
            RegisterAttendanceToLog(worker.Id, AttendanceTrackerAction.Exit, date);
        }

        private void RegisterAttendanceToLog(int workerId, AttendanceTrackerAction action, DateTime date)
        {
            AttendanceTrackingRecord record = new AttendanceTrackingRecord(action, date);
            // TODO: check if id already in dict
        }
    }
}
