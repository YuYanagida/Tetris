
using Game.Tetris.Common;
using R3;
using System.Collections.Generic;
using VContainer;
using ZLinq;

namespace Game.Tetris.Domain
{
    public class FieldSimulater
    {
        [Inject] private FieldController _fieldController;

        private (int, int) _simulatePosition;
        private Subject<IEnumerable<(int, int)>> _finalDistination = new();

        public Observable<IEnumerable<(int, int)>> FinalDistination => _finalDistination;
        public (int, int) SimulatePosition => _simulatePosition;

        public void SimulateFinalDistination((int x, int y)currentPoint, (int x, int y)[] minoPosition)
        {
            bool isFinalPoint = false;
            var checkPoint = currentPoint;

            while (!isFinalPoint)
            {

                foreach (var (x, y) in minoPosition)
                {
                    var nextPositionX = checkPoint.x + x;
                    var nextPositionY = checkPoint.y + y - 1;
                    if (_fieldController.IsObjectType(nextPositionX, nextPositionY, ObjectType.Placed, ObjectType.Wall))
                    {
                        isFinalPoint = true; 
                        break;
                    }
                }

                if (isFinalPoint)
                    break;

                checkPoint = (checkPoint.x, checkPoint.y - 1);
            }

            var final = minoPosition.AsValueEnumerable().Select(positions => (checkPoint.x + positions.x, checkPoint.y + positions.y)).ToList();

            _finalDistination.OnNext(final);
            _simulatePosition = checkPoint;
        }
    }
}