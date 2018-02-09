using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AbfallAPI.Models.DAO
{
    public class StreetDao
    {
        private XElement Data;
        private ZoneDao ZoneDao;

        public StreetDao(string wwwPath)
        {
            ZoneDao = new ZoneDao(wwwPath);
            string pathToFile = wwwPath + "/Data/Streets.xml";
            Data = XDocument.Load(pathToFile).Root;
        }

        public Street GetStreet(string id)
        {
            try
            {
                XElement Street = Data.Descendants("Street").Where(x => x.Element("Id").Value == id).First();
                return XElementToStreet(Street);
            } catch(Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> GetAllStreets()
        {
            return Data.Descendants("Street")
                .Select(x => new
                {
                    Key = x.Element("Id").Value,
                    Value = x.Element("Name").Value
                })
                .ToDictionary(k => k.Key, v => v.Value);
        }

        public Street XElementToStreet(XElement Street)
        {
            int Id = Int32.Parse(Street.Element("Id").Value);
            string Name = Street.Element("Name").Value;
            Zone Zone = ZoneDao.GetZone(Street.Element("ZoneId").Value);
            DayOfWeek Organic = (DayOfWeek)Int32.Parse(Street.Element("OrganicWasteDay").Value);
            DayOfWeek Residual = (DayOfWeek)Int32.Parse(Street.Element("ResidualWasteDay").Value);
            Region Region = (Region)Int32.Parse(Street.Element("Region").Value);
            
            return new Street(Id, Name, Zone, Organic, Residual, Region);
        }
    }
}
