using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAirelines.Models
{
    internal class Route
    {
        public int Id { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public decimal Price { get; set; }
        public string DateTimeDeparture { get; set; }
        public string BortNumber { get; set; }
        public string TimeTripMinute { get; set; }
    }
}
