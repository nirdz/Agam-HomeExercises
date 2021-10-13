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
                throw new PersonNotAllowedToEnterStoreException(RejectionReason.BodyHeatHigh);
            }
            if (!worker.IsWearingMask)
            {
                throw new PersonNotAllowedToEnterStoreException(RejectionReason.NoMask);
            }
            if (worker.IsInIsolation)
            {
                throw new PersonNotAllowedToEnterStoreException(RejectionReason.InIsolation);
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
            // Check if worker id already in dict. If not, create entry
            if (!AttendanceTrackingLog.ContainsKey(workerId))
            {
                AttendanceTrackingLog.Add(workerId, new List<AttendanceTrackingRecord>());
            }
            AttendanceTrackingLog[workerId].Add(record);
        }

        public void PrintAttendanceLog()
        {
            foreach (KeyValuePair<int, List<AttendanceTrackingRecord>> entry in AttendanceTrackingLog)
            {
                int workerId = entry.Key;
                List<AttendanceTrackingRecord> records = entry.Value;
                Console.WriteLine($"Worker id {workerId}:");
                foreach (AttendanceTrackingRecord record in records)
                {
                    Console.WriteLine(record);
                }
            }
            Console.WriteLine($"Total records: {AttendanceTrackingLog.Count}");
        }
    }
}
