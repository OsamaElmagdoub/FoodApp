using System.ComponentModel.DataAnnotations;

namespace FoodApp.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
