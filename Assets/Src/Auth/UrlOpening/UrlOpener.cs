using UnityEngine;

namespace Src.Auth.UrlOpening
{
    public class UrlOpener : IUrlOpener
    {
        public void Open(string url)
        {
            Application.OpenURL(url);
        }
    }
}