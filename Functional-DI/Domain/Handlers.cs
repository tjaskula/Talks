using System;

namespace Console
{
    public class Handlers
    {
        public void Deactivate(ItemRepository repository, DeactivateCommand c)
        {
            var item = repository.GetById(c.Id);
            item.Deactivate();
        }

        public void Deactivate(Func<ItemRepository> itemRepositoryFactory, DeactivateCommand c)
        {
            var repository = itemRepositoryFactory();
            var item = repository.GetById(c.Id);
            item.Deactivate();
        }

        public void Log<T>(T command, Action<T> next) where T : ICommand
        {
            // log something here
            next(command);
        }
    }
}