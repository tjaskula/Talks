//using System;
//using Console;
//using Xunit;

//namespace Domain.Tests
//{
//    public class HandlerTests
//    {
//        [Fact]
//        public void DeactivateCommandShouldDeactivateItem()
//        {
//            var dispatcher = new Dispatcher<ICommand>();
//            var h = new Handlers();

//            Action<DeactivateCommand> nodependsComposable = x => h.Deactivate(() => new ItemRepository(), x);

//            dispatcher.Subscribe(nodependsComposable);

//            dispatcher.Dispatch(new DeactivateCommand(5));
//        }

//        [Fact]
//        public void DeactivateCommandShouldDeactivateItemWithLog()
//        {
//            var dispatcher = new Dispatcher<ICommand>();
//            var h = new Handlers();

//            Action<DeactivateCommand> nodependsLogged 
//                                        = x => h.Log(x, next => h.Deactivate(() => new ItemRepository(), next));

//            dispatcher.Subscribe(nodependsLogged);

//            dispatcher.Dispatch(new DeactivateCommand(5));
//        }
//    }
//}