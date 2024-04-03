using System;

namespace Src.General.SceneTransitionCommands
{
    public interface ISceneTransitionHandler
    {
        public void HandleTransitionCommand(object signalSender, EventArgs args);
    }
}