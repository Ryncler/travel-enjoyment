using System.ComponentModel.DataAnnotations;

namespace SightsService.MinGans
{
    public interface IMinGanReplaceValidator
    {
        void Replace(object value, ValidationContext validationContext);
    }
}