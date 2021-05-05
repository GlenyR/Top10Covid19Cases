using System.Runtime.Serialization;

namespace Top10Covid19.ApiClient.Models
{
	[DataContract]
	public class City
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
		[DataMember(Name = "date")]
		public string Date { get; set; }
		[DataMember(Name = "fips")]
		public int? Fips { get; set; }
		[DataMember(Name = "lat")]
		public string Lat { get; set; }
		[DataMember(Name = "long")]
		public string Long { get; set; }
		[DataMember(Name = "confirmed")]
		public int? Confirmed { get; set; }
		[DataMember(Name = "deaths")]
		public int? Deaths { get; set; }
		[DataMember(Name = "confirmed_diff")]
		public int? ConfirmedDiff { get; set; }
		[DataMember(Name = "deaths_diff")]
		public int? DeathsDiff { get; set; }
		[DataMember(Name = "last_update")]
		public string LastUpdate { get; set; }
	}
}
