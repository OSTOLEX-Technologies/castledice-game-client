using System.Collections;
using System.Collections.Generic;
using Src.Components;
using Src.LoadingScenes;
using UnityEngine;

public class MainMenuPlayButtonHandler : MonoBehaviour
{
   [SerializeField] private SceneLoader _sceneLoader;
   
   public void HandlePlayButton()
   {
      _sceneLoader.LoadScene(SceneType.PVE);
   }
}
