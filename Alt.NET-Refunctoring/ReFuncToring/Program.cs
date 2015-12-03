namespace ReFuncToring
{
    class Program
    {
        static void Main(string[] args)
        {
            string query = "Select something";
            string view;

            var readData = StateMan.GetStateFromData();
            var runRules = StateMan.GetRules();
            var mapViews = StateMan.GetMappers();

            var useCasePipline = from data in readData(query)
                                 from executedRules in runRules(data)
                                 from mappedViews in mapViews(executedRules)
                                 select mappedViews;

            view = useCasePipline.IsSuccess ? useCasePipline.FromState() : "KO";

            System.Console.WriteLine("The view is executed : {0}", view);
            System.Console.ReadKey();
        }
    }
}