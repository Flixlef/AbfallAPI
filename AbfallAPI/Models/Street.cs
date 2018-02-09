using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AbfallAPI.Models
{
    public class Street
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Zone Zone { get; set; }
        public DayOfWeek OrganicWasteDay { get; set; }
        public DayOfWeek ResidualWasteDay { get; set; }
        public Region Region { get; set; }

        public Street() { }

        public Street(int id, string name, Zone zone, DayOfWeek organic, DayOfWeek residual, Region region)
        {
            Id = id;
            Name = name;
            Zone = zone;
            OrganicWasteDay = organic;
            ResidualWasteDay = residual;
            Region = region;
        }
    }
}
