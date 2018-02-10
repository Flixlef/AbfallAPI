using System;
using System.Collections.Generic;
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
                .Where(x => DateTime.ParseExact(x.Element("Date").Value, "dd.mm.yyyy", null) >= DateTime.ParseExact(now, "dd.mm.yyyy", null))
                .Where(x => x.Element("Zone").Value == street.Zone.Id.ToString())
                .OrderBy(x => DateTime.ParseExact(x.Element("Date").Value, "dd.mm.yyyy", null))
                .FirstOrDefault()
                .Element("Date").Value, "dd.mm.yyyy", null
            );
        }
    }
}
