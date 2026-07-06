using Cysharp.Threading.Tasks;
using Game.Tetris.LifeCycle;
using MackySoft.Navigathena.SceneManagement;
using MackySoft.Navigathena.Transitions;
using R3;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tetris
{

    public class TetrisSceneMover : MonoBehaviour
    {
        [SerializeField] private List<Button> _selectStageButtons;
        [SerializeField] private List<int> _deleteCulmCounts;
        [SerializeField] private List<float> _waitTimes;

        //private ISceneIdentifier _titleScene = new BuiltInSceneIdentifier("");
        //private ISceneIdentifier _homeScene = new BuiltInSceneIdentifier("");
        private ISceneIdentifier _mainScene = new BuiltInSceneIdentifier("Project06");

        //private ITransitionDirector _transitionDirector;

        private void Start()
        {
            for (int i = 0; i < _selectStageButtons.Count; i++)
            {
                int j = i;

                _selectStageButtons[j].OnClickAsObservable().SubscribeAwait(async (_, ct) =>
                {
                    await MoveScene(_deleteCulmCounts[j], _waitTimes[j]);
                }, AwaitOperation.Drop).AddTo(this);
            }
        }

        public async UniTask MoveScene(int deleteCount, float waitTime)
        {
            await GlobalSceneNavigator.Instance.Push(_mainScene, data : new TetrisGameData(10, 20, deleteCount, waitTime));
        }
    }
}