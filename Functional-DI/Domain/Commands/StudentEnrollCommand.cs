using System;

namespace Domain.Commands
{
    public class StudentEnrollCommand : ICommand
    {
        public StudentEnrollCommand(int studentId, int classId, DateTime from, DateTime to)
        {
            From = @from;
            To = to;
            StudentId = studentId;
            ClassId = classId;
        }

        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public int StudentId { get; private set; }
        public int ClassId { get; private set; }
    }
}