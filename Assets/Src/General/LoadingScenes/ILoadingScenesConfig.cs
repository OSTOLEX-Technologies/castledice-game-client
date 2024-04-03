namespace Src.General.LoadingScenes
{
    public interface ILoadingScenesConfig
    {
        public string TransitionSceneName { get; }
        
        public string GetSceneName(SceneType sceneType);
    }
}