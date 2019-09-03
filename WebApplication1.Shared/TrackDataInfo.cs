using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Shared
{
    public class TrackDataInfo
    {
        public class TrackCodeInfo
        {
            public string Code { get; set; }
            public string TrackName { get; set; }
            public int SportSystemId { get; set; }
            public string SlugName { get; set; }
            public TrackCodeInfo() { }
        }

        public string Code { get; set; }
        public string DomesticText { get; set; }
        public string EnglishText { get; set; }
        public int TrackId { get; set; }
        public string SlugName { get; set; }
        public Dictionary<string, string> RaceTrackNameList { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public Dictionary<string, TrackCodeInfo> TrackCodes { get; set; }

        public TrackDataInfo() { }
        
        public string GetTrackName(string pCode, string pItspEventCode)
        {
            string trackName = this.DomesticText;
            if (pItspEventCode != null && this.RaceTrackNameList != null && this.RaceTrackNameList.ContainsKey(pItspEventCode))
                trackName = this.RaceTrackNameList[pItspEventCode];
            else if (pCode != null && this.TrackCodes != null && this.TrackCodes.ContainsKey(pCode))
                trackName = this.TrackCodes[pCode].TrackName;

            return trackName;
        }

        public string GetSlugName(string pCode)
        {
            if (pCode != null && this.TrackCodes != null && this.TrackCodes.ContainsKey(pCode))
                return this.TrackCodes[pCode].SlugName;
            else
                return this.SlugName;
        }

        public (string, string) GetTrackNames(string pCode, string pItspEventCode)
        {
            string trackName = this.DomesticText;
            string slugName = this.SlugName;
            if (pCode != null && this.TrackCodes != null && this.TrackCodes.ContainsKey(pCode))
            {
                trackName = this.TrackCodes[pCode].TrackName;
                slugName = this.TrackCodes[pCode].SlugName;
            }
            if (pItspEventCode != null && this.RaceTrackNameList != null && this.RaceTrackNameList.ContainsKey(pItspEventCode))
                trackName = this.RaceTrackNameList[pItspEventCode];

            return (trackName, slugName);
        }
    }
}
