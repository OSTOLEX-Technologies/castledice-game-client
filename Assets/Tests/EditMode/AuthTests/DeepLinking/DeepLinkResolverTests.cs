using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.Auth.DeepLinking.LinkResolver;
using Src.Auth.DeepLinking.LinkResolver.LinkFormatter;
using Src.Auth.DeepLinking.LinkResolver.ParametersExtractor;

namespace Tests.EditMode.AuthTests.DeepLinking
{
    public class DeepLinkResolverTests
    {
        private static string[] testStrings =
        {
            "43785iu5h455",
            "t9t48preifdhhgdc",
            "3p9284irefhghfjkfkd"
        };
        
        [Test]
        public void TryResolveLink_ShouldSplitUrl([ValueSource(nameof(testStrings))] string url)
        {
            var expectedHash = url.GetHashCode().ToString();
            var expectedParams = new Dictionary<string, string>();
            var expectedResolvedLink = new ResolvedDeepLink(expectedHash, expectedParams);
            
            var schemalessUrl = url.Substring(url.Length / 2);
            
            var formatterMock = new Mock<IDeepLinkFormatter>();
            formatterMock.Setup(a => a.GetLinkWithoutScheme(url)).Returns(schemalessUrl);

            var extractorMock = new Mock<IDeepLinkDetailsExtractor>();
            extractorMock.Setup(a => a.GetLinkName(schemalessUrl)).Returns(expectedHash);
            extractorMock.Setup(a => a.TryGetParameters(schemalessUrl)).Returns(expectedParams);

            var resolver = new DeepLinkResolver(formatterMock.Object, extractorMock.Object);
            var result = resolver.TryResolveLink(url);
            
            Assert.AreEqual(expectedResolvedLink, result);
        }
    }
}