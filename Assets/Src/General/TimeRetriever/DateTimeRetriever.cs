using System;
using System.Globalization;

namespace Src.General.TimeRetriever
{
    public class DateTimeRetriever : IDateTimeRetriever
    {
        private readonly CultureInfo usedDateTimeCulture = new CultureInfo("de-DE");
        
        public string GetFormattedDateTime()
        {
            return DateTime.Now.ToString(new CultureInfo("de-DE"));
        }
    }
}