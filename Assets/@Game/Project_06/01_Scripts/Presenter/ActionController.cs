using Game.Tetris.View;
using R3;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Game.Tetris.Presenter
{
    public class ActionController : IDisposable
    {
        [Inject] private OperateUI _operateUI;

        private TetrisInputs _gameInputs = new();
        private Subject<int> _moveEvent = new();
        private Subject<bool> _rotateEvent = new();
        private Subject<Unit> _confirmedEvent = new();
              
        public Observable<int> MoveEvent => _moveEvent;
        public Observable<bool> RotateEvent => _rotateEvent;
        public Observable<Unit> ConfirmedEvent => _confirmedEvent;
        
        public void OnEnable()
        {
            //キーボード・コントローラー入力

            _gameInputs.Player.Move.performed += OnMove;
            _gameInputs.Player.Rotate.performed += OnRotate;
            _gameInputs.Player.Confirmed.performed += OnConfirmed;

            _gameInputs.Enable();

            //ゲーム内ボタン入力

            _operateUI.LeftButton.Subscribe(_ =>
            {
                if (Time.timeScale < 1) return;
                _moveEvent.OnNext(-1);
            });

            _operateUI.RightButton.Subscribe(_ =>
            {
                if (Time.timeScale < 1) return;
                _moveEvent.OnNext(1);
            });

            _operateUI.RotateButton.Subscribe(_ =>
            {
                if (Time.timeScale < 1) return;
                _rotateEvent.OnNext(true);
            });

            _operateUI.ConfirmedButton.Subscribe(_ =>
            {
                if (Time.timeScale < 1) return;
                _confirmedEvent.OnNext(Unit.Default);
            });
        }

        private void OnMove(InputAction.CallbackContext conext)
        {
            if (Time.timeScale < 1) return;
            var movevalue = conext.ReadValue<Vector2>();
            _moveEvent.OnNext((int)movevalue.x);
        }

        private void OnRotate(InputAction.CallbackContext conext)
        {
            if (Time.timeScale < 1) return;
            _rotateEvent.OnNext(true);
        }

        private void OnConfirmed(InputAction.CallbackContext conext)
        {
            if (Time.timeScale < 1) return;
            _confirmedEvent.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _gameInputs?.Dispose();
        }
    }
}