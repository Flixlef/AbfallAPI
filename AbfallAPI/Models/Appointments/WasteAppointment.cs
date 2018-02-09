using System;

namespace AbfallAPI.Models.Appointments
{
    public class WasteAppointment
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public WasteAppointment(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }
    }
}
