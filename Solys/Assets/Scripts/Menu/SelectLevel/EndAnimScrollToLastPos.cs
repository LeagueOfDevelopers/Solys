using UnityEngine;

public class EndAnimScrollToLastPos : StateMachineBehaviour
{

    private bool isNotMoving = true;
    /*public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<ScrollMoveHandler>().ScrollToLastPoint();
    }*/


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime > 0.7f && isNotMoving )
        {
            isNotMoving = false;
            animator.gameObject.GetComponent<ScrollMoveHandler>().ScrollToLastPoint();
            Debug.Log("MOVING!");
        }
    }
}
