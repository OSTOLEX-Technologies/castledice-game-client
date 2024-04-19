using Src.Components;
using Src.General.LoadingScenes;
using UnityEngine;

public class MainMenuPlayButtonHandler : MonoBehaviour
{
   [SerializeField] private SceneLoader _sceneLoader;
   
   public void HandlePlayButton()
   {
      _sceneLoader.LoadScene(SceneType.PVE);
   }
}
