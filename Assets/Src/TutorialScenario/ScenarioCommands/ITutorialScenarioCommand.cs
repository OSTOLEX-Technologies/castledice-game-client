namespace Src.TutorialScenario.ScenarioCommands
{
    public interface ITutorialScenarioCommand
    {
        public void Do();
        
        public void Undo();
    }
}