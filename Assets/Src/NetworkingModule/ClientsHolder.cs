﻿using System;
using System.Collections.Generic;
using Riptide;

namespace Src.NetworkingModule
{
    public static class ClientsHolder
    {
        private static readonly Dictionary<ClientType, ClientWrapper> Clients = new();
        
        public static ClientWrapper GetClient(ClientType type)
        {
            if (Clients.TryGetValue(type, out var client))
            {
                return  client;
            }

            throw new InvalidOperationException("Client for type " + type + " is absent.");
        }

        public static void AddClient(ClientType type, ClientWrapper client)
        {
            if (Clients.ContainsKey(type))
                throw new InvalidOperationException("Client for type " + type + " already exists.");
            Clients.Add(type, client);
        }

        public static void RemoveClient(ClientType type)
        {
            if (!Clients.ContainsKey(type))
                throw new InvalidOperationException("Client for type " + type + " is absent.");
            Clients.Remove(type);
        }
    }
}