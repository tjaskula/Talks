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

            Func<string, StateMan.Result<string>> readData = q => new StateMan.Success<string>(null);
            Func<int, StateMan.Result<double>> mapViews = e => new StateMan.Success<double>(e);
            var runRules = StateMan.GetRules();

            var useCasePipline = from data in readData(query)
                                 from executedRules in runRules(data)
                                 from mappedViews in mapViews(executedRules)
                                 select mappedViews;

            Assert.False(useCasePipline.IsSuccess);
            Assert.Equal("Cannot execute business rules. Input data is broken", useCasePipline.FromError());
        }
    }
}