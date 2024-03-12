using Src.Auth.DeepLinking.Config;
using Src.Auth.DeepLinking.LinkResolver;
using Src.Auth.DeepLinking.LinkResolver.LinkFormatter;
using Src.Auth.DeepLinking.LinkResolver.ParametersExtractor;
using Src.Auth.Exceptions.DeepLinking;
using UnityEngine;

namespace Src.Auth.DeepLinking
{
    public class DeepLinkHandler : MonoBehaviour
    {
        private static DeepLinkHandler Instance { get; set; }

        private DeepLinkResolver _linkResolver;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(this);

            _linkResolver = new DeepLinkResolver(
                new DeepLinkFormatter(), 
                new DeepLinkDetailsExtractor());

            Application.deepLinkActivated += OnDeepLinkActivated;
        }

        private void OnDeepLinkActivated(string url)
        {
            try
            {
                var resolvedLink = _linkResolver.TryResolveLink(url);

                switch (resolvedLink.LinkName)
                {
                    case DeepLinkConfig.GoogleAuthRedirectUri:
                        break;
                }
            }
            catch (DeepLinkUrlInvalidParameterException e)
            {
                #if UNITY_EDITOR
                Debug.LogError(e + "\nParameter: " + e.UsedParameter);
                #endif
            }
        }
    }
}