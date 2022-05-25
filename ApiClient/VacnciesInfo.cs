using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    // Model of recieved from server data
    public class VacanciesInfo
    {
        public int Count { get; set; }
        public Dictionary<string, int> Skills { get; set; }
        public Salary Salary { get; set; }
        public Dictionary<string, int> Schedule { get; set; }
        public Dictionary<string, int> Experience { get; set; }
        
    }

    public class Salary
    {
        public double SalaryMin { get; set; }
        public double SalaryMax { get; set; }
        public double SalaryMedian { get; set; }
        public Dictionary<string, double> SalaryExpMedian { get; set; }
    }

  
}
