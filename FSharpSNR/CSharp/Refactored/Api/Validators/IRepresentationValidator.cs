using Api.Models;

namespace Api.Validators
{
    public interface IRepresentationValidator
    {
        bool Validate(RegisterRepresentation representation);
    }
}