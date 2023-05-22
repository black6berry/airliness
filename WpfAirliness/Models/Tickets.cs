using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAirelines.Models
{
    internal class Tickets
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string PasportNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string BortNumber { get; set; }
        public string PlaneModel { get; set; }
        public decimal PriceTicket { get; set; }
        public string SeatNumber { get; set; }
        public string DateTimeDeparture { get; set; }
    }
}
