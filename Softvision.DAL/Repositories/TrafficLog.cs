using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ITrafficLogRepository
    {
        List<TrafficLog> GetAllTrafficLogsByLogTypeAndReplicated(LogType logType, WasReplicated wasReplicated);
        void InsertTrafficLog(TrafficLog trafficLog);
    }

    public partial class Repository : ITrafficLogRepository
    {

        public List<TrafficLog> GetAllTrafficLogsByLogTypeAndReplicated(LogType logType, WasReplicated wasReplicated)
        {
            List<TrafficLog> subCategories = new List<TrafficLog>();

            _dbReadLog.Execute(
                "TrafficLogsGetByLogTypeAndReplicated",
               new[]{
                    new SqlParameter("@LogType", (int)logType),
                    new SqlParameter("@WasReplicated", (int)wasReplicated)
                },
                 r => subCategories.Add(new TrafficLog()
                 {
                     Id = Read<int>(r, "Id"),
                     CodeText = Read<string>(r, "CodeText"),
                     Created = Read<DateTime>(r, "Created"),
                     WasReplicated = wasReplicated,
                     DateReplicated = Read<DateTime>(r, "DateReplicated"),
                     LogType = logType
                 }));

            return subCategories;
        }

        public void InsertTrafficLog(TrafficLog trafficLog)
        {
            _dbWriteLog.ExecuteNonQueryLog("TrafficLogsInsert",
               new[]{
                    new SqlParameter("@CodeText", trafficLog.CodeText),
                    new SqlParameter("@Created", trafficLog.Created),
                    new SqlParameter("@LogType", trafficLog.LogType)
                }
                );
        }

    }
}
