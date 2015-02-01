using System.Web.Http;
using Presentation.Models;

namespace Presentation.Controllers
{
    /// <summary>
    /// This style uses bus for publishing commands to command handlers
    /// </summary>
    // TODO : Add example of how the dependencies are injected.
    public class StudentEnrollmentBusController : ApiController
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
