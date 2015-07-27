using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ImmunizationCoverage.Controllers
{


    [RoutePrefix("immunizationcoverage")]
    public class ImmunizationCoverageController : ApiController
    {

        [Route("counties")]
        public object GetCounties()
        {

            var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var path = baseDirectory + "CSVFiles\\CountyFullImmunizationCoverageRate.csv";

            var iop = File.ReadAllLines(path).Skip(1).Take(24).Select(line => new { line, columns = line.Split(','), rows = line.Split(';') }).Select(@t => new
            {
                County = @t.columns[0],
            }).ToList();
          
            List<string> counties = new List<string>();
            foreach (var v in iop)
            {
                counties.Add(v.County);
            }
            var jsonSerialiser = new JavaScriptSerializer();
            
            foreach (var c in counties)
            {
                var json = jsonSerialiser.Serialize(c);
            }

            return counties;
        }



        [Route("coverage")]
        public object GetCoverage()
        {
            var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var path = baseDirectory + "CSVFiles\\CountyFullImmunizationCoverageRate.csv";

            var iop = File.ReadAllLines(path).Skip(1).Select(line => new { line, columns = line.Split(','), rows = line.Split(';') }).Select(@t => new
            {
                 year1 = @t.columns[1],
                 year2 = @t.columns[2],
                 year3 = @t.columns[3]
            }).ToList();

            var coverage = new List<List<float>>();

            var coverageYr1 = iop.Select(v => float.Parse(v.year1)).ToList();
            coverage.Add(coverageYr1);
            var coverageYr2 = iop.Select(v => float.Parse(v.year2)).ToList();
            coverage.Add(coverageYr2);
            var coverageYr3 = iop.Select(v => float.Parse(v.year3)).ToList();
            coverage.Add(coverageYr3);
            var jsonSerialiser = new JavaScriptSerializer();
            foreach (var c in coverage)
            {
                var json = jsonSerialiser.Serialize(c);
            }

            return coverage;
        }

        [Route("period")]
        public object GetPeriod()
        {

            var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var path = baseDirectory + "CSVFiles\\CountyFullImmunizationCoverageRate.csv";

            var iop = File.ReadAllLines(path).Take(1).Select(line => new { line, columns = line.Split(','), rows = line.Split(';') }).Select(@t => new
            {
                header = @t.rows[0],
            }).ToList();

            List<string> periods = new List<string>();
            foreach (var v in iop)
            {
                periods.Add(v.header);
            }
            var jsonSerialiser = new JavaScriptSerializer();

            foreach (var c in periods)
            {
                var json = jsonSerialiser.Serialize(c);
            }

            return periods;
        }

    }
}
