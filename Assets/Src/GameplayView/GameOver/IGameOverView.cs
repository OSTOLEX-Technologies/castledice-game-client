using castledice_game_logic;

namespace Src.GameplayView.GameOver
{
    public interface IGameOverView
    {
        void ShowWin(Player winner);
        void ShowDraw();
    }
}