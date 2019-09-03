using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Shared.ATG
{
	public class AtgDate
	{
		public int date { get; set; }
		public int month { get; set; }
		public int year { get; set; }
	}

	public class AtgTime
	{
		public int hour { get; set; }

		public int minute { get; set; }

		public int second { get; set; }

		public int tenth { get; set; }
	}
		

	public class AtgDateTime
	{
		public AtgDate date { get; set; }

		public AtgTime time { get; set; }
	}
}
