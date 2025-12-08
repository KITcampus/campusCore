using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCore.Common
{
    public class StuInfoDto
    {
        public int studentId { get; set; }
        public string residentNum { get; set; }
        public string pw { get; set; }
        public string name { get; set; }
        public int grade { get; set; }
        public string englishName { get; set; }
        public string major { get; set; }
        public string department { get; set; }
        public string admissionType { get; set; }
        public string class_ { get; set; }
        public string admissionDate { get; set; }
        public string studentStatus { get; set; }
        public string professor { get; set; }
        public string phone { get; set; }
        public string emergency1 { get; set; }
        public string emergency2 { get; set; }
        public string emergency3 { get; set; }
        public string emergency4 { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string lastUpdated { get; set; }
    }

}
