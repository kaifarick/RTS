using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBehavior : StateMachineBehaviour
{

    private Transform playerPos;
    private Transform castle;

    int range = 100;

    LayerMask mask;

    [SerializeField]
    private float Speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       castle = GameObject.Find("MainCastle").transform;
       mask = LayerMask.GetMask("Player");
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var cols = Physics.OverlapSphere(animator.gameObject.transform.position, range, mask.value);
        float dist = Mathf.Infinity;

        try
        {
            Collider currentCollider = cols[0];

            foreach (Collider col in cols)
            {
                float currentDist = Vector3.Distance(animator.gameObject.transform.position, col.transform.position);
                if (currentDist < dist)
                {
                    currentCollider = col;
                    dist = currentDist;
                }
            }
            playerPos = currentCollider.gameObject.transform;
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, playerPos.position, Speed * Time.deltaTime);
        }        
        catch
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, castle.position, Speed * Time.deltaTime); ; ;
        }       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
