using Game.Tetris.Common;
using VContainer;
using VContainer.Unity;
using R3;
using Game.Tetris.Domain;
using Game.Tetris.Presenter;
using VitalRouter;

namespace Game.Tetris.UseCase
{
    [Routes]
    public partial class AudioUsecase : IStartable
    {
        [Inject] private ActionController _actionController;
        [Inject] private RowErasure _erasure;

        public void Start()
        {
            SubScribe();
        }

        public void On(FinishGameCommand cmd)
        {
            if (cmd.IsClear)
                AudioManager.Instance.SE.Play("Clear");
            else
                AudioManager.Instance.SE.Play("Failure");
        }

        private void SubScribe()
        {
            _actionController.MoveEvent.Subscribe(_ =>
            {
                AudioManager.Instance.SE.Play("Move");
            });

            _actionController.RotateEvent.Subscribe(_ =>
            {
                AudioManager.Instance.SE.Play("Rotate");
            });

            _actionController.ConfirmedEvent.Subscribe(_ =>
            {
                AudioManager.Instance.SE.Play("Confirmed");
            });

            _erasure.ErasedEvent.Subscribe(_ =>
            {
                AudioManager.Instance.SE.Play("Erase");
            });
        }
    }
}