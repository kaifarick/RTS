using UnityEngine;

public class followBehavior : StateMachineBehaviour
{

    private Transform playerPos;
    private Transform castle;

    HammerEnemy hammer;

    int range = 100;
    float timeBetweenAtack;

    LayerMask mask;

    [SerializeField]
    private float Speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       castle = GameObject.Find("MainCastle").transform;
       mask = LayerMask.GetMask("Player");
       hammer = animator.gameObject.GetComponent<HammerEnemy>();
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

            if (dist > 1.5f)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, playerPos.position, Speed * Time.deltaTime);
            }
            else
            {
                if (timeBetweenAtack <= 0)
                {
                    HammerFriend units = currentCollider.gameObject.GetComponent<HammerFriend>();
                    units.Health -= hammer.Damage;
                    timeBetweenAtack = 2f;
                }
                else timeBetweenAtack -= Time.deltaTime;
            }
        }
        catch
        {
            float currentDist = Vector3.Distance(animator.gameObject.transform.position, castle.transform.position);
            if (currentDist > 3.5f)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, castle.position, Speed * Time.deltaTime); ;
            }
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
