using System.Runtime.Serialization;

namespace Top10Covid19.ApiClient.Models
{
    [DataContract]
	public class Report
	{
		[DataMember(Name = "date")]
		public string Date { get; set; }
		[DataMember(Name = "confirmed")]
		public int Confirmed { get; set; }
		[DataMember(Name = "deaths")]
		public int Deaths { get; set; }
		[DataMember(Name = "recovered")]
		public int? Recovered { get; set; }
		[DataMember(Name = "confirmed_diff")]
		public int? ConfirmedDiff { get; set; }
		[DataMember(Name = "deaths_diff")]
		public int? DeathsDiff { get; set; }
		[DataMember(Name = "recovered_diff")]
		public int? RecoveredDiff { get; set; }
		[DataMember(Name = "last_update")]
		public string LastUpdate { get; set; }
		[DataMember(Name = "active")]
		public int? Active { get; set; }
		[DataMember(Name = "active_diff")]
		public int? ActiveDiff { get; set; }
		[DataMember(Name = "fatality_rate")]
		public double FatalityRate { get; set; }
		[DataMember(Name = "region")]
		public Region Region { get; set; }
	}
}
