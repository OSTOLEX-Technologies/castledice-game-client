using System;
using System.Collections.Generic;
using NUnit.Framework;
using Src.AuthController.REST;

namespace Tests.EditMode.AuthTests
{
    public class RestClientRequestAdapterTests
    {
        [Test]
        [TestCaseSource(nameof(GetRequestMethodTypes))]
        public void GetRequestMethodName_ShouldReturnValidName(RestRequestMethodType type)
        {
            Assert.DoesNotThrow(() => RestClientRequestAdapter.GetRequestMethodName(type));
        }

        public static IEnumerable<RestRequestMethodType> GetRequestMethodTypes()
        {
            var methodTypes = Enum.GetValues(typeof(RestRequestMethodType));
            foreach (var authType in methodTypes) 
            {
                yield return (RestRequestMethodType) authType;
            }
        }
    }
}