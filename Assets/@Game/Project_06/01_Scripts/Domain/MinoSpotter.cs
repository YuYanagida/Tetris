using Game.Tetris.Common;
using R3;
using System;
using System.Collections.Generic;
using ZLinq;

namespace Game.Tetris.Domain
{
    public class MinoSpotter
    {
        private readonly Random _random = new();
        private Queue<MinoType> _minoQueue = new();

        private Subject<MinoType> _setMinoEvent = new();
        private Subject<IMinoShape> _getMinoEvent = new();

        public Observable<MinoType> SetMinoEvent => _setMinoEvent;
        public Observable<IMinoShape> GetMinoEvent => _getMinoEvent;

        public void Init(int stockCount)
        {
            for (int i = 0; i < stockCount; i++)
            {
                SetMinoType();
            }
        }

        public void SetMinoType()
        {
            var value = _random.Next(0, Enum.GetNames(typeof(MinoType)).Length);
            _minoQueue.Enqueue((MinoType)value);
            _setMinoEvent.OnNext((MinoType)value);
        }

        public void GetMinoShape()
        {
            var minoType = _minoQueue.Dequeue();
            _getMinoEvent.OnNext(MinoShape(minoType));
            SetMinoType();
        }

        private IMinoShape MinoShape(MinoType minoType)
        {
            return minoType switch
            {
                MinoType.Imino => new IMino(),
                MinoType.Jmino => new JMino(),
                MinoType.Lmino => new LMino(),
                MinoType.Omino => new OMino(),
                MinoType.Smino => new SMino(),
                MinoType.Tmino => new TMino(),
                MinoType.Zmono => new ZMino(),
                _ => new IMino()
            };
        }
    }
}