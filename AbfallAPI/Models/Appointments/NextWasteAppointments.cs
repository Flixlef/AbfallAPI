using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbfallAPI.Models.Appointments
{
    public class NextWasteAppointments
    {
        public Street Street { get; set; }
        public List<WasteAppointment> WasteAppointments { get; set; }

        public NextWasteAppointments()
        {
            WasteAppointments = new List<WasteAppointment>();
        }

        public void OrderWasteAppointments()
        {
            WasteAppointments.OrderBy(x => x.Date);
        }
    }
}
