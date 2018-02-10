using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AbfallAPI.Models.DAO
{
    public class YellowWasteAppointmentsDao
    {
        private XElement Data;

        public YellowWasteAppointmentsDao(string wwwPath)
        {
            string pathToFile = wwwPath + "/Data/YellowWasteAppointments.xml";
            Data = XDocument.Load(pathToFile).Root;
        }

        public DateTime GetNextYellowWasteAppointment(Street street)
        {
            return DateTime.ParseExact(Data.Descendants("Appointment")
                .Where(x => DateTime.Compare(DateTime.ParseExact(x.Element("Date").Value, "dd.MM.yyyy", new CultureInfo("de-DE")), DateTime.Now) >= 0)
                .Where(x => x.Element("ZoneId").Value == street.Zone.Id.ToString())
                .OrderBy(x => DateTime.ParseExact(x.Element("Date").Value, "dd.MM.yyyy", null))
                .FirstOrDefault()
                .Element("Date").Value, "dd.MM.yyyy", null
            );
        }
    }
}
