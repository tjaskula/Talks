using System;

namespace ReFuncToring
{
    public static class StateMan
    {
    //    public static Result<string, string> GetStateFromData()
    //    {
    //        return GetRepoStateFunc().ToStatePiper();
    //    }

    //    public static Func<string, Result<string, string>> GetRepoStateFunc()
    //    {
    //        return query =>
    //        {
    //            if (string.IsNullOrWhiteSpace(query))
    //                return new Error<string>("Cannot process an empty input");

    //            // execute de query

    //            var result = "30";

    //            return new Success<string>(result);
    //        };
    //    }

    //    public static StatePiper<string, int> GetRules()
    //    {
    //        return GetRulesFunc().ToStatePiper();
    //    }

    //    public static Func<string, Result<int>> GetRulesFunc()
    //    {
    //        return input =>
    //        {
    //            if (string.IsNullOrWhiteSpace(input))
    //                return new Error<int>("Cannot execute business rules. Input data is broken");

    //            int blResult = int.Parse(input);

    //            return new Success<int>(blResult);
    //        };
    //    }

    //    public static StatePiper<int, double> GetMappers()
    //    {
    //        return GetMappersFunc().ToStatePiper();
    //    }

    //    public static Func<int, Result<double>> GetMappersFunc()
    //    {
    //        return input =>
    //        {
    //            if (input < 0)
    //                return new Error<double>("Cannot map views");

    //            double mappingResult = input;

    //            return new Success<double>(mappingResult);
    //        };
    //    }
    }
}