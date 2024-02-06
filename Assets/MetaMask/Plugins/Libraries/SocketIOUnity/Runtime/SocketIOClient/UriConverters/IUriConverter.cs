﻿using System;
using System.Collections.Generic;

namespace MetaMask.Plugins.Libraries.SocketIOUnity.Runtime.SocketIOClient.UriConverters
{
    public interface IUriConverter
    {
        Uri GetServerUri(bool ws, Uri serverUri, int eio, string path, IEnumerable<KeyValuePair<string, string>> queryParams);
    }
}
