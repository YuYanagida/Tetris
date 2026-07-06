using Game.Tetris.View;
using VContainer;
using UnityEngine;
using System.Collections.Generic;
using Game.Tetris.Common;

namespace Game.Tetris.Presenter
{
    public class BlockController
    {
        [Inject] Block _blockPrefab;

        private BlockData _blockDatas;
        private MinoType _minoType;
        private Dictionary<(int x, int y), Block> _blockData = new();

        public MinoType MinoType { get { return _minoType; } set { _minoType = value; } }

        public void Init(BlockData blockData)
        {
            _blockDatas = blockData;
        }

        public void Instantiate((int x, int y) position)
        {
            var block = Object.Instantiate(_blockPrefab, new Vector2(position.x, position.y), Quaternion.identity);
            block.SetImage(Color.clear);
            _blockData.Add(position, block);
        }

        public void SetImage((int x, int y) position, ObjectType objectType)
        {
            if (!_blockData.TryGetValue(position, out var block))
                return;

            var color = objectType switch
            {
                ObjectType.Wall => _blockDatas.WallColor,
                ObjectType.Placed => _blockDatas.PlacedColor,
                ObjectType.Operated => _blockDatas.MinoColors[_minoType],
                _ => _blockDatas.NothingColor
            };

            block.SetImage(color);
        }

        public void SetSimulate(IEnumerable<(int x, int y)> positions, bool isSimulate)
        {
            foreach (var position in positions)
            {
                if (!_blockData.TryGetValue(position, out var block))
                    continue;

                block.SetSimulate(isSimulate);
                block.SetSimulateColor(_blockDatas.MinoColors[_minoType]);
            }
        }
    }
}