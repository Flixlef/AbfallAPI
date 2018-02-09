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

        public BlueWasteAppointmentDao()
        {
            string pathToFile = "Data/BlueWasteAppointments.xml";
            Data = XDocument.Load(pathToFile).Root;
        }

        public DateTime GetNextBlueWasteAppointment(Street street)
        {
            return DateTime.Parse(Data.Descendants("Appointment")
                .Where(x => DateTime.Parse(x.Element("Date").Value) >= DateTime.Now.Date)
                .Where(x => x.Element("Zone").Value == street.Zone.Id.ToString())
                .OrderBy(x => DateTime.Parse(x.Element("Date").Value))
                .FirstOrDefault()
                .Element("Date").Value
            );
        }
    }
}
