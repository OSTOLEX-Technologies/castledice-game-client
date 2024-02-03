namespace Src.LoadingScenes
{
    public interface ILoadingScenesConfig
    {
        public string TransitionSceneName { get; }
        
        public string GetSceneName(ESceneType sceneType);
    }
}