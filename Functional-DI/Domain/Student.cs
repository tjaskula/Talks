using System;

namespace Domain
{
    public class Student : IEquatable<Student>
    {
        private readonly TimeTable _timeTable;
        public Student(int id)
        {
            Id = id;
            LastUnrolled = Enrollment.Empty;
            _timeTable = new TimeTable();
        }

        public int Id { get; private set; }

        public void TryEnrollIn(Class c)
        {
            if (_timeTable.OverlapsWith(c))
                throw new Exception("The class overlaps with one of current enrollments.");

            if (_timeTable.Count >= 5)
                throw new Exception("Student tries to enroll to too many classes.");
        }

        public Enrollment LastUnrolled { get; private set; }

        public void Enroll(Class c)
        {
            _timeTable.BookFor(c.GetEnrollmentFor(Id));
        }

        public void Unroll(Class c)
        {
            var enrollmentToRemove = _timeTable.GetEnrollmentFor(c);
            _timeTable.FreeBookedSlotBy(enrollmentToRemove);
            LastUnrolled = LastUnrolled;
        }

        #region IEquatable
        public bool Equals(Student other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Student)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Student left, Student right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Student left, Student right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}