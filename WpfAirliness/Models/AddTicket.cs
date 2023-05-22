using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFAirelines.Models;

namespace WpfAirliness.Models
{
    internal class AddTicket 
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string PasportNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string TimeTripMinute { get;set; }
        public string DateTimeDeparture { get; set; }
        public string Price { get; set; }
        public string BortNumber { get; set; }
        public string SeatNumber { get; set; }

    }
}
