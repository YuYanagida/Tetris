using Game.Tetris.Common;
using Game.Tetris.Domain;
using R3;
using System;
using VContainer;

namespace Game.Tetris.UseCase
{
    public class MinoController
    {
        private FieldController _fieldController;

        private (int x, int y) _currentPoint;
        private MinoBase _currentMino;

        private Subject<((int, int), (int x, int y)[])> _moveEvent = new();
        private Subject<Unit> _nextMinoEvent = new();
        private Subject<Unit> _minoStopEvent = new();

        public Observable<((int x, int y) currentPosition, (int x, int y)[] minoPosition)> MoveEvent => _moveEvent;
        public Observable<Unit> NextMinoEvent => _nextMinoEvent;
        public Observable<Unit> MinoStopEvent => _minoStopEvent;

        [Inject]
        public MinoController(FieldController fieldController)
        {
            _fieldController = fieldController;
        }

        public bool CreatMino(MinoBase mino, (int x, int y) createPoint = default)
        {
            _currentMino = mino;

            //ê∂ê¨èÍèä
            var point = createPoint == default ? _fieldController.CreatePoint : createPoint;
            _currentPoint = point;

            bool creatable = TryMove(0, 0);

            if (creatable)
            {
                foreach (var (x, y) in _currentMino.MinoPosition)
                {
                    int pointX = point.x + x;
                    int pointY = point.y + y;

                    _fieldController.ChangeField(pointX, pointY, ObjectType.Operated);
                }

                _moveEvent.OnNext((_currentPoint, _currentMino.MinoPosition));
            }

            return creatable;
        }

        public void FallMino()
        {
            if (_currentMino == null) return;            

            bool fallable = TryMove(0, -1);

            if (fallable)
            {
                UpdateBlock(0, -1);
            }

            else
            {
                foreach (var (x, y) in _currentMino.MinoPosition)
                {
                    var point = CurrentPoint(x, y);

                    _fieldController.ChangeField(point.x, point.y, ObjectType.Placed);
                }

                _currentMino = null;
                _nextMinoEvent.OnNext(Unit.Default);
            }
        }

        public void SetSimulatePoint((int x, int y)simulatePoint)
        {
            var distance = Math.Abs(_currentPoint.y - simulatePoint.y);

            UpdateBlock(0, -distance);
        }

        public void MoveX(int x)
        {
            var movable = TryMove(x, 0);

            if (!movable) return;

            UpdateBlock(x, 0);
            _moveEvent.OnNext((_currentPoint, _currentMino.MinoPosition));
        }

        public void Rotate(bool isRight)
        {
            var beforePosition = _currentMino.MinoPosition;
            _currentMino.Rotate(isRight);

            var movable = TryMove(0, 0);

            if (movable)
            {
                foreach (var (x, y) in beforePosition)
                {
                    var point = CurrentPoint(x, y);
                    _fieldController.ChangeField(point.x, point.y, ObjectType.Putable);
                }

                foreach (var (x, y) in _currentMino.MinoPosition)
                {
                    var point = CurrentPoint(x, y);
                    _fieldController.ChangeField(point.x, point.y, ObjectType.Operated);
                }

                _moveEvent.OnNext((_currentPoint, _currentMino.MinoPosition));
            }
            else
            {
                _currentMino.Rotate(!isRight);
            }
        }        

        public void DownBlock(int erasedRow)
        {
            for (int y = erasedRow; y < _fieldController.FieldHeight; y++)
            {
                for (int x = 1; x < _fieldController.FieldWidth - 1; x++)
                {
                    bool isPlaced = _fieldController.IsObjectType(x, y, ObjectType.Placed);

                    if (!isPlaced) continue;

                    _fieldController.ChangeField(x, y, ObjectType.Putable);
                    _fieldController.ChangeField(x, y - 1, ObjectType.Placed);
                }
            }
        }

        private bool TryMove(int x, int y)
        {
            foreach (var (PosX, PosY) in _currentMino.MinoPosition)
            {
                if (!_fieldController.IsObjectType(_currentPoint.x + PosX + x, _currentPoint.y + PosY + y, ObjectType.Putable, ObjectType.Operated))
                {
                    return false;
                }
            }

            return true;
        }

        private void UpdateBlock(int X, int Y)
        {
            var beforePoint = _currentPoint;
            _currentPoint = (_currentPoint.x + X, _currentPoint.y + Y);

            foreach (var (x, y) in _currentMino.MinoPosition)
            {
                var point = (beforePoint.x + x, beforePoint.y + y);

                _fieldController.ChangeField(point.Item1, point.Item2, ObjectType.Putable);
            }

            foreach (var (x, y) in _currentMino.MinoPosition)
            {
                var nextPoint = CurrentPoint(x, y);

                _fieldController.ChangeField(nextPoint.x, nextPoint.y, ObjectType.Operated);
            }
        }

        private (int x, int y) CurrentPoint(int x, int y)
        {
            return (_currentPoint.x + x, _currentPoint.y + y);
        }
    }
}