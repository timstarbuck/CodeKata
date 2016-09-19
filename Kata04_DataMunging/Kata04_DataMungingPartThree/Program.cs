using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kata04_DataMungingPartThree
{
    class Program
    {
        static void Main(string[] args)
        {
            TryParseRecDelegate<FootballRecord> footballDel = FootballRecord.TryParseFromLine<FootballRecord>;
            List<FootballRecord> records = FetchData<FootballRecord>("football.dat", footballDel);

            var minSpread = records.OrderBy(r => Math.Abs(r.PointsFor - r.PointsAgainst)).First();

            Console.WriteLine($"Team with min spread = {minSpread.TeamName}");

            TryParseRecDelegate<WeatherRecord> weatherDel = WeatherRecord.TryParseFromLine<WeatherRecord>;
            List<WeatherRecord> wRecords = FetchData("weather.dat", weatherDel);

            var minSpreadTemp = wRecords.OrderBy(r => r.MaxTemp - r.MinTemp).First();

            Console.WriteLine($"Day with min spread = {minSpreadTemp.Day}");

            Console.ReadLine();


        }

        public delegate bool TryParseRecDelegate<T>(string line, out T record);

        private static List<T> FetchData<T>(string fileName, TryParseRecDelegate<T> parser)
        {
            List<T> records = new List<T>();

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("file was not found", fileName);
            }

            foreach (string line in File.ReadAllLines(fileName))
            {
                T rec;
                if (parser(line, out rec))
                {
                    records.Add(rec);
                }
            }

            return records;
        }

    }

    public class FootballRecord 
    {
        public string TeamName { get; set; }

        public int PointsFor { get; set; }

        public int PointsAgainst { get; set; }

        private static string _lineFormat = @"^(?<f1>.{7})(?<teamName>.{15})" +
                                          "(?<f2>.{21})(?<for>.{2})" +
                                          "(?<f3>.{5})(?<against>.{2})" +
                                          "(?<rest>.+)$";

        private static Regex _lineRegex = new Regex(_lineFormat);

        public static bool TryParseFromLine<T>(string line, out FootballRecord record)
        {
            Match match = _lineRegex.Match(line);

            int tempInt;
            if (int.TryParse(match.Groups["for"].Value, out tempInt))
            {
                record = new FootballRecord();
                record.TeamName = match.Groups["teamName"].Value.Trim();
                record.PointsFor = tempInt;
                record.PointsAgainst = int.TryParse(match.Groups["against"].Value, out tempInt) ? tempInt : 0;
                return true;
            }

            record = default(FootballRecord);
            return false;
        }

    }


    public class WeatherRecord
    {

        public int Day { get; set; }

        public int MaxTemp { get; set; }

        public int MinTemp { get; set; }

        public int AvgTemp { get; set; }

        public string HDDay { get; set; }

        public float AvgDewPoint { get; set; }

        public string HrP1 { get; set; }

        public float TPcpn { get; set; }

        public string WxType { get; set; }

        public int PDir { get; set; }

        public float AvSp { get; set; }

        public int Dir { get; set; }

        public int MxS { get; set; }

        public float SkyC { get; set; }

        public int MxR { get; set; }

        public int MnR { get; set; }

        public float AvSLP { get; set; }

        private static string _lineFormat = @"^(?<f1>.{2})(?<day>.{2})" +
                  "(?<f2>.{1})(?<maxTemp>.{3})" +
                  "(?<f3>.{3})(?<minTemp>.{3})" +
                  "(?<f4>.{3})(?<avgTemp>.{3})" +
                  "(?<f5>.{2})(?<hDDay>.{6})" +
                  "(?<f6>.{1})(?<avDP>.{5})" +
                  "(?<f7>.{1})(?<HrP>.{5})" +
                  "(?<f8>.{1})(?<TPcpn>.{4})" +
                  "(?<f9>.{1})(?<WxType>.{6})" +
                  "(?<f10>.{1})(?<PDir>.{4})" +
                  "(?<f11>.{1})(?<AvSp>.{4})" +
                  "(?<f12>.{1})(?<Dir>.{3})" +
                  "(?<f13>.{1})(?<MxS>.{3})" +
                  "(?<f14>.{1})(?<SkyC>.{4})" +
                  "(?<f15>.{1})(?<MxR>.{3})" +
                  "(?<f16>.{1})(?<MnR>.{2})" +
                  "(?<f15>.{1})(?<AvSLP>.+)$";

        private static Regex _lineRegex = new Regex(_lineFormat);


        public static bool TryParseFromLine<T>(string line, out WeatherRecord record)
        {
            Match match = _lineRegex.Match(line);

            int tempInt;
            if (int.TryParse(match.Groups["day"].Value, out tempInt))
            {
                float tempFloat;
                record = new WeatherRecord();
                record.Day = tempInt;
                record.MaxTemp = int.TryParse(match.Groups["maxTemp"].Value.Replace("*", ""), out tempInt) ? tempInt : 0;
                record.MinTemp = int.TryParse(match.Groups["minTemp"].Value.Replace("*", ""), out tempInt) ? tempInt : 0;
                record.AvgTemp = int.TryParse(match.Groups["avgTemp"].Value.Replace("*", ""), out tempInt) ? tempInt : 0;
                record.HDDay = match.Groups["hDDay"].Value.Trim();
                record.AvgDewPoint = float.TryParse(match.Groups["avDP"].Value, out tempFloat) ? tempFloat : 0;
                record.HrP1 = match.Groups["HrP"].Value;
                record.TPcpn = float.TryParse(match.Groups["TPcpn"].Value, out tempFloat) ? tempFloat : 0;
                record.WxType = match.Groups["WxType"].Value;
                record.PDir = int.TryParse(match.Groups["PDir"].Value, out tempInt) ? tempInt : 0;
                record.AvSp = float.TryParse(match.Groups["AvSP"].Value, out tempFloat) ? tempFloat : 0;
                record.Dir = int.TryParse(match.Groups["Dir"].Value, out tempInt) ? tempInt : 0;
                record.MxS = int.TryParse(match.Groups["MxS"].Value, out tempInt) ? tempInt : 0;
                record.SkyC = float.TryParse(match.Groups["SkyC"].Value, out tempFloat) ? tempFloat : 0;
                record.MxR = int.TryParse(match.Groups["MxR"].Value, out tempInt) ? tempInt : 0;
                record.MnR = int.TryParse(match.Groups["MnR"].Value, out tempInt) ? tempInt : 0;
                record.AvSLP = float.TryParse(match.Groups["AvSLP"].Value, out tempFloat) ? tempFloat : 0;
                return true;
            }

            record = null;
            return false;
        }
    }

}
