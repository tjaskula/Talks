using Domain;

namespace Infrastructure
{
    public class StudentRepository
    {
        public Student GetById(int id)
        {
            return new Student(1);
        }
    }
}