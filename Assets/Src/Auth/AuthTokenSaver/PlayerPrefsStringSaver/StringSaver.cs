using UnityEngine;

namespace Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver
{
    public class StringSaver : IStringSaver
    {
        public bool TryGetStringValue(string name, out string value)
        {
            if (!PlayerPrefs.HasKey(name))
            {
                value = string.Empty;
                return false;
            }

            value = PlayerPrefs.GetString(name);
            return true;
        }

        public void SaveStringValue(string name, string value)
        {
            PlayerPrefs.SetString(name, value);
        }
    }
}