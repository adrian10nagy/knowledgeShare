using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Softvision.Helpers
{
    interface IWeb
    {
        //Session Session { get; }
        Coookie Coookie { get; }
    }

    public class Web: IWeb
    {
        private static Web _instance = new Web();
        public static Web Instance { get { return _instance ?? getInstance(); } }

        static Web getInstance()
        {
            return new Web();
        }

        public SessionHelper Session { get { return new SessionHelper(); } }
        public Coookie Coookie { get { return new Coookie(); } }
    }
}