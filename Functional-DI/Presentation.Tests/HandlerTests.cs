using System;
using Domain.Commands;
using Infrastructure;
using Presentation.CommandHandlers;
using Presentation.Helpers;
using Xunit;

namespace Presentation.Tests
{
    public class HandlerTests
    {
        [Fact]
        public void DeactivateCommandShouldDeactivateItem()
        {
            var dispatcher = new Dispatcher<ICommand>();

            var studentRepository = new StudentRepository();
            var classRepository = new ClassRepository();

            var sh = new StudentEnrollmentHandlers();
            Action<StudentEnrollCommand> studentEnrollPipeline = c => sh.Enroll(studentRepository, classRepository, c);

            dispatcher.Subscribe(studentEnrollPipeline);

            dispatcher.Dispatch(new StudentEnrollCommand(1, 2, DateTime.Now, DateTime.Now.AddMonths(6)));
        }

        [Fact]
        public void DeactivateCommandShouldDeactivateItemWithLog()
        {
            var dispatcher = new Dispatcher<ICommand>();
            var sh = new StudentEnrollmentHandlers();
            var ch = new CommonCommandHandlers();

            var studentRepository = new StudentRepository();
            var classRepository = new ClassRepository();

            Action<StudentEnrollCommand> studentEnrollPipeline
                                        = x => ch.Log(x, next => sh.Enroll(studentRepository, classRepository, next));

            dispatcher.Subscribe(studentEnrollPipeline);

            dispatcher.Dispatch(new StudentEnrollCommand(1, 2, DateTime.Now, DateTime.Now.AddMonths(6)));
        }

        [Fact]
        public void DeactivateCommandShouldDeactivateItemWithLogAndPerRequest()
        {
            var dispatcher = new Dispatcher<ICommand>();
            var sh = new StudentEnrollmentHandlers();
            var ch = new CommonCommandHandlers();
            var lifeTime = new LifeTime();

            Func<StudentRepository> studentRepositoryFactory = () => new StudentRepository();
            Func<ClassRepository> classRepositoryFactory = () => new ClassRepository();

            Action<StudentEnrollCommand> studentEnrollPipeline
                                        = x => ch.Log(x, next => sh.Enroll(lifeTime.PerThread(studentRepositoryFactory), 
                                                                           lifeTime.PerThread(classRepositoryFactory), next));

            dispatcher.Subscribe(studentEnrollPipeline);

            dispatcher.Dispatch(new StudentEnrollCommand(1, 2, DateTime.Now, DateTime.Now.AddMonths(6)));

            lifeTime.Dispose();
        }
    }
}