using Game.Tetris.Common;

namespace Game.Tetris.Domain
{
    public class LMino : MinoBase, IMinoShape
    {
        public MinoType MinoType => MinoType.Lmino;

        public MinoLine[] MinoPositions => _minoPositions;

        public MinoBase Mino => this;

        public override void Init()
        {
            _minoPositions = new MinoLine[4]
            {
                //  o o o
                //  o
                new((- 1, -1), (-1, 0), (0, 0), (1, 0)),

                //  o o
                //    o
                //    o
                new((-1, 1), (0, 1), (0, 0), (0, -1)),

                //      o
                //  o o o
                new((-1, 0), (0, 0), (1, 0), (1, 1)),

                //    o 
                //    o
                //    o o
                new((1, -1), (0, -1), (0, 0), (0, 1))
            };
        }
    }
}