using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Src.Auth;
using Src.Auth.AuthTokenSaver;
using Src.Auth.JwtManagement;
using Src.Auth.JwtManagement.Tokens;
using Tests.Utils.Mocks;
using UnityEngine;

namespace Tests.EditMode.AuthTests.TokenSaving
{
    [TestFixture]
    public class AuthTokenSaverTests
    {
        private const string SampleToken = "SAMPLE_TOKEN";
        private const string LastLoginKey = "LastLoginAuthTokensStoreInfo";

        private AuthTokenSaver _tokenSaver;
        private StringSaverMock _saverMock;
        private AbstractJwtStore _sampleStore;

        [SetUp]
        public void Init()
        {
            _saverMock = new StringSaverMock();
            _tokenSaver = new AuthTokenSaver(_saverMock);
            
            var sampleToken = new JwtToken(SampleToken, int.MaxValue, DateTime.Now);
            _sampleStore = new JwtStore(sampleToken, sampleToken);
        }


        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public void SaveAuthTokens_ShouldSaveTokens_WithSpecifiedName(AuthType type)
        {
            _tokenSaver.SaveAuthTokens(_sampleStore, type);

            var stored = _saverMock.Values[AuthTokenSaver.GetStorePrefNameByAuthType(type)];
            var serialized = JsonConvert.SerializeObject(_sampleStore);
            
            Assert.IsTrue(stored == serialized);
        }


        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public void TryGetTokenValueByAuthType_ShouldReturnStoredToken_IfThereIsSavedOne(AuthType type)
        {
            _tokenSaver.SaveAuthTokens(_sampleStore, type);
            _tokenSaver.TryGetTokenStoreByAuthType(out var store, type);
            Assert.IsTrue(_sampleStore.Equals(store));
        }


        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public void UpdateLastLoginInfo_ShouldSaveGivenInfo(AuthType type)
        {
            _tokenSaver.UpdateLastLoginInfo(type);
            var storedValue = _saverMock.Values[LastLoginKey];
            _saverMock.Values.Remove(LastLoginKey);
            Debug.Log(storedValue);
            Debug.Log(type.ToString());
            Assert.IsTrue(storedValue == type.ToString());
        }
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public void TryGetLastLoginInfo_ShouldReturnInfo_SavedPreviously(AuthType type)
        {
            _tokenSaver.UpdateLastLoginInfo(type);
            var success = _tokenSaver.TryGetLastLoginInfo(out var storedValue);
            _saverMock.Values.Remove(LastLoginKey);
            
            Assert.IsTrue(success && storedValue == type);
        }
        [Test]
        public void TryGetLastLoginInfo_ShouldFail_LoadingUnsavedPreviouslyInfo()
        {
            var success = _tokenSaver.TryGetLastLoginInfo(out var storedValue);

            Assert.IsTrue(!success);
        }
        
        public static IEnumerable<AuthType> GetAuthTypes()
        {
            var authTypes = Enum.GetValues(typeof(AuthType));
            foreach (var authType in authTypes)
            {
                yield return (AuthType)authType;
            }
        }
    }
}