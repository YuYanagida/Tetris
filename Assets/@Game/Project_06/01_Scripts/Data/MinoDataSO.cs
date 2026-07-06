using Game.Tetris.Common;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinoDataSO", menuName = "ScriptableObjects/Game/Tetris/MinoDataSO")]
public class MinoDataSO : ScriptableObject
{
    [SerializeField] private Color _wallColor;
    [SerializeField] private Color _placedColor;
    [SerializeField] private Color _nothingColor;
    [SerializeField] private List<MinoData> _minoData;

    public Color WallColor => _wallColor;
    public Color PlacedColor => _placedColor;
    public Color NothingColor => _nothingColor;
    public IReadOnlyList<MinoData> MinoData => _minoData;
}

[System.Serializable]
public class MinoData
{
    [SerializeField] private MinoType _minoType;
    [SerializeField] private Color _color;
    [SerializeField] private Sprite _shape;

    public MinoType MinoType => _minoType;
    public Color Color => _color;
    public Sprite Shape => _shape;
}