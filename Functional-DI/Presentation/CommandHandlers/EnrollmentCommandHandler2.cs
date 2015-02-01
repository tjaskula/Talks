using System;
using Domain.Commands;
using Infrastructure;

namespace Presentation.CommandHandlers
{
    /// <summary>
    /// This is just a sample of how this may go wrong injecting everything into the constructor
    /// </summary>
    public class EnrollmentCommandHandler2 : ICommandHandler<StudentEnrollCommand>,
                                            ICommandHandler<StudentUnrollCommand>
    {
        private readonly StudentRepository _studentRepository;
        private readonly ClassRepository _classRepository;
        private readonly StudentArchiveRepository _studentArchiveRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly EnrollementNotificationService _notificationService;
        private readonly ILogger _logger;
        private readonly AuthorizationService _authorizationService;
        private readonly CalendarService _calendarService;
        private readonly ServiceFoo _serviceFoo;
        private readonly ServiceBlah _serviceBlah;
        private readonly FactoryFoo _facoFactoryFoo;
        private readonly FactoryBlah _factoryBlah;

        public EnrollmentCommandHandler2(StudentRepository studentRepository,
                                            ClassRepository classRepository,
                                            StudentArchiveRepository studentArchiveRepository,
                                            UnitOfWork unitOfWork,
                                            EnrollementNotificationService notificationService,
                                            ILogger logger,
                                            AuthorizationService authorizationService,
                                            CalendarService calendarService,
                                            ServiceFoo serviceFoo,
                                            ServiceBlah serviceBlah,
                                            FactoryFoo facoFactoryFoo,
                                            FactoryBlah factoryBlah
                                        )
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _studentArchiveRepository = studentArchiveRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _logger = logger;
            _authorizationService = authorizationService;
            _calendarService = calendarService;
            _serviceFoo = serviceFoo;
            _serviceBlah = serviceBlah;
            _facoFactoryFoo = facoFactoryFoo;
            _factoryBlah = factoryBlah;
        }

        public void Handles(StudentEnrollCommand command)
        {
            var student = _studentRepository.GetById(command.StudentId);
            var @class = _classRepository.GetById(command.ClassId);

            try
            {
                _unitOfWork.BeginTransaction();
                student.TryEnrollIn(@class);
                @class.TryEnroll(student);

                student.Enroll(@class);
                @class.Enroll(student);
                _notificationService.Notify(student);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _logger.LogError(e);
            }
        }

        public void Handles(StudentUnrollCommand command)
        {
            var student = _studentRepository.GetById(command.StudentId);
            var @class = _classRepository.GetById(command.ClassId);
            var studentArchive = _studentArchiveRepository.GetById(command.StudentId);

            _unitOfWork.BeginTransaction();
            student.Unroll(@class);
            @class.Unroll(student);
            studentArchive.AddToPassedEnrollements(student.LastUnrolled, command.Reason);
            _notificationService.Notify(student);
            _unitOfWork.Commit();
        }
    }

    // JUNK HERE JUST FOR THE PURPOSE OF WHAT MIGHT THE COMMAND HANDLER BE
    public class FactoryBlah
    {
    }

    public class FactoryFoo
    {
    }

    public class ServiceBlah
    {
    }

    public class ServiceFoo
    {
    }

    public class CalendarService
    {
    }

    public class AuthorizationService
    {
    }
}