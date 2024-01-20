using Src.AuthController.DeepLinking.Config;
using Src.AuthController.DeepLinking.LinkResolver;
using Src.AuthController.DeepLinking.LinkResolver.LinkFormatter;
using Src.AuthController.DeepLinking.LinkResolver.ParametersExtractor;
using Src.AuthController.Exceptions.DeepLinking;
using UnityEngine;

namespace Src.AuthController.DeepLinking
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
                var linkName = _linkResolver.TryResolveLink(url);

                switch (linkName.LinkName)
                {
                    case DeepLinkConfig.GoogleAuthRedirectUri:
                        //TODO: disable google auth panel|canvas
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