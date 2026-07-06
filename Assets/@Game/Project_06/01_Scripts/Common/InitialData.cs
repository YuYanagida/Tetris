namespace Game.Tetris.Common
{
    public class InitialData
    {
        private int _needCount;
        private float _waitTime;
        private float _increaseTime;

        public int NeedCount => _needCount;
        public float WaitTime => _waitTime;
        public float IncreaseTime => _increaseTime;

        public void SetValue(int needCount, float waitTime)
        {
            _needCount = needCount;
            _waitTime = waitTime;
            _increaseTime = WaitTime / 100;
        }
    }
}