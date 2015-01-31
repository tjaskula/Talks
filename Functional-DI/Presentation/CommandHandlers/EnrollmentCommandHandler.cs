using System;
using Domain.Commands;
using Infrastructure;

namespace Presentation.CommandHandlers
{
    public class EnrollmentCommandHandler : ICommandHandler<StudentEnrollCommand>,
                                            ICommandHandler<StudentUnrollCommand>
    {
        private readonly StudentRepository _studentRepository;
        private readonly ClassRepository _classRepository;
        private readonly StudentArchiveRepository _studentArchiveRepository;

        public EnrollmentCommandHandler(StudentRepository studentRepository,
                                        ClassRepository classRepository,
                                        StudentArchiveRepository studentArchiveRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _studentArchiveRepository = studentArchiveRepository;
        }

        public void Handles(StudentEnrollCommand command)
        {
            var student = _studentRepository.GetById(command.StudentId);
            var @class = _classRepository.GetById(command.ClassId);

            try
            {
                student.TryEnrollIn(@class);
                @class.TryEnroll(student);

                student.Enroll(@class);
                @class.Enroll(student);
            }
            catch (Exception e)
            {
                // log
            }
        }

        public void Handles(StudentUnrollCommand command)
        {
            var student = _studentRepository.GetById(command.StudentId);
            var @class = _classRepository.GetById(command.ClassId);
            var studentArchive = _studentArchiveRepository.GetById(command.StudentId);

            student.Unroll(@class);
            @class.Unroll(student);
            studentArchive.AddToPassedEnrollements(student.LastUnrolled, command.Reason);
        }
    }
}