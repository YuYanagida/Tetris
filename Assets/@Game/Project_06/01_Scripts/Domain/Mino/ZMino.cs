using Game.Tetris.Common;

namespace Game.Tetris.Domain
{
    public class ZMino : MinoBase, IMinoShape
    {
        public MinoType MinoType => MinoType.Zmono;

        public MinoLine[] MinoPositions => _minoPositions;

        public MinoBase Mino => this;

        public override void Init()
        {
            _minoPositions = new MinoLine[2]
                {
                    //  o o 
                    //    o o
                    new((-1, 0), (0, 0), (0, -1), (1, -1)),

                    //    o
                    //  o o
                    //  o
                    new((-1, -1), (-1, 0), (0, 0), (0, 1))
                };
        }
    }
}