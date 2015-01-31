using Domain.Commands;

namespace Presentation.CommandHandlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        void Handles(T command);
    }
}