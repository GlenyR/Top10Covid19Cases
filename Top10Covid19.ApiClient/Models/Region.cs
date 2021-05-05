using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Top10Covid19.ApiClient.Models
{
	[DataContract]
	public class Region
	{
		[DataMember(Name = "iso")]
		public string Iso { get; set; }
		[DataMember(Name = "name")]
		public string Name { get; set; }
		[DataMember(Name = "province")]
		public string Province { get; set; }
		[DataMember(Name = "lat")]
		public string Lat { get; set; }
		[DataMember(Name = "long")]
		public string Long { get; set; }
		[DataMember(Name = "cities")]
		public List<City> Cities { get; set; }
	}

}
