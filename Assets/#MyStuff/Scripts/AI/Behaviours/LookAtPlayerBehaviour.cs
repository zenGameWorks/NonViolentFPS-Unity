using UnityEngine;
using DG.Tweening;

[CreateAssetMenu( menuName = "AI Kit/Behaviours/LookAtPlayerBehaviour" )]
public class LookAtPlayerBehaviour : Behaviour
{
    public override void DoBehaviour(StateMachine _stateMachine)
    {
        // machine.Head.LookAt( machine.Player.transform, Vector3.up );
        _stateMachine.Head.DOLookAt( _stateMachine.Player.transform.position, 0.08f, AxisConstraint.None, Vector3.up ).SetEase( Ease.InOutCirc );
    }
}