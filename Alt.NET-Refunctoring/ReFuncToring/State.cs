using System;

namespace ReFuncToring
{
    public static class StateMan
    {
        public static Func<string, Result<string, string>> GetRepoStateFunc()
        {
            return query =>
            {
                if (string.IsNullOrWhiteSpace(query))
                    return Result.Error<string, string>("Cannot process an empty input");

                // execute de query
                var result = "30";

                return Result.Success<string, string>(result);
            };
        }

        public static Func<string, Result<string, int>> GetRulesFunc()
        {
            return input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return Result.Error<string, int>("Cannot execute business rules. Input data is broken");

                int blResult = int.Parse(input);

                return Result.Success<string, int>(blResult);
            };
        }

        public static Func<int, Result<string, double>> GetMappersFunc()
        {
            return input =>
            {
                if (input < 0)
                    return Result.Error<string, double>("Cannot map views");

                double mappingResult = input;

                return Result.Success<string, double>(mappingResult);
            };
        }
    }
}