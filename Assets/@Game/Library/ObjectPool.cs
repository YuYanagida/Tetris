using System.Collections.Generic;

namespace Game.Library
{
    public class ObjectPool<T1, T2>
        where T1 : SpawnObjectBase<T2>
        where T2 : SpawnParameterBase
    {
        private List<T1> _activeObjects = new(MaxStackCount);
        private Stack<T1> _releaseObjects = new(MaxStackCount);

        private const int MaxStackCount = 10;

        private Factry<T2> _factry;

        public ObjectPool(T1 prefab)
        {
            _factry = new(prefab);
        }

        /// <summary> オブジェクトを取得する </summary>
        public T1 GetObject(T2 param)
        {
            var obj = Pop(param);
            _activeObjects.Add(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        /// <summary> オブジェクトを返却する </summary>
        /// <param name="obj"></param>
        public void ReleaseObject(T1 obj)
        {
            if (obj == null) return;                

            obj.gameObject.SetActive(false);
            _activeObjects.Remove(obj);
            _releaseObjects.Push(obj);
        }

        private T1 Pop(T2 param)
        {
            if (_releaseObjects.Count > 0)
            {
                var popObj = _releaseObjects.Pop();
                popObj.Init(param);

                return popObj;
            }
            else
            {
                return _factry.Spawn(param) as T1;
            }
            
        }

    }    

    public class Factry<T>
        where T : SpawnParameterBase
    {
        private readonly SpawnObjectBase<T> _prefab;

        public Factry(SpawnObjectBase<T> spawnObject)
        {
            _prefab = spawnObject;
        }

        public SpawnObjectBase<T> Spawn(T p)
        {
            var obj = UnityEngine.Object.Instantiate(_prefab);
            obj.Init(p);
            return obj;
        }
    }
}