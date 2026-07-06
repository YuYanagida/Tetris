using Cysharp.Threading.Tasks;
using MackySoft.Navigathena.SceneManagement;
using System.Threading;
using UnityEngine;

public class HomeEntryPoint : SceneEntryPointBase
{
    protected override UniTask OnEnter(ISceneDataReader reader, CancellationToken cancellationToken)
    {
        return base.OnEnter(reader, cancellationToken);
    }
}
