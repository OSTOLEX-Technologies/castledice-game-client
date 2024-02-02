using System;

namespace Src.Auth.Exceptions.DeepLinking
{
    public class DeepLinkUrlInvalidParameterException : Exception
    {
        public string UsedParameter { get; }
        
        public DeepLinkUrlInvalidParameterException() {}
        
        public DeepLinkUrlInvalidParameterException(string usedParameter)
        {
            UsedParameter = usedParameter;
        }
        
        public DeepLinkUrlInvalidParameterException(string message, Exception inner) : base(message, inner)
        {}

        public DeepLinkUrlInvalidParameterException(string message, string usedParameter) : base(message)
        {
            UsedParameter = usedParameter;
        }
    }
}