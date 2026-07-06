using NUnit.Framework;


namespace Game.Library.Test
{
    public class TestObjectPool
    {
        public sealed class TestParameter : SpawnParameterBase
        {
            public int IntValue;
            public float FloatValue;
            public string StringValue;
        }

        public sealed class TestObject : SpawnObjectBase<TestParameter>
        {

        }

        private ObjectPool<TestObject, TestParameter> _objectPool;
        private TestObject _objPrefab;

        [OneTimeSetUp]
        public void Init()
        {
            var obj = UnityEngine.Object.Instantiate(new UnityEngine.GameObject());
            obj.AddComponent<TestObject>();
            _objPrefab = obj.GetComponent<TestObject>();

            _objectPool = new(_objPrefab);
        }

        [Test]
        public void A_オブジェクトを生成する()
        {
            TestParameter param = new();
            var newObj = _objectPool.GetObject(param);

            Assert.That(newObj != null);            
        }

        [Test]
        public void A_オブジェクトを非表示にできる()
        {
            TestParameter param = new();
            var newObj = _objectPool.GetObject(param);

            _objectPool.ReleaseObject(newObj);

            Assert.That(newObj != null);
            Assert.That(newObj.gameObject.activeSelf == false);
        }

        [Test]
        public void A_プールのオブジェクトを再利用できる()
        {
            TestParameter param = new();
            var objA = _objectPool.GetObject(param);

            _objectPool.ReleaseObject(objA);

            var objB = _objectPool.GetObject(param);

            Assert.That(objA.gameObject == objB.gameObject);
        }

        [Test]
        public void B_パラメータが正しく入力されている()
        {
            int intValue = 1;
            float floatValue = 0.5f;
            string stringValue = "Test";

            TestParameter param = new() { IntValue = intValue, FloatValue = floatValue, StringValue = stringValue };
            var newObj = _objectPool.GetObject(param);

            Assert.That(newObj.SpawnParameter.IntValue == intValue);
            Assert.That(newObj.SpawnParameter.FloatValue == floatValue);
            Assert.That(newObj.SpawnParameter.StringValue == stringValue);
        }        
    }
}