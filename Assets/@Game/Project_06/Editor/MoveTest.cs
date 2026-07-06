using Game.Tetris.Common;
using Game.Tetris.Domain;
using Game.Tetris.UseCase;
using NUnit.Framework;
using System.Collections.Generic;
using VContainer;
using ZLinq;

namespace Game.Project6.Test
{
    [TestFixture]
    public class MoveTest
    {
        private FieldController _fieldCreater;
        private MinoController _minoController;
        
        private ObjectType[,] _field;

        private int width;
        private int hight;

        [SetUp]
        public void SetUp()
        {
            //Container=========================

            _fieldCreater = new();
            _minoController = new(_fieldCreater);

            //Init==============================

            width = 10;
            hight = 20;

            _field = _fieldCreater.CreateField(width, hight);
            _fieldCreater.CreateWall(_field);
        }

        [Test]
        public void B_01ミノを生成できる()
        {
            var mino = new IMino();           
            _minoController.CreatMino(mino);
            (int width, int hight) createPoint = _fieldCreater.CreatePoint;

            CheckBlockIsOperated(mino.MinoPosition, createPoint);
        }

        [Test]
        public void B_02ミノが落下する()
        {
            var mino = new IMino();
            _minoController.CreatMino(mino);
            _minoController.FallMino();

            Assert.That(_fieldCreater.IsObjectType(6, 20, ObjectType.Putable));
        }

        [Test]
        public void B_03ミノを横移動できる()
        {
            //適当なミノをフィールドに設置する
            var mino = new IMino();
            _minoController.CreatMino(mino);

            int one = 1;

            //移動
            _minoController.MoveX(one);

            //移動分ズレたマス
            (int width, int hight) movedPoint = (_fieldCreater.CreatePoint.x + one, _fieldCreater.CreatePoint.y);

            CheckBlockIsOperated(mino.MinoPosition, movedPoint);
        }

        [Test]
        public void B_04ミノを回転できる()
        {
            var mino = new IMino();
            _minoController.CreatMino(mino);

            _minoController.Rotate(true);

            var rotatePoint = _fieldCreater.CreatePoint;

            CheckBlockIsOperated(mino.MinoPosition, rotatePoint);
        }

        [Test]
        public void C_01列が消えた時に上に残ったブロックが一段下がる()
        {
            for (int x = 0; x < width; x++)
            {
                _field[x, 1] = ObjectType.Putable;
            }

            _field[2, 2] = ObjectType.Placed;
            _field[3, 2] = ObjectType.Placed;
            _field[4, 2] = ObjectType.Placed;

            _minoController.DownBlock(1);

            Assert.That(_field[2, 1] == ObjectType.Placed);
            Assert.That(_field[3, 1] == ObjectType.Placed);
            Assert.That(_field[4, 1] == ObjectType.Placed);
        }

        [Test]
        public void C_02任意のマス分落下中のミノを下に落とせる()
        {
            var mino = new IMino();
            _minoController.CreatMino(mino);

            _minoController.SetSimulatePoint((0, 1));

            Assert.That(_field[6, 1] == ObjectType.Operated);
        }

        private void CheckBlockIsOperated((int x, int y)[] positions, (int x, int y) operatedPoint)
        {
            var minoPositions = positions.AsValueEnumerable().Select(position => (position.x + operatedPoint.x, position.y + operatedPoint.y));

            for (int y = 0; y < _field.GetLength(1); y++)
            {
                for (int x = 0; x < _field.GetLength(0); x++)
                {
                    if (minoPositions.Contains((x, y)))
                        Assert.That(_fieldCreater.IsObjectType(x, y, ObjectType.Operated));
                    else
                        Assert.That(!_fieldCreater.IsObjectType(x, y, ObjectType.Operated));
                }
            }
        }
    }
}