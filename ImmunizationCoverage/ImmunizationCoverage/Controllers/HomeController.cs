using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ImmunizationCoverage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public string GetCounties()
        {

            var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var path = baseDirectory + "CSVFiles\\CountyFullImmunizationCoverageRate.csv";

            var iop = System.IO.File.ReadAllLines(path).Skip(1).Take(24).Select(line => new { line, columns = line.Split(','), rows = line.Split(';') }).Select(@t => new
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
            var json1 = jsonSerialiser.Serialize(counties);
            return json1;
        }


        public string GetCoverage()
        {
            var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var path = baseDirectory + "CSVFiles\\CountyFullImmunizationCoverageRate.csv";

            var iop = System.IO.File.ReadAllLines(path).Skip(1).Select(line => new { line, columns = line.Split(','), rows = line.Split(';') }).Select(@t => new
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

            var json1 = jsonSerialiser.Serialize(coverage);
            return json1;
           
        }
    }
}