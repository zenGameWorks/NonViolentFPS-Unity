using UnityEngine;

[CreateAssetMenu( menuName = "AI Kit/State" )]
public class State : ScriptableObject
{
    [SerializeField] Behaviour[] Behaviours;
    [SerializeField] EnterAction[] enterActions;
    public EnterAction[] EnterActions
    {
        get => enterActions;
        private set => enterActions = value;
    }
    [SerializeField] ExitAction[] exitActions;
    public ExitAction[] ExitActions
    {
        get => exitActions;
        private set => exitActions = value;
    }
    [SerializeField] Transition[] Transitions;

    public void UpdateState(StateMachine _stateMachine)
    {
        DoBehaviours( _stateMachine );
        EvaluateConditions( _stateMachine );
    }

    private void DoBehaviours(StateMachine _stateMachine)
    {
        foreach ( var behaviour in Behaviours )
        {
            behaviour.DoBehaviour( _stateMachine );
        }
    }

    private void EvaluateConditions(StateMachine _stateMachine)
    {
        foreach ( var transition in Transitions )
        {
            var conditionTrue = transition.condition.Evaluate( _stateMachine );

            _stateMachine.TransitionToState( conditionTrue ? transition.trueState : transition.falseState );
        }
    }

    public void Enter(StateMachine _stateMachine)
    {
        foreach ( var enterAction in EnterActions )
        {
            enterAction.Enter( _stateMachine );
        }
    }

    public void Exit(StateMachine _stateMachine)
    {
        foreach ( var exitAction in ExitActions )
        {
            exitAction.Exit( _stateMachine );
        }
    }
}