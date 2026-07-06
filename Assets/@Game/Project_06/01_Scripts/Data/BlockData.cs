using Game.Tetris.Common;
using System.Collections.Generic;
using UnityEngine;
using ZLinq;

namespace Game.Tetris.Presenter
{
    public struct BlockData
    {
        private Color _wallColor;
        private Color _placedColor;
        private Color _nothingColor;
        private Dictionary<MinoType, Color> _minoColors;
        private Dictionary<MinoType, Sprite> _minoShapes;

        public readonly Color WallColor => _wallColor;
        public readonly Color PlacedColor => _placedColor;
        public readonly Color NothingColor => _nothingColor;
        public readonly IReadOnlyDictionary<MinoType, Color> MinoColors => _minoColors;
        public readonly IReadOnlyDictionary<MinoType, Sprite> MinoShapes => _minoShapes;

        public BlockData(MinoDataSO minodata)
        {
            _wallColor = minodata.WallColor;
            _placedColor = minodata.PlacedColor;
            _nothingColor = minodata.NothingColor;
            _minoColors = minodata.MinoData.AsValueEnumerable().ToDictionary(data => data.MinoType, data => data.Color);
            _minoShapes = minodata.MinoData.AsValueEnumerable().ToDictionary(data => data.MinoType, data => data.Shape);
        }
    }
}