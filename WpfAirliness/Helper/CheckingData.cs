using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfAirliness.Helper
{
    /// <summary>
    /// класс для проверки значения в элеменете 
    /// </summary>
    internal class CheckingData
    {
        /// <summary>
        /// Расширение для класса принимает массив TextBox
        /// </summary>
        /// <param name="txbData"></param>
        /// <returns></returns>
        public static bool Check( params System.Windows.Controls.TextBox[] txbData)
        {
            bool result = false;
            foreach (var txb in txbData)
            {
                if (txb.Text != null && txb.Text != "")
                {
                    result = true;
                }
                else
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// Расширение для класса принимает массив TextBox и 1 ComboBox
        /// </summary>
        /// <param name="cmbData"></param>
        /// <param name="txbData"></param>
        /// <returns></returns>
        public static bool Check(ComboBox cmbData, params System.Windows.Controls.TextBox[] txbData)
        {
            bool result = false;
            if (cmbData.Text != null && cmbData.Text != "")
            {
                foreach (var txb in txbData)
                {
                    if (txb.Text != null && txb.Text != "")
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            }
            else
            {
                return result;
            }
        }

        public static bool Check(ComboBox cmbData, DatePicker dtpData, params System.Windows.Controls.TextBox[] txbData)
        {
            bool result = false;
            if (cmbData.Text != null && cmbData.Text != "")
            { 
                if (dtpData.Text != null && dtpData.Text != "")
                {
                    foreach (var txb in txbData)
                    {
                        if (txb.Text != null && txb.Text != "")
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                    return result;

                }
                else
                {
                    return result;
                }
            }
            else
            {
                return result;
            }
        }
        /// <summary>
        /// Расширение для класса принимает массив ComboBox или перечисление
        /// </summary>
        /// <param name="cmbxData"></param>
        /// <returns></returns>
        public static bool Check(params ComboBox[] cmbxData)
        {
            bool result = false;
            foreach (var cmbx in cmbxData)
            {
                if (cmbx.Text != null && cmbx.Text != "")
                {
                    result = true;
                }
                else
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// Расширение для класса принимает массив DatePicker или перечисление
        /// </summary>
        /// <param name="calendarData"></param>
        /// <returns></returns>
        public static bool Check(params DatePicker[] calendarData)
        {
            bool result = false;
            foreach (var calendar in calendarData)
            {
                if (calendar.SelectedDate != null )
                {
                    result = true;
                }
                else
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
