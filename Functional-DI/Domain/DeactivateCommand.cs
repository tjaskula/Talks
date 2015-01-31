namespace Console
{
    public class DeactivateCommand : ICommand
    {
        public DeactivateCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }
    }
}