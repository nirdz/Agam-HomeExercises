using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class WorkersManager
    {
        /*
         *  Log of when each worker clocked in and out of work.
         *  List of pairs - id of worker, list of records.
         *  Each AttendanceTrackingRecord contains action (entrance and exit) and date.
        */
        private Dictionary<int, List<AttendanceTrackingRecord>> attendanceTrackingLog;

        public WorkersManager()
        {
            attendanceTrackingLog = new Dictionary<int, List<AttendanceTrackingRecord>>();
        }

        /*
         *  Clocks in worker and logs his attendance.
         *  Throws exception if worker not allowed to clock in.
        */
        public void ClockInWorker(Worker worker, DateTime date)
        {
            
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

        /*
         *  Clocks out worker and logs it.
        */
        public void ClockOutWorker(Worker worker, DateTime date)
        {
            RegisterAttendanceToLog(worker.Id, AttendanceTrackerAction.Exit, date);
        }

        private void RegisterAttendanceToLog(int workerId, AttendanceTrackerAction action, DateTime date)
        {
            AttendanceTrackingRecord record = new AttendanceTrackingRecord(action, date);
            // Check if worker id already in dict. If not, create entry
            if (!attendanceTrackingLog.ContainsKey(workerId))
            {
                attendanceTrackingLog.Add(workerId, new List<AttendanceTrackingRecord>());
            }
            attendanceTrackingLog[workerId].Add(record);
        }

        public void PrintAttendanceLog()
        {
            foreach (KeyValuePair<int, List<AttendanceTrackingRecord>> entry in attendanceTrackingLog)
            {
                int workerId = entry.Key;
                List<AttendanceTrackingRecord> records = entry.Value;
                Console.WriteLine($"Worker id {workerId}:");
                foreach (AttendanceTrackingRecord record in records)
                {
                    Console.WriteLine(record);
                }
            }
            Console.WriteLine($"Total records: {attendanceTrackingLog.Count}");
        }

        private class AttendanceTrackingRecord
        {
            public AttendanceTrackerAction Action { get; set; }
            public DateTime Date { get; set; }

            public AttendanceTrackingRecord(AttendanceTrackerAction action, DateTime date)
            {
                Action = action;
                Date = date;
            }

            public override string ToString()
            {
                return $"Action: {Action}, Date: {Date}";
            }
        }
    }
}
