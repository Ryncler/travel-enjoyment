using System.ComponentModel.DataAnnotations;

namespace SightsService.MinGans
{
    public interface IMinGanCheckValidator
    {
        ValidationResult IsValid(object value, ValidationContext validationContext);
    }
}