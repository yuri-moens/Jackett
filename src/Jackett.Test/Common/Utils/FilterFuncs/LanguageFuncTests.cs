using NUnit.Framework;
using static Jackett.Common.Utils.FilterFunc;

namespace Jackett.Test.Common.Utils.FilterFuncs
{
    [TestFixture]
    public class LanguageFuncTests
    {
        private class LanguageIndexerStub : IndexerStub
        {
            public LanguageIndexerStub(string language)
            {
                Language = language;
            }

            public override bool IsConfigured => true;

            public override string Language { get; }
        }

        [Test]
        public void CaseInsensitiveSource_CaseInsensitiveFilter()
        {
            var language = "en";
            var region = "US";

            var lrLanguage = new LanguageIndexerStub($"{language.ToLower()}-{region.ToLower()}");
            var LRFilterFunc = Language.ToFunc($"{language.ToUpper()}-{region.ToUpper()}");
            Assert.IsTrue(LRFilterFunc(lrLanguage));

            var lRLanguage = new LanguageIndexerStub($"{language.ToLower()}-{region.ToUpper()}");
            var LrFilterFunc = Language.ToFunc($"{language.ToUpper()}-{region.ToLower()}");
            Assert.IsTrue(LrFilterFunc(lRLanguage));

            var LrLanguage = new LanguageIndexerStub($"{language.ToUpper()}-{region.ToLower()}");
            var lRFilterFunc = Language.ToFunc($"{language.ToLower()}-{region.ToUpper()}");
            Assert.IsTrue(lRFilterFunc(LrLanguage));

            var LRLanguage = new LanguageIndexerStub($"{language.ToUpper()}-{region.ToUpper()}");
            var lrFilterFunc = Language.ToFunc($"{language.ToLower()}-{region.ToLower()}");
            Assert.IsTrue(lrFilterFunc(LRLanguage));
        }

        [Test]
        public void LanguageWithoutRegion()
        {
            var language = "en";
            var funcFilter = Language.ToFunc(language);

            Assert.IsTrue(funcFilter(new LanguageIndexerStub(language)));
            Assert.IsTrue(funcFilter(new LanguageIndexerStub($"{language}-region1")));
            Assert.IsFalse(funcFilter(new LanguageIndexerStub($"language2-{language}")));
        }
    }
}
