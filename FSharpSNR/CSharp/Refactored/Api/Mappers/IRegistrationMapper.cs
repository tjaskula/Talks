using Domain;
using Api.Models;

namespace Api.Mappers
{
    public interface IRegistrationMapper
    {
        Account Map(RegisterRepresentation representation);
    }
}