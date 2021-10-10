using System;

namespace Scheduler
{
    public class Configuration
    {
        public Configuration()
        { }
        public DateTime CurrentDate { get; set; }
        public DateTime? Date  { get; set; }
        public Type Type { get; set; }
        public Frecuency Frecuency { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
