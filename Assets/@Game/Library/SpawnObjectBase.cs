using UnityEngine;

namespace Game.Library
{
    public abstract class SpawnParameterBase { }

    public abstract class SpawnObjectBase<T> : MonoBehaviour
        where T : SpawnParameterBase
    {
        protected T _spawnParameter;
        public T SpawnParameter => _spawnParameter;

        public void Init(T spawnParam)
        {
            _spawnParameter = spawnParam;
        }
    }
}