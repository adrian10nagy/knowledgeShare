using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Namespace Repositories not Settings so partial class to be visible
namespace DAL.Repositories
{
    public partial class Repository
    {
        public static T Read<T>(IDataRecord record, string key)
        {
            return Read(record, key, default(T));
        }

        public static T Read<T>(IDataRecord record, string key, T defaultValue)
        {
            return record[key] != DBNull.Value ? (T)record[key] : defaultValue;
        }
    }
}
