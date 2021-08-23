namespace FoodRecipes.Data
{
    public class DataConstants
    {
        public class UserConstants
        {
            public const int FullNameMinLength = 2;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 20;
        }

        public class CookConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;

            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 20;
        }

        public class RecipeConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;

            public const int IngredientsMinLength = 10;
            public const int IngredientsMaxLength = 500;

            public const int DirectionsMinLength = 15;
            public const int DirectionsMaxLength = 1000;

            public const int MinCookingTime = 0;
            public const int MaxCookingTime = 600;
        }

        public class RecipeCategoryConstants
        {
            public const int NameMaxLength = 25;
        }

        public class IngredientCategoryConstants
        {
            public const int NameMaxLength = 25;
        }

        public class SellerConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;

            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 20;
        }
        public class IngredientConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;

            public const int CountryOfOriginMinLength = 5;
            public const int CountryOfOriginMaxLength = 20;
        }
    }
}
