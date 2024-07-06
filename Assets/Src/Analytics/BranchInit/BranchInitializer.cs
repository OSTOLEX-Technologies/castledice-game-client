using System;
using UnityEngine;

namespace Src.Analytics.BranchInit
{
    public class BranchInitializer : MonoBehaviour
    {
        private void Awake()
        {
            Branch.initSession(InitCallback);
        }

        private void InitCallback(
            BranchUniversalObject universalObject,
            BranchLinkProperties linkProperties,
            string error)
        {
            if (error == null) return;
            
            Debug.LogError(error);
            throw new Exception(error);
        }
    }
}