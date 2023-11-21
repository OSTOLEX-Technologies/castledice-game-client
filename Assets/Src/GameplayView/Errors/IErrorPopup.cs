namespace Src.GameplayView.Errors
{
    public interface IErrorPopup
    {
        void Show();
        void Hide();
        void SetMessage(string message);
    }
}