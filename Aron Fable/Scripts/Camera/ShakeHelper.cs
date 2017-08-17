using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeHelper : StateMachineBehaviour {

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Shake1", false);
        animator.SetBool("Shake2", false);
    }

}
