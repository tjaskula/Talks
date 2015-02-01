using Xunit;

namespace Presentation.Tests
{
    public class HandlerTests
    {
        [Fact]
        public void DeactivateCommandShouldDeactivateItem()
        {
            //var dispatcher = new Dispatcher<ICommand>();
            //var h = new StudentEnrollmentHandlers();

            //Action<DeactivateCommand> nodependsComposable = x => h.Deactivate(() => new ItemRepository(), x);

            //dispatcher.Subscribe(nodependsComposable);

            //dispatcher.Dispatch(new DeactivateCommand(5));
        }

        [Fact]
        public void DeactivateCommandShouldDeactivateItemWithLog()
        {
            //var dispatcher = new Dispatcher<ICommand>();
            //var h = new StudentEnrollmentHandlers();

            //Action<DeactivateCommand> nodependsLogged
            //                            = x => h.Log(x, next => h.Deactivate(() => new ItemRepository(), next));

            //dispatcher.Subscribe(nodependsLogged);

            //dispatcher.Dispatch(new DeactivateCommand(5));
        }
    }
}