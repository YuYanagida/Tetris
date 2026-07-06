using R3;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Canvas _resultCanvas;
    [SerializeField] private GameObject _clearObject;
    [SerializeField] private GameObject _gameoverObject;
    [SerializeField] private GameObject _poseObject;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _poseButton;
    [SerializeField] private Button _resumeButton;

    public Button RetryButton => _retryButton;
    public Button HomeButton => _homeButton;
    public Button PoseButton => _poseButton;
    public Button ResumeButton => _resumeButton;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void ShowResult(bool isClear)
    {
        Result(isClear);
        _resultCanvas.enabled = true;
    }

    public void Pose()
    {
        Time.timeScale = 0;

        _clearObject.SetActive(false);
        _gameoverObject.SetActive(false);
        _poseObject.SetActive(true);
        _resultCanvas.enabled = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;

        _resultCanvas.enabled= false;
        _poseObject.SetActive(false);
    }

    private void Result(bool isClear)
    {
        _clearObject.SetActive(isClear);
        _gameoverObject.SetActive(!isClear);
    }
}
