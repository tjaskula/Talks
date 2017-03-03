using System;
using Xunit;

namespace ReFuncToring
{
    public class Tests
    {
        [Fact]
        public void ShouldFailBusinessLogicWhenDataIsCorrupted()
        {
            string query = "Select something";

            Func<string, Result<string, string>> readData = q => Result.Success<string, string>(null);
            Func<int, Result<string, double>> mapViews = e => Result.Success<string, double>(e);
            var runRules = StateMan.GetRulesFunc();

            var useCasePipline = from data in readData(query)
                                 from executedRules in runRules(data)
                                 from mappedViews in mapViews(executedRules)
                                 select mappedViews;

            Assert.False(useCasePipline.IsSuccess);
            Assert.Equal("Cannot execute business rules. Input data is broken", useCasePipline.Error);
        }
    }
}