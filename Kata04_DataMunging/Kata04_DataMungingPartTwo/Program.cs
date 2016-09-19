using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kata04_DataMungingPartTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            // excercise goal = list team with smallest spread between points for and against

            List<FootballRecord> records = FetchData("football.dat");

            var minSpread = records.OrderBy(r => Math.Abs(r.PointsFor - r.PointsAgainst)).First();

            Console.WriteLine($"Team with min spread = {minSpread.TeamName}");

            Console.ReadLine();

        }

        private static List<FootballRecord> FetchData(string fileName)
        {
            List<FootballRecord> records = new List<FootballRecord>();

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("file was not found", fileName);
            }

            foreach (string line in File.ReadAllLines(fileName))
            {
                FootballRecord rec;
                if (FootballRecord.TryParseFromLine(line, out rec))
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

        public static bool TryParseFromLine(string line, out FootballRecord record)
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

            record = null;
            return false;
        }

    }

}
