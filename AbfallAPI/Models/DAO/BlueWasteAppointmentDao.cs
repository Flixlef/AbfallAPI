using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AbfallAPI.Models.DAO
{
    public class BlueWasteAppointmentDao
    {
        private XElement Data;

        public BlueWasteAppointmentDao(string wwwPath)
        {
            string pathToFile = wwwPath + "/Data/BlueWasteAppointments.xml";
            Data = XDocument.Load(pathToFile).Root;
        }

        public DateTime GetNextBlueWasteAppointment(Street street)
        {
            string now = DateTime.Now.ToString("dd.mm.yyyy");

            return DateTime.ParseExact(Data.Descendants("Appointment")
                .Where(x => DateTime.Compare(DateTime.ParseExact(x.Element("Date").Value, "dd.MM.yyyy", new CultureInfo("de-DE")), DateTime.Now) >= 0)
                .Where(x => x.Element("Zone").Value == street.Zone.Id.ToString())
                .OrderBy(x => DateTime.ParseExact(x.Element("Date").Value, "dd.MM.yyyy", null))
                .FirstOrDefault()
                .Element("Date").Value, "dd.MM.yyyy", null
            );
        }
    }
}
