using FluentValidation;
using Microsoft.Extensions.Localization;
using Titan.UFC.ExceptionAndValidation;
using TitanTemplate.titanaddressapi.Helpers;
using TitanTemplate.titanaddressapi.LocalizationResource;

namespace TitanTemplate.titanaddressapi.Models
{
    /// <summary>
    /// Address Validatior Interface
    /// </summary>
    public interface IAddressValidator { }
    /// <summary>
    /// Address Validator Class
    /// </summary>
    public class AddressValidator : BaseValidator<Address>, IAddressValidator
    {
        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="sharedLocalizer"></param>
        public AddressValidator(IStringLocalizer<SharedResource> sharedLocalizer) : base()
        {
            RuleFor(m => m.AddressLine1)
               .NotEmpty()
               .WithErrorCode(ErrorTypes.RequiredError.ToString())
               .WithMessage(sharedLocalizer[SharedResourceKeys.AddressLine1Rquired]);

            RuleFor(m => m.AddressLine1)
              .MaximumLength(90)
              .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
              .WithMessage(sharedLocalizer[SharedResourceKeys.AddressLine1Length]);

            RuleFor(m => m.Name)
               .NotEmpty()
               .WithErrorCode(ErrorTypes.RequiredError.ToString())
               .WithMessage(sharedLocalizer[SharedResourceKeys.NameRquired]);
            RuleFor(m => ValidationHelper.CheckName(m.Name))
                .NotEqual(false)
                .WithErrorCode(ErrorTypes.RequiredError.ToString())
                .WithMessage(sharedLocalizer[SharedResourceKeys.NameLength]);
            
            RuleFor(m => m.AddressLine2)
             .MaximumLength(90)
             .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
             .WithMessage(sharedLocalizer[SharedResourceKeys.AddressLine2Length]);
            RuleFor(m => m.City)
             .MaximumLength(90)
             .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
             .WithMessage(sharedLocalizer[SharedResourceKeys.CityLength]);
            RuleFor(m => m.StateCode)
            .MaximumLength(90)
            .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
            .WithMessage(sharedLocalizer[SharedResourceKeys.StateCodeLength]);
            RuleFor(m => ValidationHelper.CheckLatitude(m.Latitude.ToString()))
                .NotEqual(false)
                .WithErrorCode(ErrorTypes.RequiredError.ToString())
                .WithMessage(sharedLocalizer[SharedResourceKeys.InvalidLatitudeValue]);
            RuleFor(m => ValidationHelper.CheckLongitude(m.Longitude.ToString()))
                .NotEqual(false)
                .WithErrorCode(ErrorTypes.RequiredError.ToString())
                .WithMessage(sharedLocalizer[SharedResourceKeys.InvalidLatitudeValue]);

        }



    }
}
