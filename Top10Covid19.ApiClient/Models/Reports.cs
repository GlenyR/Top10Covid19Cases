using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Top10Covid19.ApiClient.Models
{
    [DataContract]
	public class Reports
	{
		[DataMember(Name = "data")]
		public List<Report> Data { get; set; }
	}

}
