using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using AbfallAPI.Models;
using AbfallAPI.Models.Appointments;
using AbfallAPI.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbfallAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AppointmentsController : Controller
    {
        private ZoneDao Zones;
        private StreetDao Streets;
        private YellowWasteAppointmentsDao YellowWasteAppointments;
        private BlueWasteAppointmentDao BlueWasteAppointments;
        private ResidualWasteAppointmentDao ResidualWasteAppointments;
        private BabyWasteAppointmentsDao BabyWasteAppointments;

        public AppointmentsController()
        {
            Streets = new StreetDao();
            Zones = new ZoneDao();
            YellowWasteAppointments = new YellowWasteAppointmentsDao();
            BlueWasteAppointments = new BlueWasteAppointmentDao();
            ResidualWasteAppointments = new ResidualWasteAppointmentDao();
            BabyWasteAppointments = new BabyWasteAppointmentsDao();
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
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Gelber Sack", GetNextYellowWasteAppointment(street)));
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Blaue Tonne", GetNextBlueWasteAppointment(street)));
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Graue Tonne", GetNextGreyWasteAppointment(street)));
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Windelsack", GetNextBabyWasteAppointment(street)));
            nextWasteAppointments.WasteAppointments.Add(new WasteAppointment("Biotonne", GetNextOrganicWasteAppointment(street)));

            return new ObjectResult(nextWasteAppointments);
        }

        [Route("Streets")]
        public IActionResult GetStreets()
        {
            return new ObjectResult(Streets.GetAllStreets());
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