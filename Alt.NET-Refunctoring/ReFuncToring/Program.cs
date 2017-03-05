using System;
using ReFuncToring.ObjectOriented;

namespace ReFuncToring
{
    class Program
    {
        static void Main(string[] args)
        {
            string query = "Select something";
            //double queryResult;

            // Oriented Objet
            var repository = new Repository();
            var bl = new BusinessLogic(repository);
            var application = new ApplicationService(bl);

            var result = application.MapViews(query);

            Console.WriteLine("Query result: {0}", result);
            Console.ReadKey();

            // Functional

            Func<Request, Result<string, Request>> validate1 = input =>
            {
                if (string.IsNullOrEmpty(input.Name))
                    return Result.Error<string, Request>("Name must not be blank");
                return Result.Success<string, Request>(input);
            };

            Func<Request, Result<string, Request>> validate2 = input =>
            {
                if (input.Name.Length > 50)
                    return Result.Error<string, Request>("Name must not be longer than 50 chars");
                return Result.Success<string, Request>(input);
            };

            Func<Request, Result<string, Request>> validate3 = input =>
            {
                if (string.IsNullOrEmpty(input.Email))
                    return Result.Error<string, Request>("Email must not be blank");
                return Result.Success<string, Request>(input);
            };

            var request = new Request();
            var combinedValidation = validate1(request).SelectMany(v2 => validate2(v2), (req, validated2) => new
                                                            {
                                                               req,
                                                               validated2
                                                            }).SelectMany(v3 => validate2(v3.validated2), (v3, validationPassed) => validationPassed);

            var validationResult1 = combinedValidation.IsSuccess
                ? combinedValidation.Success.ToString()
                : combinedValidation.Error;

            Console.WriteLine("Validation1 result: {0}", validationResult1);
            Console.ReadKey();

            var combinedValidation2 = validate1(request)
                                        .SelectMany(
                                            v2 => validate2(v2)).SelectMany(
                                                v3 => validate3(v3));

            var validationResult2 = combinedValidation2.IsSuccess
                ? combinedValidation2.Success.ToString()
                : combinedValidation2.Error;

            Console.WriteLine("Validation2 result: {0}", validationResult2);
            Console.ReadKey();

            var readData = StateMan.GetRepoStateFunc();
            var runRules = StateMan.GetRulesFunc();
            var mapViews = StateMan.GetMappersFunc();

            var useCasePipline = from data in readData(query)
                                 from executedRules in runRules(data)
                                 from mappedViews in mapViews(executedRules)
                                 select mappedViews;

            var queryResult = useCasePipline.IsSuccess ? useCasePipline.Success.ToString() : useCasePipline.Error;

            Console.WriteLine("Query result: {0}", queryResult);
            Console.ReadKey();
        }
    }

    class Request
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}