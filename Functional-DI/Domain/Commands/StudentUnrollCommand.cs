namespace Domain.Commands
{
    public class StudentUnrollCommand : ICommand
    {
        public StudentUnrollCommand(int studentId, int classId, string reason)
        {
            StudentId = studentId;
            ClassId = classId;
            Reason = reason;
        }

        public int StudentId { get; private set; }
        public int ClassId { get; private set; }

        public string Reason { get; private set; }
    }
}