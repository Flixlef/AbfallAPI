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
            return DateTime.Parse(Data.Descendants("Appointment")
                .Where(x => DateTime.ParseExact(x.Element("Date").Value, "dd.mm.yyyy", null) >= DateTime.Now.Date)
                .Where(x => x.Element("Zone").Value == street.Zone.Id.ToString())
                .OrderBy(x => DateTime.ParseExact(x.Element("Date").Value, "dd.mm.yyyy", null))
                .FirstOrDefault()
                .Element("Date").Value
            );
        }
    }
}
