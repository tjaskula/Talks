using System;
using Domain.Commands;
using Infrastructure;
using Presentation.CommandHandlers;
using Presentation.Helpers;

namespace Presentation
{
    public class Bootstrapper
    {
        private LifeTime _lifeTime;

        public Dispatcher<ICommand> RegisterDependencies()
        {
            var dispatcher = new Dispatcher<ICommand>();
            _lifeTime = new LifeTime();

            Func<StudentRepository> studentRepositoryFactory = () => new StudentRepository();
            Func<ClassRepository> classRepositoryFactory = () => new ClassRepository();

            var sh = new StudentEnrollmentHandlers();
            var ch = new CommonCommandHandlers();
            Action<StudentEnrollCommand> studentEnrollPipeline = c =>
                                                ch.Audit(c, c1 =>
                                                    ch.Log(c1, c2 =>
                                                        sh.Enroll(_lifeTime.PerWebRequest(studentRepositoryFactory),
                                                        _lifeTime.PerWebRequest(classRepositoryFactory), c2)));

            dispatcher.Subscribe(studentEnrollPipeline);

            return dispatcher;
        }

        public void Dispose()
        {
            _lifeTime.Dispose();
        }
    }
}