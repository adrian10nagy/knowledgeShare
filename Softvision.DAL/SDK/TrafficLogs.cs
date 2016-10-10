using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class TrafficLogs
    {
        private static ITrafficLogRepository _repository;

        static TrafficLogs()
        {
            _repository = new Repository();
        }

        public List<TrafficLog> GetAllTrafficLogsByLogType(LogType logType, WasReplicated wasReplicated)
        {
            return _repository.GetAllTrafficLogsByLogTypeAndReplicated(logType, wasReplicated);
        }

        public void WriteLog(TrafficLog trafficLog)
        {
            _repository.InsertTrafficLog(trafficLog);
        }
    }
}
