using System;

namespace EngineerProject.Mobile.Utility
{
    public static class DataFormatters
    {
        public static string DateToString(this DateTime value) => $"{value.ToShortDateString()} {value.ToShortTimeString()}";
    }
}