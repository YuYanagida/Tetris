using Cysharp.Threading.Tasks;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Library
{
    public class LoadData<T> : IDisposable
        where T : UnityEngine.Object
    {
        private AssetReferenceT<T> _assetReference;
        private AsyncOperationHandle<T> _handle;
        private T _data;

        public T Data => _data;

        public LoadData(AssetReferenceT<T> assetReferenceT)
        {
            _assetReference = assetReferenceT;
        }

        public async UniTask Load()
        {
            _handle = Addressables.LoadAssetAsync<T>(_assetReference);
            _data = await _handle.Task;
        }

        public void Release()
        {
            Addressables.Release(_handle);
        }

        public void Dispose()
        {
            Release();
        }
    }
}