using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Shared.ATG;

namespace WebApplication1.Shared
{
    public class RaceCalendarDayRaces
    {
        public AtgDate RaceDayDate { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public string TrackCode { get; set; }
        public string ItspEventCode { get; set; }
        public string CountryCode { get; set; }
        public List<TrackPoolSetup> TrackPoolSetups { get; set; }

        public DateTime? NextPostDateTimeUTC { get; set; }
        public string NextBetType { get; set; }
        public bool GotRaces { get; set; }
    }

    public class RaceDate
    {
        public DateTime Date { get; set; }
        public bool IsSelected { get; set; }

        public RaceDate()
        {
            IsSelected = false;
        }
    }
}
