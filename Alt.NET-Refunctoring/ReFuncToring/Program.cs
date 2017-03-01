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

            System.Console.WriteLine("Query result: {0}", result);
            //System.Console.ReadKey();

            // Functional 
            var readData = StateMan.GetRepoStateFunc();
            var runRules = StateMan.GetRulesFunc();
            var mapViews = StateMan.GetMappersFunc();

            var useCasePipline = from data in readData(query)
                                 from executedRules in runRules(data)
                                 from mappedViews in mapViews(executedRules)
                                 select mappedViews;

            var queryResult = useCasePipline.IsSuccess ? useCasePipline.Success.ToString() : useCasePipline.Error;

            System.Console.WriteLine("Query result: {0}", queryResult);
            System.Console.ReadKey();
        }
    }
}