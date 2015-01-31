using System.Web.Http;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class StudentEnrollmentController : ApiController
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