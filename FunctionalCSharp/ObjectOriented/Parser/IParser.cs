namespace ObjectOriented.Parser
{
    public interface IParser<in T, out V>
    {
        V Parse(T input);
    }
}