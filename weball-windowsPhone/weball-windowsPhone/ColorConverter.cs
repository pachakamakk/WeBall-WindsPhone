using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using WPControls;

namespace weball_windowsPhone
{
    public class ColorConverter : IDateToBrushConverter
    {
        public List<DateTime> Dates { get; set; }
        public Brush Convert(DateTime dateTime, bool isSelected, Brush defaultValue, BrushType brushType)
        {
            //System.Diagnostics.Debug.WriteLine("Date: " + dateTime.ToString() + " and " + );
            if (brushType == BrushType.Background)
            {
                if (Dates != null && Dates.Any(d => (d.Day == dateTime.Day) &&
                                                (d.Month == dateTime.Month) &&
                                                (d.Year == dateTime.Year)))
                {
                    return new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                /*if (dateTime == new DateTime(DateTime.Today.Year, DateTime.Today.Month, 6))
                {
                    return new SolidColorBrush(Colors.Cyan);
                }
                else
                {
                    return defaultValue;
                }*/
            }
            return defaultValue;
        }
    }
}