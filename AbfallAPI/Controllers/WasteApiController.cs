using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using AbfallAPI.Models;
using AbfallAPI.Models.Appointments;
using AbfallAPI.Models.DAO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbfallAPI.Controllers
{
    [Produces(contentType: "application/json")]
    [Route("api/[controller]")]
    public class AppointmentsController : Controller
    {
        private IHostingEnvironment _env;
        private ZoneDao Zones;
        private StreetDao Streets;
        private YellowWasteAppointmentsDao YellowWasteAppointments;
        private BlueWasteAppointmentDao BlueWasteAppointments;
        private ResidualWasteAppointmentDao ResidualWasteAppointments;
        private BabyWasteAppointmentsDao BabyWasteAppointments;

        public AppointmentsController(IHostingEnvironment env)
        {
            _env = env;
            string WwwPath = _env.WebRootPath;
            Streets = new StreetDao(WwwPath);
            Zones = new ZoneDao(WwwPath);
            YellowWasteAppointments = new YellowWasteAppointmentsDao(WwwPath);
            BlueWasteAppointments = new BlueWasteAppointmentDao(WwwPath);
            ResidualWasteAppointments = new ResidualWasteAppointmentDao(WwwPath);
            BabyWasteAppointments = new BabyWasteAppointmentsDao(WwwPath);
        }

        public IActionResult GetAll(string id)
        {
            Street street;
            NextWasteAppointments nextWasteAppointments = new NextWasteAppointments();
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }
                
            try
            {
                street = Streets.GetStreet(id);
            } catch(Exception )
            {
                return NotFound();
            }

            nextWasteAppointments.Street = street;
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Gelber Sack", GetNextYellowWasteAppointment(street), "yellow"));
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Blaue Tonne", GetNextBlueWasteAppointment(street), "blue"));
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Graue Tonne", GetNextGreyWasteAppointment(street), "grey"));
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Windelsack", GetNextBabyWasteAppointment(street), "baby"));
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Biotonne", GetNextOrganicWasteAppointment(street), "organic"));

            return new ObjectResult(nextWasteAppointments);
        }

        [Route("streets")]
        public IActionResult GetStreets()
        {
            return new ObjectResult(Streets.GetAllStreets());
        }

        [Route("test")]
        public IActionResult GetTest()
        {
            return new ObjectResult(1);
        }

        private DateTime GetNextYellowWasteAppointment(Street street)
        {
            return YellowWasteAppointments.GetNextYellowWasteAppointment(street);
        }

        private DateTime GetNextGreyWasteAppointment(Street street)
        {
            bool EvenWeek = ResidualWasteAppointments.IsEvenWeek(street);
            return Helpers.GetNextDay(DateTime.Now, street.ResidualWasteDay, EvenWeek).Date;
        }

        private DateTime GetNextBabyWasteAppointment(Street street)
        {
            bool EvenWeek = BabyWasteAppointments.IsEvenWeek(street);
            return Helpers.GetNextDay(DateTime.Now, street.ResidualWasteDay, EvenWeek).Date;
        }

        private DateTime GetNextBlueWasteAppointment(Street street)
        {
            return BlueWasteAppointments.GetNextBlueWasteAppointment(street);
        }

        private DateTime GetNextOrganicWasteAppointment(Street street)
        {
            return Helpers.GetNextDay(DateTime.Now, street.OrganicWasteDay).Date;
        }
    }
}