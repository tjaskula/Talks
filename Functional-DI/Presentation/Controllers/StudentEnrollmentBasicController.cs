using System.Web.Http;
using Presentation.Models;

namespace Presentation.Controllers
{
    /// <summary>
    /// This is the most basic controller taking all the dependencies in the constructor like repositories, loggin services, security, etc.
    /// This style of injecting is the most basic and should be avoided.
    /// </summary>
    // TODO : Add example of how the dependencies are injected.
    public class StudentEnrollmentBasicController : ApiController
    {
        public void Enroll(EnrollmentRendering r)
        {
            
        }

        public EnrollmentRendering Get()
        {
            return new EnrollmentRendering();
        }
    }
}