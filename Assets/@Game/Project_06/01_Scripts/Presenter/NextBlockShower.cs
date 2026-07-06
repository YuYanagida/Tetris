using Game.Tetris.Common;
using Game.Tetris.View;
using VContainer;

namespace Game.Tetris.Presenter
{
    public class NextBlockShower
    {
        [Inject] private NextBlockUI _nextBlockUI;
        private BlockData _blockData;

        public void Init(BlockData blockData)
        {
            _blockData = blockData;
        }

        public void EnQueueNextBlock(MinoType minoType)
        {
            _nextBlockUI.ChangeImage(_blockData.MinoShapes[minoType], _blockData.MinoColors[minoType]);
        }

        public void DeQueueTopBlock()
        {
            _nextBlockUI.HideTopImage();
        }
    }
}