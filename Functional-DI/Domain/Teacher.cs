namespace Domain
{
    public class Teacher
    {
        public Teacher(int maxInClass)
        {
            Max = maxInClass;
        }

        public int Max { get; private set; }
    }
}