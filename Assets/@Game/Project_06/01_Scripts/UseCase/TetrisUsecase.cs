using Cysharp.Threading.Tasks;
using Game.Tetris.Common;
using Game.Tetris.Domain;
using Game.Tetris.Presenter;
using R3;
using VContainer;
using VContainer.Unity;
using VitalRouter;
using ZLinq;

namespace Game.Tetris.UseCase
{
    public class TetrisUsecase : IStartable, ILoading
    {
        [Inject] MinoController _minoController;
        [Inject] BlockController _blockController;
        [Inject] RowErasure _erasure;
        [Inject] MinoSpotter _minoSpotter;
        [Inject] NextBlockShower _nextBlockShower;
        [Inject] FieldSimulater _simulatField;
        [Inject] OnLoadingData _loadingData;
        [Inject] ICommandPublisher _commandPublisher;

        public void Start()
        {
            Init().Forget();
        }

        private async UniTask Init()
        {
            await UniTask.WaitUntil(() => _loadingData.IsCompleteLoad);

            Subscribe();

            _minoSpotter.Init(3);
            _minoSpotter.GetMinoShape();
        }

        private void Subscribe()
        {
            //次のミノを表示を要求された時
            _minoController.NextMinoEvent.Subscribe(_ =>
            {
                _erasure.CheckLine();
                _minoSpotter.GetMinoShape();
            });

            //ミノを能動的に動かした時
            _minoController.MoveEvent.Subscribe(positions =>
            {
                _simulatField.SimulateFinalDistination(positions.currentPosition, positions.minoPosition);
            });

            //着地予測地点が変更された時
            //Pairwiseは最初の一回は購読しないので、Takeでその分購読
            _simulatField.FinalDistination.Take(1).Subscribe(positions =>
            {
                _blockController.SetSimulate(positions, true);
            });

            //前回の位置と今回の位置を購読
            _simulatField.FinalDistination.Pairwise().Subscribe(positions =>
            {
                _blockController.SetSimulate(positions.Previous, false);
                _blockController.SetSimulate(positions.Current, true);
            });            

            //列を消した時
            _erasure.ErasedEvent.Subscribe(value =>
            {
                _minoController.DownBlock(value);                    
            });

            //新しい予告ミノをセットした時
            _minoSpotter.SetMinoEvent.Subscribe(minoType =>
            {
                _nextBlockShower.EnQueueNextBlock(minoType);
            });

            //次に表示するミノを取得した時
            _minoSpotter.GetMinoEvent.Subscribe(minoShape =>
            {
                //予告ミノの更新
                _nextBlockShower.DeQueueTopBlock();

                //出現ミノの生成
                _blockController.MinoType = minoShape.MinoType;
                var isCreate = _minoController.CreatMino(minoShape.Mino);

                //次のブロックを生成できないならばゲームオーバー
                if (!isCreate)
                    _commandPublisher.PublishAsync(new FinishGameCommand(false));
            });
        }        
    }
}