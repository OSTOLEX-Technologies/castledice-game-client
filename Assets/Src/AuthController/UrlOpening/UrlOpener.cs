using UnityEngine;

namespace Src.AuthController.UrlOpening
{
    public class UrlOpener : IUrlOpener
    {
        public void Open(string url)
        {
            Application.OpenURL(url);
        }
    }
}