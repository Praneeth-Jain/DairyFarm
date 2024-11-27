using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class SubMenuItems
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int MenuItemId {  get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public int Order {  get; set; }
     }
}
