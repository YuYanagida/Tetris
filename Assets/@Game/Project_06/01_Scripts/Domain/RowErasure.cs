using Game.Tetris.Common;
using R3;
using System.Collections.Generic;
using VContainer;

namespace Game.Tetris.Domain
{
    public class RowErasure
    {
        [Inject] private FieldController _fieldController;
        private Stack<int> _eventStack = new();

        private Subject<int> _erasedEvent = new();

        public Observable<int> ErasedEvent => _erasedEvent;

        public void CheckLine()
        {
            int putableCount = 0;

            for (int y = 1; y < _fieldController.FieldHeight; y++)
            {
                for (int x = 0; x < _fieldController.FieldWidth; x++)
                {
                    if (_fieldController.IsObjectType(x, y, ObjectType.Putable, ObjectType.Operated))
                        putableCount++;
                }

                if (putableCount == 0)
                {
                    ErasuredLine(y);
                    _eventStack.Push(y);
                }
                else if (putableCount >= _fieldController.FieldWidth - 2)
                {
                    break;
                }

                putableCount = 0;
            }

            while (_eventStack.Count > 0)
            {
                var y = _eventStack.Pop();
                _erasedEvent.OnNext(y);
            }                
        }


        public void ErasuredLine(int y)
        {
            for (int x = 1; x < _fieldController.FieldWidth - 1; x++)
            {
                _fieldController.ChangeField(x, y, Common.ObjectType.Putable);
            }
        }
    }
}