using ObjectOriented.IO;

namespace ObjectOriented
{
    public class Bootstrapper
    {
        public void RegisterServices()
        {
            var fileReader = new FileStoreReader();
        }
    }
}