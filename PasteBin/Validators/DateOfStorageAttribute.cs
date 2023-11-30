using PasteBin.Data;
using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.Validators
{
    public class DateOfStorageAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if(date<= DateTime.Now)
            {
                return new ValidationResult("Истёк срок действия ссылки. Свяжитесь с владельцем поста для продления ссылки");
            }
            return ValidationResult.Success;
        }
    }
}
