using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AbfallAPI.Models.DAO
{
    public class ResidualWasteAppointmentDao
    {
        private XElement Data;

        public ResidualWasteAppointmentDao(string wwwPath)
        {
            string pathToFile = wwwPath + "/Data/GreyWasteAppointments.xml";
            Data = XDocument.Load(pathToFile).Root;
        }

        public bool IsEvenWeek(Street street)
        {
            return Boolean.Parse(Data.Descendants("Appointment")
                .Where(x => (Region)(Int32.Parse(x.Element("Region").Value)) == street.Region)
                .First()
                .Element("Even").Value
            );
        }
    }
}
