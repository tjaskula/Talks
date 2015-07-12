using System.Text.RegularExpressions;
using Api.Models;

namespace Api.Validators
{
    public class RepresentationValidator : IRepresentationValidator
    {
        public bool Validate(RegisterRepresentation representation)
        {
            if (representation == null)
                return false;
            return !Regex.IsMatch(representation.Password, @"(?!.*\s)[0-9a-zA-Z!@#\\$%*()_+^&amp;}{:;?.]*$");
        }
    }
}