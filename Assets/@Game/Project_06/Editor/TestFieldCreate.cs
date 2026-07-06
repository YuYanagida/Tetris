using Game.Tetris.Common;
using Game.Tetris.Domain;
using NUnit.Framework;

namespace Game.Project6.Test
{
    public class TestFieldCreate
    {
        private ObjectType[,] _field;
        private (int x, int y) _createPoint;

        private int x;
        private int y;

        [SetUp]
        public void SetUp()
        {
            x = 8;
            y = 20;

            FieldController fieldCreater = new();

            _field = fieldCreater.CreateField(x, y);
            _createPoint = (x / 2, y);
            fieldCreater.CreateWall(_field);
        }

        [Test]
        public void A_フィールドの大きさを決定する()
        {
            int count = 0;
            foreach (var objectType in _field)
            {
                if(objectType == ObjectType.Putable)
                    count++;
            }

            Assert.That(count, Is.EqualTo(x * y));
        }

        [Test]
        public void A_床と壁を生成する()
        {
            //床
            for (int i = 0; i < x; i++)
            {
                Assert.That(_field[i, 0], Is.EqualTo(ObjectType.Wall));
            }

            //壁
            for (int i = 0; i < y; i++)
            {
                Assert.That(_field[0, i], Is.EqualTo(ObjectType.Wall));
                Assert.That(_field[x + 1, i], Is.EqualTo(ObjectType.Wall));
            }
        }

        [Test]
        public void B_ミノの生成()
        {
            IMino imino = new();

            var able = TryCreate(imino.MinoPosition);

            Assert.That(able, Is.EqualTo(true));
        }

        private bool TryCreate((int x, int y)[] minoPositions)
        {
            foreach (var minoPos in minoPositions)
            {
                if (_field[_createPoint.x + minoPos.x, _createPoint.y + minoPos.y] != ObjectType.Putable)
                    return false;
            }

            return true;
        }
    }
}