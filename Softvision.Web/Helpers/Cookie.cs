using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Softvision.Helpers
{
    public class Coookie
    {
        private string _name { get; set; }
        private string _value { get; set; }
        private string _expiration { get; set; }

        public void SetCookie(string name, string value, int expiration)
        {
            HttpCookie cookie = new HttpCookie("StudentCookies");
            cookie.Name = name;
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddDays(expiration);
        }

        public string GetCookie(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            var cookieValue = cookie != null ? cookie.Value : string.Empty;
            return cookieValue;
        }
    }
}