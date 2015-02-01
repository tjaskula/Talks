using System.Web.Http;
using Domain.Commands;
using Presentation.Helpers;
using Presentation.Models;

namespace Presentation.Controllers
{
    /// <summary>
    /// This style uses dispatcher with a functional composition.
    /// </summary>
    public class StudentEnrollmentFuncController : ApiController
    {
        private readonly IDispatcher<ICommand> _dispatcher;

        public StudentEnrollmentFuncController(IDispatcher<ICommand> dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public void Enroll(EnrollmentRendering r)
        {
            _dispatcher.Dispatch(r.ToCommand());
        }

        public EnrollmentRendering Get()
        {
            return new EnrollmentRendering();
        }
    }
}