using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimerViewer {
    class IFFReader {

        public class TimerRec {
            public DateTime when { get; set; }
            public string job { get; set; }
            public string emp { get; set; }
            public string item { get; set; }
            public string pitem { get; set; }
            public bool billable { get; set; }
            public string duration { get; set; }
            public string notes { get; set; }
        }

        const string TIMER_HEADER = "TIMEACT";

        public static IEnumerable<TimerRec> ImportFile(string fileName) {
            var reader = new StreamReader(fileName);
            var input = reader.ReadToEnd();

            var transactionTypes = Regex.Split(input, @"^(!.*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            var us_culture = new CultureInfo("en-US");

            var res = new List<TimerRec>();

            foreach ( var txn in transactionTypes ) {
                var transactionType = txn.Replace("\r", "").Replace("\n", "").Trim();

                if( transactionType.StartsWith(TIMER_HEADER)) { 
                    var entries = txn.Split(new[] { TIMER_HEADER }, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 1);

                    foreach(var rec in entries) {
                        var cols = rec.Split('\t').ToList();

                        cols.RemoveAt(0); // get rid of first empty element

                        var when = DateTime.Parse(cols[0], us_culture); // parse US-style dates

                        res.Add( new TimerRec {
                            when = when,
                            job = cols[1],
                            emp = cols[2],
                            item = cols[3],
                            pitem = cols[4],
                            duration = cols[5],
                            notes = cols[7],
                            billable = cols[9].StartsWith("1"),
                        });
                    }

                }
            }

            return res;
        }
    }
}
