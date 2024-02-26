using System;
using System.Collections.Generic;
using Src.Auth.Exceptions.DeepLinking;

namespace Src.Auth.DeepLinking.LinkResolver.ParametersExtractor
{
    public class DeepLinkDetailsExtractor : IDeepLinkDetailsExtractor
    {
        private char SectionsDelimiter => '?';
        private char ParametersDelimiter => '&';
        private char ParameterDefiner => '=';
        
        public Dictionary<string, string> TryGetParameters(string link)
        {
            var parameters = new Dictionary<string, string>();
            if (!link.Contains(SectionsDelimiter)) return parameters;

            var paramsStringArray = link.Split(SectionsDelimiter)[1]
                .Split(ParametersDelimiter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var parameter in paramsStringArray)
            {
                if (!parameter.Contains(ParameterDefiner)) 
                    throw new DeepLinkUrlInvalidParameterException(parameter);
                var splitParameter = parameter.Split(ParameterDefiner);
                
                parameters.Add(splitParameter[0], splitParameter[1]);
            }

            return parameters;
        }
        
        public string GetLinkName(string link)
        {
            return link.Contains(SectionsDelimiter) ? link.Split(SectionsDelimiter)[0] : link;
        }
    }
}