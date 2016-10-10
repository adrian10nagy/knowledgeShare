using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TrafficLog
    {
        public int Id { get; set; }
        public string CodeText { get; set; }
        public DateTime Created { get; set; }
        public WasReplicated WasReplicated { get; set; }
        public DateTime DateReplicated { get; set; }
        public LogType LogType { get; set; }
    }

    public enum LogType
    {
        Unknown = 0,
        Read = 1,
        Write = 2
    }

    public enum WasReplicated
    {
        No = 0,
        Yes = 1,
    }
}
