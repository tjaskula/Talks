namespace ReFuncToring.ObjectOriented
{
    public class ApplicationService
    {
        private readonly BusinessLogic _bl;

        public ApplicationService(BusinessLogic bl)
        {
            _bl = bl;
        }

        public double MapViews(string query)
        {
            var r = _bl.RunRules(query);
            return r;
        }
    }
}