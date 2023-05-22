using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using WPFAirelines.Models;

namespace WpfAirliness.Helper
{
    /// <summary>
    /// Класс для работы с корзиной
    /// </summary>
    /// 
    public enum CharSeat
    {
        A,
        B,
        C,
        D,
        E,
        F
    }
    static class BasketClass
    {
        public static List<Route> basketListTikets { get; set; }

        public static string RandomSeatNumber<CharSeat>()
        {
            var rnd = new Random();
            var value = Enum.GetValues(typeof(CharSeat));
            var result = $"{value.GetValue(rnd.Next(value.Length))}{rnd.Next(1, 26)}";
            Trace.WriteLine(result);
            return result;

        }
    }
}
