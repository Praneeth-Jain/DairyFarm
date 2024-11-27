using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class MenuItems
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Order { get; set; }
    }
}
