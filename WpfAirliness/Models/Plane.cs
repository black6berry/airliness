using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAirelines.Models
{
    internal class Plane
    {
        public string Id { get; set; }
        public string BortNumber { get; set; }
        public string Model { get; set; }
        public string CreationDate { get; set; }
        public string YearsOfUse { get; set; }
        public string ReadyFly { get; set; }
        public string MaxPassenger { get; set; }

    }
}
