using WebApplication1.Shared.ATG;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Shared
{
	public class ComingRaceBox
	{
		public string Key { get; set; }
		public DateTime RaceTimeUTC { get; set; }
		public AtgDateTime RaceTimeUTCAtg { get; set; }
		public string RaceDate { get; set; }
		public string BetType { get; set; }
		public int TrackId { get; set; }
		public string TrackName { get; set; }
		public string TrackSlug { get; set; }
		public string TrackCode { get; set; }
		public Amount Turnover { get; set; }
		public bool HasJackpot { get; set; }
		public Amount Jackpot { get; set; }
		public bool SaleOpen { get; set; }

        public string CountryCode { get; set; }
    }
}
