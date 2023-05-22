using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WPFAirelines.Models
{
    internal class Captain
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string PasportNumber { get; set; }
        public string Address { get; set; }
        public string PersonalNumber { get; set; }
        public string PilotExperience { get; set; }
    }
}
