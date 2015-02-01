using Domain.Commands;
using Infrastructure;

namespace Presentation.CommandHandlers
{
    public class StudentEnrollmentHandlers
    {
        public void Enroll(StudentRepository studentRepository, ClassRepository classRepository,
                            StudentEnrollCommand command)
        {
            var student = studentRepository.GetById(command.StudentId);
            var @class = classRepository.GetById(command.ClassId);

            student.TryEnrollIn(@class);
            @class.TryEnroll(student);

            student.Enroll(@class);
            @class.Enroll(student);
        }

        public void Unroll(StudentRepository studentRepository, ClassRepository classRepository,
                           StudentArchiveRepository studentArchiveRepository, StudentUnrollCommand command)
        {
            var student = studentRepository.GetById(command.StudentId);
            var @class = classRepository.GetById(command.ClassId);
            var studentArchive = studentArchiveRepository.GetById(command.StudentId);

            student.Unroll(@class);
            @class.Unroll(student);
            studentArchive.AddToPassedEnrollements(student.LastUnrolled, command.Reason);
        }
    }
}