using UnityEngine;

public enum TipType { Run, Shoot, LevelEnd }

public class UITipsSwitcher : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _shootTip;
    [SerializeField] private GameObject _runTip;
    [SerializeField] private GameObject _levelEndTip;

    private void Start()
    {
        _player.UpdateUI += OnUpdateUI;
    }

    private void OnUpdateUI(TipType type)
    {
        switch (type)
        {
            case TipType.Run:
                ShowRunTip();
                break;
            case TipType.Shoot:
                ShowShootTip();
                break;
            case TipType.LevelEnd:
                ShowLevelEndTip();
                break;
        }
    }

    private void ShowShootTip()
    {
        _shootTip.SetActive(true);
        _runTip.SetActive(false);
        _levelEndTip.SetActive(false);
    }

    private void ShowRunTip()
    {
        _shootTip.SetActive(false);
        _runTip.SetActive(true);
        _levelEndTip.SetActive(false);
    }

    private void ShowLevelEndTip()
    {
        _shootTip.SetActive(false);
        _runTip.SetActive(false);
        _levelEndTip.SetActive(true);
    }

    private void OnDestroy()
    {
        _player.UpdateUI -= OnUpdateUI;
    }
}
