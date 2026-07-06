using Cysharp.Threading.Tasks;
using Game.Tetris.Domain;
using Game.Tetris.Presenter;
using VContainer;
using R3;
using VContainer.Unity;
using Game.Tetris.Common;

namespace Game.Tetris.UseCase
{
    public class FieldSetter : IStartable, ILoading
    {        
        [Inject] private FieldController _fieldController;
        [Inject] private BlockController _blockController;
        [Inject] private BlockLookLoader _blockLookLoader;
        [Inject] private NextBlockShower _nextBlockShower;
        [Inject] private OnLoadingData _loadingData;

        public void Start()
        {
            Init().Forget();
        }

        public async UniTask Init()
        {
            _loadingData.BeginLoading(this);

            await _blockLookLoader.Load();
            var loadData = _blockLookLoader.LoadData;

            BlockData blockData = new(loadData);

            _blockController.Init(blockData);
            _nextBlockShower.Init(blockData);

            //var field = _fieldController.CreateField(x, y);

            for (int i = 0; i < _fieldController.FieldWidth; i++)
            {
                for (int j = 0; j < _fieldController.FieldHeight; j++)
                {
                    _blockController.Instantiate((i, j));
                }
            }

            _fieldController.ChangeTypeEvent.Subscribe(value =>
            {
                _blockController.SetImage(value.position, value.type);
            });

            _fieldController.CreateWall();

            _loadingData.CompleteLoad(this);
        }        
    }
}