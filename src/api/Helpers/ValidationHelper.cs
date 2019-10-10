using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanTemplate.titanaddressapi.Helpers
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Check the name of the address is valid
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CheckName(string name)
        {
            bool retVal = true;

            if (!string.IsNullOrEmpty(name))
            {
                retVal = !System.Text.RegularExpressions.Regex.IsMatch(name, "[^a-zA-Z\\s]");
            }
            return retVal;
        }
        /// <summary>
        /// Check the latitude
        /// </summary>
        /// <param name="lalitude"></param>
        /// <returns></returns>
        public static bool CheckLatitude(string lalitude)
        {
            bool retVal = false;


            
            var latitudeRegx = @"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$";

            if (!string.IsNullOrEmpty(lalitude))
            {
                retVal = System.Text.RegularExpressions.Regex.IsMatch(lalitude, latitudeRegx);
            }
            return retVal;
        }
        /// <summary>
        /// Check longidude
        /// </summary>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static bool CheckLongitude(string longitude)
        {
            bool retVal = false;
            var longitudeRegx = @"^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$";

            if (!string.IsNullOrEmpty(longitude))
            {
                retVal = System.Text.RegularExpressions.Regex.IsMatch(longitude, longitudeRegx);
            }
            return retVal;
        }

        /// <summary>
        /// Check maximum length for a string
        /// </summary>
        /// <param name="content"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static bool CheckMaximumLength(string content, int maxLength)
        {
            bool retVal = true;

            if (!string.IsNullOrEmpty(content) && content.Length > maxLength)
            {
                retVal = false;
            }

            return retVal;
        }
        /// <summary>
        /// Check maximum length for a string
        /// </summary>
        /// <param name="content"></param>
        /// <param name="minLength"></param>
        /// <returns></returns>
        public static bool CheckMinimumLength(string content, int minLength)
        {
            bool retVal = true;

            if (!string.IsNullOrEmpty(content) && content.Length < minLength)
            {
                retVal = false;
            }

            return retVal;
        }
    }
}
