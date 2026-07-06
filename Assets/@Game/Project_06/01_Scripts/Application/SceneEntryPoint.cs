using Cysharp.Threading.Tasks;
using Game.Tetris.Common;
using Game.Tetris.Domain;
using Game.Tetris.Presenter;
using Game.Tetris.UseCase;
using MackySoft.Navigathena;
using MackySoft.Navigathena.SceneManagement;
using MackySoft.Navigathena.SceneManagement.VContainer;
using System;
using System.Threading;
using VContainer;

namespace Game.Tetris.LifeCycle
{
    public class SceneEntryPoint : SceneLifecycleBase
    {
        [Inject] private FieldController _useCase;
        [Inject] private InitialData _initialData;

        protected override UniTask OnEnter(ISceneDataReader reader, CancellationToken cancellationToken)
        {
            if (reader.TryRead(out TetrisGameData sceneData))
            {
                _useCase.CreateField(sceneData.FieldWidth, sceneData.FieldHight);
                _initialData.SetValue(sceneData.DeleteCulmsCount, sceneData.WaitTime);
            }
            return UniTask.CompletedTask;
        }
        /*
        protected override UniTask OnExit(ISceneDataWriter writer, CancellationToken cancellationToken)
        {
            writer.Write(new TetrisGameData(10, 20, 50, .5f));
            return UniTask.CompletedTask;
        }
        */
#if UNITY_EDITOR

        protected override UniTask OnEditorFirstPreInitialize(ISceneDataWriter writer, CancellationToken cancellationToken)
        {
            // ISceneDataWriterÇ…èâä˙ÉfÅ[É^ÇèëÇ´çûÇﬁ
            writer.Write(new TetrisGameData(10, 20, 50, .5f));
            return UniTask.CompletedTask;
        }
#endif

    }

    public sealed class TetrisGameData : ISceneData
    {
        public int FieldWidth { get; }
        public int FieldHight {  get; }
        public int DeleteCulmsCount {  get; }
        public float WaitTime {  get; }

        public TetrisGameData(int x, int y, int deleteCulmsCount, float waitTime)
        {
            FieldWidth = x;
            FieldHight = y;
            DeleteCulmsCount = deleteCulmsCount;
            WaitTime = waitTime;
        }

        public TetrisGameData()
        {
        }
    }
}