using Cysharp.Threading.Tasks;
using Game.Tetris.Common;
using Game.Tetris.Domain;
using Game.Tetris.Presenter;
using R3;
using System;
using System.Threading;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VitalRouter;

namespace Game.Tetris.UseCase
{
    [Routes]
    public partial class GameLoop : IStartable, IDisposable
    {
        [Inject] MinoController _minoController;
        [Inject] RowErasure _rowErasure;
        [Inject] Presenter.Result _result;
        [Inject] OnLoadingData _onLoadingData;
        [Inject] InitialData _initialData;
        [Inject] ScoreCounter _scoreCounter;

        private bool _isGame;
        private bool _isClear;        
        private float _waitTime = .5f;
        private CancellationTokenSource _cancellationTokenSource = new();

        public void Start()
        {
            _waitTime = _initialData.WaitTime;
            _scoreCounter.Init();

            Subscribe();

            StartGame().Forget();
        }

        public void On(FinishGameCommand cmd)
        {
            _cancellationTokenSource.Cancel();

            GameEnd(cmd.IsClear);
        }

        private async UniTask StartGame()
        {
            await UniTask.WaitUntil(() => _onLoadingData.IsCompleteLoad, cancellationToken: _cancellationTokenSource.Token);

            await MainGame();
        }

        private void Subscribe()
        {
            _rowErasure.ErasedEvent.Subscribe(value =>
            {
                _waitTime -= _initialData.IncreaseTime;
                _scoreCounter.Count();
            });

            _result.Subscribe();
        }

        private async UniTask MainGame()
        {
            _isGame = true;

            while (_isGame)
            {
                await UniTask.WaitForSeconds(_waitTime, ignoreTimeScale: false, cancellationToken: _cancellationTokenSource.Token);
                _minoController.FallMino();
            }

            _result.ShowResult(_isClear);
        }

        private void GameEnd(bool isClear)
        {
            _isGame = false;
            _isClear = isClear;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}