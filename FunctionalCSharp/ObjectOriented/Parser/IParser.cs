namespace ObjectOriented.Parser
{
    public interface IParser<out T>
    {
        T Parse(string input);
    }
}