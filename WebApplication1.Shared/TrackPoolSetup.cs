using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Shared.ATG;

namespace WebApplication1.Shared
{
    public class TrackPoolSetup
    {
        public CodeText BetType { get; set; }

        public bool HasBoost { get; set; }

        public TrackDataInfo[] HostTrackInfo { get; set; }

        public string ItspEventCode { get; set; }

        public LegInfo[] LegInfo { get; set; }

        public string MainTrackName { get; set; }

        public string MainTrackNameEnglish { get; set; }

        public AtgDate RaceDayDate { get; set; }

        public DateTime? PostDateTimeUTC { get; set; }

        public CodeText Track { get; set; }

        public TrackKey TrackKey { get; set; }

        public string MainTrackSlugName { get; set; }

        public DateTime RaceTimeUTC { get; set; }//?

        public Amount Turnover { get; set; }
        public Amount Jackpot { get; set; }
        public bool HasJackpot { get; set; }
        public bool SaleOpen { get; set; }


    }
}
