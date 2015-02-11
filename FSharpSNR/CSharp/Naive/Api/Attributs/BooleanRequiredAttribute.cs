using System.ComponentModel.DataAnnotations;

namespace Api.Attributs
{
    public class BooleanRequiredAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (bool)value;
        }
    }
}