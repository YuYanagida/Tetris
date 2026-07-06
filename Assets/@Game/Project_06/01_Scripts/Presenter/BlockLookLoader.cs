using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;

namespace Game.Tetris.Presenter
{
    public class BlockLookLoader
    {
        [Inject] AssetReferenceT<MinoDataSO> _minoDataAssetReafarence;

        private AsyncOperationHandle<MinoDataSO> _minoDataHandle;
        private MinoDataSO _loadData;

        public MinoDataSO LoadData => _loadData;

        public async UniTask Load()
        {

            _minoDataHandle = Addressables.LoadAssetAsync<MinoDataSO>(_minoDataAssetReafarence);
            _loadData = await _minoDataHandle.Task;
        }
    }
}