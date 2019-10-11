using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Ufc.Addresses.API.Service
{
    public class BaseService
    {
        /// <summary>
        /// To produce custom validation error
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <returns>ValidationException</returns>
        protected ValidationException GetValidationException(string errorMessage)
        {
            var errorObject = new List<ValidationFailure>() { new ValidationFailure("NonProperty", errorMessage) { } };
            ValidationException exception = new ValidationException(errorObject);
            return exception;
        }

        /// <summary>
        /// Connvert string valued to GUID
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        protected Guid? ConvertToGuid(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Guid.Parse(value);
            }
            else
            {
                return null;
            }
        }
    }
}
