using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models
{
    public class Reservation
    {
        
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string EndLocation { get; set; } = string.Empty;
    }
}
