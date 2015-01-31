using System.Collections.Generic;

namespace Domain
{
    public class StudentArchive
    {
        private readonly IList<Enrollment> _passedEnrollments = new List<Enrollment>();

        public void AddToPassedEnrollements(Enrollment e, string reason)
        {
            if (!e.IsEmpty)
            {
                e.Deactivate(reason);
                _passedEnrollments.Add(e);
            }
        }
    }
}
