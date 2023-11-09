using TMPro;

namespace Src.GameplayView.ActionPointsCount
{
    public class ActionPointsCountView : IActionPointsCountView
    {
        private readonly TextMeshProUGUI _countTextMesh;
        private readonly TextMeshProUGUI _labelTextMesh;

        public ActionPointsCountView(TextMeshProUGUI countTextMesh, TextMeshProUGUI labelTextMesh)
        {
            _countTextMesh = countTextMesh;
            _labelTextMesh = labelTextMesh;
        }

        public void ShowActionPointsCount(int count)
        {
            _countTextMesh.gameObject.SetActive(true);
            _labelTextMesh.gameObject.SetActive(true);
            _countTextMesh.text = count.ToString();
        }

        public void HideActionPointsCount()
        {
            _countTextMesh.gameObject.SetActive(false);
            _labelTextMesh.gameObject.SetActive(false);
        }
    }
}