namespace ObjectOriented.Parser
{
    public interface IParser<T, R>
    {
        ParserResult<R> Parse(ParserResult<T> input);
    }
}