using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Top10Covid19.ApiClient.Models
{   
	[DataContract]
	public class Regions
	{
		[DataMember(Name = "data")]
		public List<Region> Data { get; set; }
	}
}