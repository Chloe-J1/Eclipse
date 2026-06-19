using UnityEngine;

public class IdleBreak : StateMachineBehaviour
{

    [SerializeField]
    private float _minTimeUntilBreak;
    [SerializeField]
    private float _maxTimeUntilBreak;

    private float _timeUntilBreak;

    private bool _doIdleBreak;
    private float _idleTime;
    private int _currentIdle;




    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {

        if (!_doIdleBreak)
        {
            _idleTime += Time.deltaTime;


            if (_idleTime > _timeUntilBreak && stateinfo.normalizedTime % 1 < 0.02f)
            {
                _doIdleBreak = true;
                _currentIdle = 1;
            }
        }
        else if (stateinfo.normalizedTime % 1 > 0.98f)
        {
            ResetIdle();

        }
        animator.SetFloat("idleBreak", _currentIdle, 0.2f, Time.deltaTime);
    }



    private void ResetIdle()
    {
        _doIdleBreak = false;
        _currentIdle = 0;

        _timeUntilBreak = Random.Range(_minTimeUntilBreak, _maxTimeUntilBreak);

        _idleTime = 0;



    }

}
