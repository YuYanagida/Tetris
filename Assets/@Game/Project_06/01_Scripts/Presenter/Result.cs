using MackySoft.Navigathena.SceneManagement;
using R3;
using VContainer;

namespace Game.Tetris.Presenter
{
    public class Result
    {
        [Inject] private ResultUI _resultUI;

        public void Subscribe()
        {
            _resultUI.RetryButton.OnClickAsObservable().Subscribe(_ =>
            {
                GlobalSceneNavigator.Instance.Reload();
            });

            _resultUI.HomeButton.OnClickAsObservable().Subscribe(_ =>
            {
                GlobalSceneNavigator.Instance.Pop();
            });

            _resultUI.PoseButton.OnClickAsObservable().Subscribe(_ =>
            {
                _resultUI.Pose();
            });

            _resultUI.ResumeButton.OnClickAsObservable().Subscribe(_ =>
            {
                _resultUI.Resume();
            });
        }

        public void ShowResult(bool isClear)
        {
            _resultUI.ShowResult(isClear);

        }
    }
}