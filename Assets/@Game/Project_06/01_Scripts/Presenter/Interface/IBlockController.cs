using Game.Tetris.Common;

namespace Game.Tetris.Presenter
{
    public interface IBlockController
    {
        public void Instantiate((int x, int y) position, BlockData blockData);

        public void SetImage((int x, int y) position, ObjectType objectType);
    }
}