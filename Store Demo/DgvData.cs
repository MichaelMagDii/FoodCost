using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Cost
{
    public class DgvData
    {
        /// <summary>
        /// This class is used to bind data at Department,Setup_Class,Setup_SubClass,Level4 and users tables with datagridviews
        /// </summary>

        public string Code { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string IsActive { get; set; }
        public string Active { get; set; }
        public string Level { get; set; }
        public string User_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string IsMain { get; set; }
        public string Category { get; set; }
        public int Qty { get; set; }
        public float Cost { get; set; }
        public float Total_Cost { get; set; }
        public string IsOutlet { get; set; }
    }
}
