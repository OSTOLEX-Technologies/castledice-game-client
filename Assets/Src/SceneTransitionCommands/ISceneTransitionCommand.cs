using System;

namespace Src.SceneTransitionCommands
{
    public interface ISceneTransitionCommand
    {
        public void HandleTransitionCommand(object signalSender, EventArgs args);
    }
}