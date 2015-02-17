namespace Api.Services
{
    public interface INotifier
    {
        void SendActivationNotification(string emailAddress);
    }
}