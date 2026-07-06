using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Tetris.View
{
    public class InputView : MonoBehaviour
    {
        [SerializeField] private InputAction _inputAction;

        public Subject<(int, int)> _moveAction = new();

        public Observable<(int, int)> MoveAction => _moveAction;
    }
}