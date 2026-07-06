using Game.Tetris.Common;
using R3;
using ZLinq;

namespace Game.Tetris.Domain
{
    public class FieldController
    {
        private ObjectType[,] _field;
        private int _fieldWidth;
        private int _fieldHeight;
        private (int x, int y) _createPoint;
        private Subject<((int x, int y), ObjectType)> _changeTypeEvent = new();

        public int FieldWidth => _fieldWidth;
        public int FieldHeight => _fieldHeight;
        public (int x, int y) CreatePoint => _createPoint;

        public Observable<((int x, int y) position, ObjectType type)> ChangeTypeEvent => _changeTypeEvent;

        public bool IsObjectType(int x, int y, params ObjectType[] targetTypes)
        {
            try
            {
                return targetTypes.AsValueEnumerable().Contains(_field[x, y]);
            }
            catch
            {
                return targetTypes.AsValueEnumerable().Contains(ObjectType.Putable);
            }
            
        }

        public void ChangeField(int x, int y, ObjectType changeType)
        {
            try
            {
                _field[x, y] = changeType;
                _changeTypeEvent.OnNext(((x, y), changeType));
            }
            catch
            {
                return;
            }
        }

        public ObjectType[,] CreateField(int x, int y)
        {
            //壁と床のスペースも含む
            _field = new ObjectType[x + 2, y + 1];
            _fieldWidth = _field.GetLength(0);
            _fieldHeight = _field.GetLength(1);
            _createPoint = ((x + 2) / 2, y);

            return _field;
        }

        /// <summary> 壁と床を生成する </summary>
        public void CreateWall(ObjectType[,] field = default)
        {
            //床
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                //生成処理
                _field[i, 0] = ObjectType.Wall;
                _changeTypeEvent.OnNext(((i, 0), ObjectType.Wall));
            }

            //壁
            for (int i = 0; i < _field.GetLength(1); i++)
            {
                //生成処理
                //左の壁
                _field[0, i] = ObjectType.Wall;
                _changeTypeEvent.OnNext(((0, i), ObjectType.Wall));

                //右の壁                
                _field[_field.GetLength(0) - 1, i] = ObjectType.Wall;
                _changeTypeEvent.OnNext(((_field.GetLength(0) - 1, i), ObjectType.Wall));
            }
        }
    }
}