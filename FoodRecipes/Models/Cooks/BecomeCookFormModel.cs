namespace FoodRecipes.Models.Cooks
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.CookConstants;

    public class BecomeCookFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name= "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
