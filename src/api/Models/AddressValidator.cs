using FluentValidation;
using Microsoft.Extensions.Localization;
using Titan.UFC.ExceptionAndValidation;
using Titan.Ufc.Addresses.API.Helpers;
using Titan.Ufc.Addresses.API.Resources;

namespace Titan.Ufc.Addresses.API.Models
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
        public AddressValidator(ISharedResource sharedLocalizer) : base()
        {
            RuleFor(m => m.Address1)
               .NotEmpty()
               .WithErrorCode(ErrorTypes.RequiredError.ToString())
               .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Address1_Required]);

            RuleFor(m => m.Address1)
              .MaximumLength(100)
              .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
              .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Address1_MaxLength]);

            RuleFor(m => m.Address2)
          .NotEmpty()
          .WithErrorCode(ErrorTypes.RequiredError.ToString())
          .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Address2_Required]);
       
            RuleFor(m => m.Address2)
          .MaximumLength(100)
          .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
          .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Address2_MaxLength]);           

            RuleFor(m => m.AddressLine3)
          .MaximumLength(100)
          .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
          .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Address3_MaxLength]);

            RuleFor(m => m.ContactName)
       .MaximumLength(100)
       .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
       .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Contact_Name_Length]);

            RuleFor(m => ValidationHelper.CheckName(m.ContactName))
           .NotEqual(false)
           .WithErrorCode(ErrorTypes.InvalidStringError.ToString())
           .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Contact_Name_Invalid]);

            RuleFor(m => m.ContactName)
               .NotEmpty()
               .WithErrorCode(ErrorTypes.RequiredError.ToString())
               .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Contact_Name_Required]);

            RuleFor(m => ValidationHelper.CheckName(m.ContactName))
                .NotEqual(false)
                .WithErrorCode(ErrorTypes.RequiredError.ToString())
                .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Contact_Name_Invalid]);

            RuleFor(m => m.City)
             .MaximumLength(50)
             .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
             .WithMessage(sharedLocalizer[SharedResourceKeys.Address_City_MaxLength]);

         
            RuleFor(m => m.City)
        .NotEmpty()
        .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
        .WithMessage(sharedLocalizer[SharedResourceKeys.Address_City_Name_Required]);


            RuleFor(m => ValidationHelper.CheckName(m.City))
              .NotEqual(false)
              .WithErrorCode(ErrorTypes.InvalidStringError.ToString())
              .WithMessage(sharedLocalizer[SharedResourceKeys.Address_City_Invalid]);
       

            RuleFor(m => ValidationHelper.CheckLatitude(m.Latitude.ToString()))
            .NotEqual(false)
            .WithErrorCode(ErrorTypes.InvalidNumberError.ToString())
            .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Latitude_Invalid]);

            RuleFor(m => ValidationHelper.CheckLongitude(m.Longitude.ToString()))
         .NotEqual(false)
         .WithErrorCode(ErrorTypes.InvalidNumberError.ToString())
         .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Longitude_Invalid]);


            RuleFor(m => m.PinCode)
        .NotEmpty()
        .WithErrorCode(ErrorTypes.RequiredError.ToString())
        .WithMessage(sharedLocalizer[SharedResourceKeys.Address_PostalCode_Required].Value);

            RuleFor(m => m.PinCode)
            .MinimumLength(5)
            .WithErrorCode(ErrorTypes.MinLengthError.ToString())
            .WithMessage(sharedLocalizer[SharedResourceKeys.Address_PostalCode_MinLength].Value);

            RuleFor(m => m.PinCode)
           .MaximumLength(10)
           .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
           .WithMessage(sharedLocalizer[SharedResourceKeys.Address_PostalCode_MaxLength].Value);

            RuleFor(m => m.CountryCode)
         .MaximumLength(2)
         .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
         .WithMessage(sharedLocalizer[SharedResourceKeys.Address_CountryCode_MaxLength].Value);

            RuleFor(m => m.StateCode)
            .MinimumLength(2)
            .WithErrorCode(ErrorTypes.MinLengthError.ToString())
            .WithMessage(sharedLocalizer[SharedResourceKeys.Address_StateCode_MinLength].Value);

            RuleFor(m => m.StateCode)
           .MaximumLength(3)
           .WithErrorCode(ErrorTypes.MaxLengthError.ToString())
           .WithMessage(sharedLocalizer[SharedResourceKeys.Address_StateCode_MaxLength].Value);


            RuleFor(m => m.StateCode)
           .NotEmpty()
           .WithErrorCode(ErrorTypes.RequiredError.ToString())
           .WithMessage(sharedLocalizer[SharedResourceKeys.Address_State_Code_Required].Value);

           
            RuleFor(m => m.CountryCode)
           .NotEmpty()
           .WithErrorCode(ErrorTypes.RequiredError.ToString())
           .WithMessage(sharedLocalizer[SharedResourceKeys.Address_Country_Code_Is_Required].Value);


        }
    }
}
