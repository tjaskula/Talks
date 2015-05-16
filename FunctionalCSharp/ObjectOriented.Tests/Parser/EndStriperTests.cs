using ObjectOriented.Parser;
using Xunit;

namespace ObjectOriented.Tests.Parser
{
    public class EndStriperTests
    {
        private const string Input = @"""Gone,"" murmured Valentine; ""adieu, my sweet Haidee--adieu, my sister!""

""Who can say whether we shall ever see them again?"" said Morrel with
tearful eyes.

""Darling,"" replied Valentine, ""has not the count just told us that all
human wisdom is summed up in two words?--'_Wait and hope_.'""





End of Project Gutenberg's The Count of Monte Cristo, by Alexandre Dumas, Pere

*** END OF THIS PROJECT GUTENBERG EBOOK THE COUNT OF MONTE CRISTO ***

***** This file should be named 1184.txt or 1184.zip *****
This and all associated files of various formats will be found in:
        http://www.gutenberg.org/1/1/8/1184/

Produced by Anonymous Project Gutenberg Volunteers

Updated editions will replace the previous one--the old editions
will be renamed.

Creating the works from public domain print editions means that no
one owns a United States copyright in these works, so the Foundation
(and you!) can copy and distribute it in the United States without
permission and without paying copyright royalties.  Special rules,
set forth in the General Terms of Use part of this license, apply to
copying and distributing Project Gutenberg-tm electronic works to
protect the PROJECT GUTENBERG-tm concept and trademark.  Project
Gutenberg is a registered trademark, and may not be used if you
charge for the eBooks, unless you receive specific permission.  If you
do not charge anything for copies of this eBook, complying with the
rules is very easy.  You may use this eBook for nearly any purpose
such as creation of derivative works, reports, performances and
research.  They may be modified and printed and given away--you may do
practically ANYTHING with public domain eBooks.  Redistribution is
subject to the trademark license, especially commercial
redistribution.";

        private const string Expected = @"""Gone,"" murmured Valentine; ""adieu, my sweet Haidee--adieu, my sister!""

""Who can say whether we shall ever see them again?"" said Morrel with
tearful eyes.

""Darling,"" replied Valentine, ""has not the count just told us that all
human wisdom is summed up in two words?--'_Wait and hope_.'""





End of Project Gutenberg's The Count of Monte Cristo, by Alexandre Dumas, Pere

";

        [Fact]
        public void ShouldStripStartDataWhenBookText()
        {
            var startStriper = new EndStriper();
            var stripped = startStriper.Parse(Input);

            Assert.Equal(Expected, stripped);
        }
    }
}