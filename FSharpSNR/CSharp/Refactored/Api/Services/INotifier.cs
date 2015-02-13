namespace Api.Services
{
    public interface INotifier
    {
        void SendActivaionNotification(string emailAddress);
    }
}