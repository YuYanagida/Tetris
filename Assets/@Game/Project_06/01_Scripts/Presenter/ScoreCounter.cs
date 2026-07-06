using Game.Tetris.Common;
using Game.Tetris.Domain;
using VContainer;
using R3;
using VitalRouter;

namespace Game.Tetris.Presenter
{
    public class ScoreCounter
    {
        [Inject] private InitialData _initialData;
        [Inject] private ICommandPublisher _commandPublisher;
        [Inject] private ScoreCounterView _clearCounterView;

        private int count;// { get; set { count; } }

        public void Init()
        {
            count = _initialData.NeedCount;
            _clearCounterView.SetOrder(_initialData.NeedCount);
            _clearCounterView.SetSitsuation(count);
        }

        public void Count()
        {
            count--;
            _clearCounterView.SetSitsuation(count);

            if (count == 0)
            {
                _commandPublisher.PublishAsync(new FinishGameCommand(true));
            }
        }
    }
}