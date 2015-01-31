using System;

namespace Domain
{
    public class Enrollment
    {
        private readonly int _classId;
        private readonly DateTime _from;
        private readonly DateTime _to;
        private readonly bool _isEmpty;
        private string _reason;

        private static Enrollment _emptyEnrollment;

        private Enrollment()
        {
            _isEmpty = true;
        }

        public Enrollment(int classId, DateTime from, DateTime to)
        {
            _classId = classId;
            _from = @from;
            _to = to;
        }

        public static Enrollment Empty
        {
            get
            {
                return _emptyEnrollment ?? (_emptyEnrollment = new Enrollment());
            }
        }

        public void Deactivate(string reason)
        {
            _reason = reason;
        }

        public int ClassId
        {
            get
            {
                return _classId;
            }
        }

        public DateTime From
        {
            get
            {
                return _from;
            }
        }

        public DateTime To
        {
            get
            {
                return _to;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
        }
    }
}