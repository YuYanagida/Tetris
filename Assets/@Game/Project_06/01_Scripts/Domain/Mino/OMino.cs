using Game.Tetris.Common;

namespace Game.Tetris.Domain
{
    public class OMino : MinoBase, IMinoShape
    {
        public MinoType MinoType => MinoType.Omino;

        public MinoLine[] MinoPositions => _minoPositions;

        public MinoBase Mino => this;

        public override void Init()
        {
            _minoPositions = new MinoLine[1]
                {
                    new((0, 0), (1, 0), (0, -1), (1, -1))
                };
        }
    }
}