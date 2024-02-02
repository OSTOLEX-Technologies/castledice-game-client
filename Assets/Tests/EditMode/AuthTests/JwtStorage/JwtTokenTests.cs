using System;
using NUnit.Framework;
using Src.Auth.JwtManagement.Tokens;

namespace Tests.EditMode.AuthTests.JwtStorage
{
    public class JwtTokenTests
    {
        const string TokenStub = "abc";
        const string TokenReplacementStub = "cba";
        
        [Test]
        public void Valid_ShouldTellCheckIfIssuedTimeIsLessThanExpiresTime([Values(360, 5000, 28000)] int issued, [Values(600, 150, 5002)] int expires)
        {
            var jwt = new JwtToken(TokenStub, expires, DateTime.Now - TimeSpan.FromSeconds(issued));
            
            Assert.AreEqual(issued < expires, jwt.Valid);
        }
        
        [Test]
        public void GetToken_ShouldReturnContainingToken()
        {
            var jwt = new JwtToken(TokenStub, 0, DateTime.Now);
            
            Assert.AreEqual(TokenStub, jwt.Token);
        }
        
        [Test]
        public void UpdateToken_ShouldReplaceContainingToken_WithNewOne()
        {
            var jwt = new JwtToken(TokenStub, 0, DateTime.Now);
            jwt.UpdateToken(TokenReplacementStub);
            
            Assert.AreEqual(TokenReplacementStub, jwt.Token);
        }
    }
}