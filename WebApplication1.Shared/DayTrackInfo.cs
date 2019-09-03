using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Shared.ATG;

namespace WebApplication1.Shared
{
    public struct DayTrackInfo
    {
        public RacingCard RacingCard { get; set; }
        public Dictionary<int, TrackBetInfo> TrackBetInfos { get; set; }
        public DoublePoolInfo DoublePoolInfo { get; set; }
        public MarkingBetPoolInfo MarkingBetPoolInfo { get; set; }
        public RaceInfoX[] RaceInfos { get; set; }
        public bool MultiTracks { get; set; }
        public double[,] DoubleOdds { get; set; }
        public Dictionary<int, TrackInfoX> TrackDataInfos { get; set; }
    }

    public struct RaceInfoX
    {
        public int LegNr { get; set; }
        public int TrackId { get; set; }
        public VPPoolInfo PoolInfo { get; set; }
        public VPResult Results { get; set; }
    }
    public class TrackInfoX
    {
        public string Code { get; set; }
        public string DomesticText { get; set; }
        public string EnglishText { get; set; }
        public int TrackId { get; set; }
        public string SlugName { get; set; }
        public string RaceTrackName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class TrackDays
    {
        public int LegNr { get; set; }

        public int StartNr { get; set; }
    }
}
