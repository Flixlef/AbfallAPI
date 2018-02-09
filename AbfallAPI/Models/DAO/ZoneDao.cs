using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Xml.Linq;

namespace AbfallAPI.Models.DAO
{
    public class ZoneDao
    {
        private XElement Data;

        public ZoneDao(string wwwPath)
        {
            string pathToFile = wwwPath + "/Data/Zones.xml";
            Data = XDocument.Load(pathToFile).Root;
        }

        public Zone GetZone(string id)
        {
            XElement Zone = Data.Descendants("Zone").Where(x => x.Element("Id").Value == id).First();
            return XElementToZone(Zone);
        }

        private Zone XElementToZone(XElement Zone)
        {
            int Id = Int32.Parse(Zone.Element("Id").Value);
            string Name = Zone.Element("Name").Value;

            return new Zone(Id, Name);
        }
    }
}
