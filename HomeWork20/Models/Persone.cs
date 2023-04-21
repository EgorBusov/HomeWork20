using System.ComponentModel.DataAnnotations;

namespace HomeWork20.Models
{
    public class Persone
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
