using ObjectOriented.Parser;
using Xunit;

namespace ObjectOriented.Tests.Parser
{
    public class StartStriperTests
    {
        private const string Input = @"Project Gutenberg's The Count of Monte Cristo, by Alexandre Dumas, Pere
                                
This eBook is for the use of anyone anywhere at no cost and with
almost no restrictions whatsoever.  You may copy it, give it away or
re-use it under the terms of the Project Gutenberg License included
with this eBook or online at www.gutenberg.org


Title: The Count of Monte Cristo

Author: Alexandre Dumas, Pere

Posting Date: November 8, 2008 [EBook #1184]
Release Date: January, 1998
[Last updated on January 4, 2012]

Language: English


*** START OF THIS PROJECT GUTENBERG EBOOK THE COUNT OF MONTE CRISTO ***




Produced by Anonymous Project Gutenberg Volunteers





THE COUNT OF MONTE CRISTO

by Alexandre Dumas, Pere




Chapter 1. Marseilles--The Arrival.";

        private const string Expected = @"




Produced by Anonymous Project Gutenberg Volunteers





THE COUNT OF MONTE CRISTO

by Alexandre Dumas, Pere




Chapter 1. Marseilles--The Arrival.";

        [Fact]
        public void ShouldStripStartDataWhenBookText()
        {
            var startStriper = new StartStriper();
            var stripped = startStriper.Parse(new ParserResult<string>(parsed : Input));

            Assert.Equal(new ParserResult<string>(parsed: Expected).Parsed, stripped.Parsed);
        }
    }
}