using Domain.Commands;
using Presentation.Helpers;

namespace Presentation
{
    public class Bootstrapper
    {
        public Dispatcher<ICommand> RegisterDependencies()
        {
            var dispatcher = new Dispatcher<ICommand>();

            return dispatcher;
        }
    }
}