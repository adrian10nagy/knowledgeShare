using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Entities
{
    public class ArticlesPerMonth
    {
        public List<ArticleBL> articles { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
