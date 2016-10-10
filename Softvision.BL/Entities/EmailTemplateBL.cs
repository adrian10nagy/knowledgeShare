using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Entities
{
    public class EmailTemplateBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Structure { get; set; }
        public int NrOfSubstitutes { get; set; }
    }
}
