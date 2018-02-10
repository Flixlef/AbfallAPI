using System;
using System.Collections.Generic;
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
            string now = DateTime.Now.ToString("dd.mm.yyyy");

            return DateTime.ParseExact(Data.Descendants("Appointment")
                .Where(x => DateTime.ParseExact(x.Element("Date").Value, "dd.mm.yyyy", null) >= DateTime.ParseExact(now, "dd.mm.yyyy", null))
                .Where(x => x.Element("ZoneId").Value == street.Zone.Id.ToString())
                .OrderBy(x => DateTime.ParseExact(x.Element("Date").Value, "dd.mm.yyyy", null))
                .FirstOrDefault()
                .Element("Date").Value, "dd.mm.yyyy", null
            );
        }
    }
}
