using Game.Tetris.Domain;
using Game.Tetris.UseCase;
using MackySoft.Navigathena.SceneManagement.VContainer;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Game.Tetris.View;
using Game.Tetris.Presenter;
using UnityEngine.AddressableAssets;
using Game.Tetris.Common;
using AudioConductor.Core.Models;
using VitalRouter.VContainer;

namespace Game.Tetris.LifeCycle
{
    public class TetrisLifetimeScope : LifetimeScope
    {
        [SerializeField] private Block _blockPrefab;
        [SerializeField] private AssetReferenceT<MinoDataSO> _minoData;
        [SerializeField] private AssetReferenceT<AudioConductorSettings> _audioConductorSettings;
        [SerializeField] private AssetReferenceT<CueSheetAsset> _cueSheetAsset;

        protected override void Configure(IContainerBuilder builder)
        {
            //View
            builder.RegisterComponentInHierarchy<NextBlockUI>();
            builder.RegisterComponentInHierarchy<OperateUI>();
            builder.RegisterComponentInHierarchy<ResultUI>();
            builder.RegisterComponentInHierarchy<ScoreCounterView>();
            
            //Presenter            
            builder.Register<BlockController>(Lifetime.Singleton).WithParameter(_blockPrefab);
            builder.Register<BlockLookLoader>(Lifetime.Singleton).WithParameter(_minoData);  
            builder.Register<NextBlockShower>(Lifetime.Singleton);                      
            builder.Register<ActionController>(Lifetime.Singleton);                                    

            //Domain
            builder.Register<FieldSimulater>(Lifetime.Singleton);            
            builder.Register<FieldController>(Lifetime.Singleton);            
            builder.Register<MinoController>(Lifetime.Singleton);            
            builder.Register<MinoSpotter>(Lifetime.Singleton);
            builder.Register<RowErasure>(Lifetime.Singleton);
            builder.Register<Result>(Lifetime.Singleton);
            builder.Register<ScoreCounter>(Lifetime.Singleton);

            //UseCase
            builder.RegisterEntryPoint<TetrisUsecase>(Lifetime.Singleton);
            builder.RegisterEntryPoint<FieldSetter>(Lifetime.Singleton);
            
            //Common
            builder.Register<OnLoadingData>(Lifetime.Singleton);
            builder.Register<InitialData>(Lifetime.Singleton);

            //Scene
            builder.RegisterSceneLifecycle<SceneEntryPoint>();

            //Map
            builder.RegisterVitalRouter(routing =>
            {
                routing.MapEntryPoint<GameLoop>();
                routing.MapEntryPoint<ActionUseCase>();
                routing.MapEntryPoint<AudioUsecase>();
            });
        }
    }
}