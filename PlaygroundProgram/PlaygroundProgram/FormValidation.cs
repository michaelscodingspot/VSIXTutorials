using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundProgram
{
    public class FormValidation
    {
        public void ValidateDetails(FormDetails details)
        {
            if (details.FirstName.Length < 2)
            {
                throw new InvalidDetailsException("Invalid length", nameof(details.FirstName));
            }

            if (details.LastName.Length < 2)
            {
                throw new InvalidDetailsException("Invalid length", nameof(details.LastName));
            }

            if (!details.AgreeToTermsAndConditions)
            {
                throw new InvalidDetailsException("Terms and Conditions must be agreed to.", 
                    nameof(details.AgreeToTermsAndConditions));
            }

            if (details.Age < 18)
            {
                throw new InvalidDetailsException("Age must be more than 18.", nameof(details.Age));
            }

            if (details.Password.Length < 5)
            {
                throw new InvalidDetailsException("Invalid length.", nameof(details.Password));
            }

            if (!ContainsBothSmallAndBigLetters(details.Password))
            {
                throw new InvalidDetailsException("Password must contain both small letters and big letters.", nameof(details.Password));
            }

        }



















        private bool ContainsBothSmallAndBigLetters(string password)
        {
            throw new NotImplementedException();
        }
    }
}
