using System;
using UnityEngine;

namespace Src.Analytics.BranchInit
{
    public class BranchInitializer : MonoBehaviour
    {
        [SerializeField] private bool enableLogging;
        
        private void Awake()
        {
            if (enableLogging)
            {
                Branch.enableLogging();
            }
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