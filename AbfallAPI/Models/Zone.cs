using System.ComponentModel.DataAnnotations.Schema;

namespace AbfallAPI.Models
{
    public class Zone
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Zone() { }

        public Zone(int id, string name)
        { 
            Id = id;
            Name = name;
        }
    }
}