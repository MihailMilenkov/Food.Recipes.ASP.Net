namespace FoodRecipes.Models.Cooks
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.CookConstants;

    public class BecomeCookFormModel
    {
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(
            NameMaxLength, 
            MinimumLength = NameMinLength,
            ErrorMessage = "The name bust be between {2} and {1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The phone number is required.")]
        [StringLength(
            PhoneNumberMaxLength, 
            MinimumLength = PhoneNumberMinLength,
            ErrorMessage = "The phone number bust be between {2} and {1} characters.")]
        [Display(Name= "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
