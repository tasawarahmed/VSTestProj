using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace IttefaqConstructionServices.App_Code
{
    public class Validator
    {
        /*-----------------------------------------------------------
         * -----------------------Error Messages---------------------
         * ----------------------------------------------------------*/
        public static string ArgumentExceptionMessage()
        {
            return "There is some error processing inputs. ";
        }

        public static string ArgumentExceptionMessage2()
        {
            return " Please ensure that you fill in all required fields correctly before saving the record.";
        }

        public static string InnerExceptionMessage(string ex)
        {
            return "There occurred some inner exception. " + ex + " Please consult your maintenance team and retry.";
        }

        /*-----------------------------------------------------------
         * -----------------------Public Validators------------------
         * ----------------------------------------------------------*/

        public static string ValidateFloatRange(string controlValue, string minValue, string maxValue, string control)
        {
            if (float.Parse(controlValue) <= float.Parse(maxValue) && float.Parse(controlValue) >= float.Parse(minValue))
            {
                return controlValue;
            }
            else
            {
                throw new ArgumentException("One of the supplied " + control + " Marks of Students lie beyond the acceptable range.");
            }
        }

        public static string ValidatePositiveNumber(string input, string control)
        {
            if (!IsPositiveNumber(input))
            {
                throw new ArgumentException("Only Positive Numbers are acceptable in Text Box " + control);
            }
            else
            {
                return input;
            }
        }

        public static string ValidatePositiveInteger(string input, string control)
        {
            if (!IsPositiveInteger(input))
            {
                throw new ArgumentException("Not a Valid Number for this selection in Text Box " + control);
            }
            else
            {
                return input;
            }
        }

        public static string ValidateInteger(string input, string control)
        {
            if (!IsInteger(input))
            {
                throw new ArgumentException("Not a Valid Number for this selection in Text Box " + control);
            }
            else
            {
                return input;
            }
        }

        public static string ValidateName(string input, string control)
        {
            if (!IsName(input))
            {
                throw new ArgumentException("Not seems to be a Valid Name in Text Box " + control);
            }
            else
            {
                return input;
            }
        }

        public static string ValidatePhoneNumber(string input, string control)
        {
            if (!IsPositiveInteger(input))
            {
                throw new ArgumentException("Not a Valid Phone Number in Text Box " + control);
            }
            else
            {
                return input;
            }
        }

        public static string ValidateEmail(string input, string control)
        {
            if (!IsEmail(input))
            {
                throw new ArgumentException("Not seems to be a Valid Email in Text Box " + control);
            }
            else
            {
                return input;
            }
        }

        public static string ValidateAddress(string input, string control)
        {
            if (!IsAddress(input))
            {
                throw new ArgumentException("Not seems to be a Valid Address in Text Box " + control);
            }
            else
            {
                return input;
            }
        }

        public static string ValidateCNIC(string input, string control)
        {
            if (!IsCNIC(input))
            {
                throw new ArgumentException("Not seems to be a Valid National Identity Card Number In Text Box " + control);
            }
            else
            {
                return input;
            }
        }

        public static DateTime ValidateDate(string date, string control)
        {
            if (!isMatch(date))
            {
                throw new ArgumentException("Not seems to be a Valid Date Format in Text Box" + control);
            }

            if (!IsDate(date))
            {
                throw new ArgumentException("Not seems to be a Valid Date in Text Box" + control);
            }
            else
            {
                return DateTime.Parse(date);
            }
        }

        private static bool isMatch(string date)
        {
            Regex datePattern = new Regex(@"^(([0-9])|([0-2][0-9])|([3][0-1]))\-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec|January|February|March|April|May|June|July|August|September|October|November|December)\-\d{4}$");
            return datePattern.IsMatch(date);
        }

        /*-----------------------------------------------------------
         * -----------------------Validating Functions---------------
        * ----------------------------------------------------------*/

        private static bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) && objNaturalPattern.IsMatch(strNumber);
        }

        // Function to test for Positive Integers with zero inclusive
        private static bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }

        // Function to Test for Positive Integers
        private static bool IsPositiveInteger(String strNumber)
        {
            Regex objIntPattern = new Regex("^[0-9]+$|^[0-9]+$");
            return objIntPattern.IsMatch(strNumber);
        }

        // Function to Test for Integers both Positive & Negative
        private static bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }

        // Function to Test for Positive Number both Integer & Real
        private static bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) && objPositivePattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber);
        }

        // Function to test whether the string is valid number or not
        private static bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber) && !objTwoMinusPattern.IsMatch(strNumber) && objNumberPattern.IsMatch(strNumber);
        }

        // Function To test for Alphabets.
        private static bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        // Function to Check for AlphaNumeric.
        private static bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        // Function to Check for Address.
        private static bool IsAddress(String strToCheck)
        {
            Regex objAddressPattern = new Regex(@"[^)(a-zA-Z0-9,/ '-]");
            return !objAddressPattern.IsMatch(strToCheck);
        }

        // Function to Check for Valid Names
        private static bool IsName(String strToCheck)
        {
            Regex objNamePattern = new Regex(@"^[a-zA-Z'-.\s]{1,40}$");
            return objNamePattern.IsMatch(strToCheck);
        }

        // Function to Check for Valid Emails
        private static bool IsEmail(String strToCheck)
        {
            Regex objEmailPattern = new Regex(@"[\w-]+@([\w-]+\.)+[\w-]+");
            return objEmailPattern.IsMatch(strToCheck);
        }

        // Function to Check for Valid CNIC
        private static bool IsCNIC(String strToCheck)
        {
            Regex objCNICPattern = new Regex(@"(^\d{11}$)|(^\d{13}$)");
            return objCNICPattern.IsMatch(strToCheck);
        }

        private static bool IsDate(string input)
        {
            DateTime date;
            if (DateTime.TryParse(input, out date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}