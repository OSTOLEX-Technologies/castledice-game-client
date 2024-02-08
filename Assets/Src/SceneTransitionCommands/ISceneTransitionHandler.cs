using System;

namespace Src.SceneTransitionCommands
{
    public interface ISceneTransitionHandler
    {
        public void HandleTransitionCommand(object signalSender, EventArgs args);
    }
}