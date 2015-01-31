using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class TimeTable
    {
        private readonly IList<Enrollment> _enrollments = new List<Enrollment>();

        public int Count
        {
            get
            {
                return this._enrollments.Count;
            }
        }

        public bool OverlapsWith(Class @class)
        {
            Func<Enrollment, bool> overlaps = e => e.From < @class.To && @class.From < e.To;

            return this._enrollments.Any(overlaps);
        }

        public void BookFor(Enrollment enrollment)
        {
            if (!enrollment.IsEmpty)
                this._enrollments.Add(enrollment);
        }

        public Enrollment GetEnrollmentFor(Class @class)
        {
            var enrollment = _enrollments.SingleOrDefault(c => c.ClassId == @class.Id);
            if (enrollment == null) return Enrollment.Empty;

            return enrollment;
        }

        public void FreeBookedSlotBy(Enrollment e)
        {
            _enrollments.Remove(e);
        }
    }
}