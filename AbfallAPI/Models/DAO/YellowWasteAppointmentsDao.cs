﻿using System;
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
            return DateTime.Parse(Data.Descendants("Appointment")
                .Where(x => DateTime.ParseExact(x.Element("Date").Value, "dd.mm.yyyy", null) >= DateTime.Now.Date)
                .Where(x => x.Element("ZoneId").Value == street.Zone.Id.ToString())
                .OrderBy(x => DateTime.Parse(x.Element("Date").Value))
                .FirstOrDefault()
                .Element("Date").Value
            );
        }
    }
}
