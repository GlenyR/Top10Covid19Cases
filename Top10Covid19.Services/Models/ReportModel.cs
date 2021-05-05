using System;

namespace Top10Covid19.Services.Models
{
    public class ReportModel
    {

        public ReportModel() {}
        public ReportModel(string name, int confirmed, int deaths)
        {
            _name = name;
            _confirmed = confirmed;
            _deaths = deaths;

        }

        private readonly string _name;
        private readonly int _confirmed;
        private readonly int _deaths;

        public string Name { get { return _name; } }
        public int Confirmed { get  { return _confirmed; } }
        public int Deaths { get { return _deaths; } }


        public string[] GetStringArrayValues()
        {
            return new string[] { Name, Confirmed.ToString(), Deaths.ToString() };
        }
    }
}


