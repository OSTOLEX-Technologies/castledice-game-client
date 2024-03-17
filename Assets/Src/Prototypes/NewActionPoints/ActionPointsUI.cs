using castledice_game_logic;
using TMPro;
using UnityEngine;

public class ActionPointsUI
{
    private readonly TextMeshProUGUI _textMesh;
    private readonly GameObject _banner;
    private readonly Player _player;
    
    public ActionPointsUI(TextMeshProUGUI textMesh, GameObject banner, Player player)
    {
        _textMesh = textMesh;
        _banner = banner;
        _player = player;
        _player.ActionPoints.ActionPointsIncreased += OnActionPointsIncreased;
        _player.ActionPoints.ActionPointsDecreased += OnActionPointsDecreased;
    }

    private void OnActionPointsDecreased(object sender, int e)
    {
        if (_player.ActionPoints.Amount <= 0)
        {
            _banner.SetActive(false);
        }
        _textMesh.text = _player.ActionPoints.Amount.ToString();
    }

    private void OnActionPointsIncreased(object sender, int e)
    {
        _banner.SetActive(true);
        _textMesh.text = _player.ActionPoints.Amount.ToString();
    }
}
