using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.Models
{
    public class DescriptionValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;
            string[] words = fieldValue.Split(' ');
            if (words.Length < 5) 
            {
                return new ValidationResult("La descrizione deve contenere almeno 5 parole");
            }
            return ValidationResult.Success;
        }
    }
}
