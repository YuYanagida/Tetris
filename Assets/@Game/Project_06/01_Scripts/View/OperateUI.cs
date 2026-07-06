using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tetris.View
{
    public class OperateUI : MonoBehaviour 
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _rotateButton;
        [SerializeField] private Button _confirmedButton;

        public Observable<Unit> LeftButton => _leftButton.OnClickAsObservable();
        public Observable<Unit> RightButton => _rightButton.OnClickAsObservable();
        public Observable<Unit> RotateButton => _rotateButton.OnClickAsObservable();
        public Observable<Unit> ConfirmedButton => _confirmedButton.OnClickAsObservable();
    }
}