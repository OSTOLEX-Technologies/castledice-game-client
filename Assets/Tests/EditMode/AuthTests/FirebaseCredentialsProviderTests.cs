﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.AuthController;
using Src.AuthController.CredentialProviders;
using Src.AuthController.REST.REST_Responses;

namespace Tests.EditMode.AuthTests
{
    public class FirebaseCredentialsProviderTests
    {
        [Test]
        [TestCaseSource(nameof(GetAuthTypes))]
        public async Task GetCredentialAsync_ShouldGetValidCredentials(FirebaseAuthProviderType firebaseAuthProviderType)
        {
            switch (firebaseAuthProviderType)
            {
                case FirebaseAuthProviderType.Google:
                    var googleCredentials = new GoogleIdTokenResponse();
                    var googleCredProviderMock = new Mock<GoogleCredentialProvider>();
                    googleCredProviderMock.Setup(s => s.GetCredentialAsync()).ReturnsAsync(googleCredentials);

                    var result = await googleCredProviderMock.Object.GetCredentialAsync();
                    Assert.AreSame(googleCredentials, result);
                    break;
            }
        }
            
        public static IEnumerable<FirebaseAuthProviderType> GetAuthTypes()
        {
            var authTypes = Enum.GetValues(typeof(FirebaseAuthProviderType));
            foreach (var firebaseAuthType in authTypes) 
            {
                yield return (FirebaseAuthProviderType) firebaseAuthType;
            }
        }
    }
}