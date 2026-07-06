using Game.Tetris.Common;

namespace Game.Tetris.Domain
{
    public interface IMinoShape
    {
        public MinoType MinoType { get; }
        public MinoLine[] MinoPositions { get; }
        public MinoBase Mino { get; }
    }

    public struct MinoLine
    {
        public (int x, int y)[] MinoLines;

        public MinoLine(params (int, int)[] lines)
        {
            MinoLines = lines;
        }
    }

    public abstract class MinoBase
    {
        protected int _currentState;
        protected MinoLine[] _minoPositions;

        public (int x, int y)[] MinoPosition => _minoPositions[_currentState].MinoLines;

        public MinoBase()
        {
            Init();
        }

        /// <summary> •Ï”‚Ì’l‚ğŒˆ’è‚·‚é </summary>
        public abstract void Init();        

        public void Rotate(bool isRight)
        {
            _currentState = isRight ? (_currentState + 1) % _minoPositions.Length : (_currentState + _minoPositions.Length - 1) % _minoPositions.Length;
        }

        public void Reset()
        {
            _currentState = 0;
        }
    }
}