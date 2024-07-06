using System;
using System.Collections.Generic;
using UnityEngine;

namespace Src.Analytics.Events
{
    public static class BranchEventSender
    {
        private static AndroidJavaClass _branchClass;
        
        private const string ForbiddenCustomEventName = "custom event";
        
        public static void SendCustomEvent(
            BranchEventName eventName,
            IReadOnlyDictionary<string, string> eventData)
        {
            if (eventName.Name.Equals(ForbiddenCustomEventName))
            {
                throw new ArgumentException(
                    $"Custom Branch event name cannot have a name as: {eventName}");
            }
            
            var branchEvent = new BranchEvent(eventName.Name);
            foreach (var keyValuePair in eventData)
            {
                branchEvent.AddCustomData(
                    keyValuePair.Key,
                    keyValuePair.Value);
            }
            branchEvent.SetAlias(eventName.Alias);

            Branch.sendEvent(branchEvent);
        }
    }
}