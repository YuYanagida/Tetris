using Game.Tetris.Presenter;
using VContainer;
using VContainer.Unity;
using R3;
using Game.Tetris.Domain;
using System;
using Game.Tetris.Common;
using VitalRouter;

namespace Game.Tetris.UseCase
{
    [Routes]
    public partial class ActionUseCase : IStartable
    {
        [Inject] private MinoController _minoController;
        [Inject] private ActionController _actionController;
        [Inject] private FieldSimulater _simulatField;

        private CompositeDisposable _disposable = new();

        public void Start()
        {
            _actionController.OnEnable();

            Subscribe();
        }

        public void On(FinishGameCommand _)
        {
            _disposable?.Dispose();
        }

        private void Subscribe()
        {
            _actionController.MoveEvent.Subscribe(value =>
            {
                _minoController.MoveX(value);
            }).AddTo(_disposable);

            _actionController.RotateEvent.Subscribe(isRight =>
            {
                _minoController.Rotate(isRight);
            }).AddTo(_disposable);

            _actionController.ConfirmedEvent.Subscribe(_ =>
            {
                _minoController.SetSimulatePoint(_simulatField.SimulatePosition);
            }).AddTo(_disposable);
        }
    }
}