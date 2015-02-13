using Api.Models;
using Domain;

namespace Api.Mappers
{
    public class RegistrationMapper : IRegistrationMapper
    {
        public Account Map(RegisterRepresentation representation)
        {
            return new Account(representation.Email, representation.Password, representation.Provider);
        }
    }
}