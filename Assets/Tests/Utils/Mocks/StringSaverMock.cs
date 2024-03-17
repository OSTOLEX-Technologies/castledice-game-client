using System.Collections.Generic;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;

namespace Tests.Utils.Mocks
{
    public class StringSaverMock : IStringSaver
    {
        public Dictionary<string, string> Values { get; set; }

        public StringSaverMock()
        {
            Values = new Dictionary<string, string>();
        }

        public bool TryGetStringValue(string name, out string value)
        {
            value = Values.ContainsKey(name) ? Values[name] : "";
            return Values.ContainsKey(name);
        }

        public void SaveStringValue(string name, string value)
        {
            Values[name] = value;
        }

        public void DeleteStringValue(string name)
        {
            Values = new Dictionary<string, string>();
        }
    }
}