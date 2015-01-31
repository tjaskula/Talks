using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Class
    {
        private const int DefaultMax = 25;
        private readonly int _id;
        private readonly DateTime _from;
        private readonly DateTime _to;
        private int _max;
        private int _currentCount;

        private Teacher _teacher;
        private readonly IList<Student> _students = new List<Student>();

        public Class(int id, DateTime from, DateTime to)
        {
            _max = DefaultMax;
            _id = id;
            _from = from;
            _to = to;
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public DateTime To
        {
            get
            {
                return _to;
            }
        }

        public DateTime From
        {
            get
            {
                return _from;
            }
        }

        public void Assign(Teacher teacher)
        {
            _max = teacher.Max;
            _teacher = teacher;
        }

        public void TryEnroll(Student student)
        {
            if (_currentCount >= _max)
                throw new Exception("Too many students.");

            if (_students.Any(s => s.Id == student.Id))
                throw new Exception("Student already enrolled.");
        }

        public void Enroll(Student student)
        {
            _students.Add(student);
            _currentCount = _students.Count;
        }

        public Enrollment GetEnrollmentFor(int studentId)
        {
            var enrollment = _students.SingleOrDefault(s => s.Id == studentId);
            if (enrollment == null) return Enrollment.Empty;

            return new Enrollment(_id, _from, _to);
        }

        public void Unroll(Student student)
        {
            _students.Remove(student);
        }
    }
}