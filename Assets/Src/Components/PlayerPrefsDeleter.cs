using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDeleter : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
}
