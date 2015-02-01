using System;
using Domain.Commands;
using Infrastructure;
using Presentation.CommandHandlers;
using Presentation.Helpers;

namespace Presentation
{
    public class Bootstrapper
    {
        public Dispatcher<ICommand> RegisterDependencies()
        {
            var dispatcher = new Dispatcher<ICommand>();

            var studentRepository = new StudentRepository();
            var classRepository = new ClassRepository();

            var sh = new StudentEnrollmentHandlers();
            Action<StudentEnrollCommand> studentEnrollPipeline = c => sh.Enroll(studentRepository, classRepository, c);

            dispatcher.Subscribe(studentEnrollPipeline);

            return dispatcher;
        }
    }
}