namespace ObjectOriented.AppService
{
    public interface IStoreReader<out T>
    {
        T Read();
    }
}