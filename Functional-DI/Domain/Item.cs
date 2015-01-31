namespace Console
{
    public class Item
    {
        private bool isDeactivated;

        public bool IsDeactivated 
        {
            get
            {
                return this.isDeactivated;
            }
        }

        public void Deactivate()
        {
            this.isDeactivated = true;
        }
    }
}