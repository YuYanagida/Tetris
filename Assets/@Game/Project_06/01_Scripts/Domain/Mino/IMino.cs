using Game.Tetris.Common;

namespace Game.Tetris.Domain
{
    public class IMino : MinoBase, IMinoShape
    {
        public MinoType MinoType => MinoType.Imino;

        public MinoLine[] MinoPositions => _minoPositions;

        public MinoBase Mino => this;

        public override void Init()
        {
            _minoPositions = new MinoLine[2]
            {
                new((-1, 0), (0, 0), (1, 0), (2, 0)),
                new((0, -1), (0, 0), (0 ,1), (0, 2))
            };
        }
    }
}