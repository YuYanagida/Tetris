using VitalRouter;

namespace Game.Tetris.Common
{
    public readonly struct FinishGameCommand : ICommand
    {
        public bool IsClear { get; }

        public FinishGameCommand(bool isClear) => IsClear = isClear;
    }
}