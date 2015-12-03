using System;

namespace ReFuncToring
{
    public static partial class StateMan
    {
        public static StatePiper<string, string> GetStateFromData()
        {
            return GetRepoStateFunc().ToStatePiper();
        }

        public static Func<string, Result<string>> GetRepoStateFunc()
        {
            return query =>
            {
                if (string.IsNullOrWhiteSpace(query))
                    return new Error<string>("Cannot process an empty input");

                // execute de query

                var result = "Some result from the database";

                return new Success<string>(result);
            };
        }

        public static StatePiper<string, string> GetRules()
        {
            return GetRulesFunc().ToStatePiper();
        }

        public static Func<string, Result<string>> GetRulesFunc()
        {
            return input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return new Error<string>("Cannot process an empty input");

                string blResult = "Business logic executed";

                return new Success<string>(blResult);
            };
        }

        public static StatePiper<string, string> GetMappers()
        {
            return GetMappersFunc().ToStatePiper();
        }

        public static Func<string, Result<string>> GetMappersFunc()
        {
            return input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return new Error<string>("Cannot process an empty input");

                string mappingResult = "Object Mapped";

                return new Success<string>(mappingResult);
            };
        }
    }
}