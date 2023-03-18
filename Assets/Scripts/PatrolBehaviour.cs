using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour{
    private GameObject[] patrolPoints;
    private int randomPoint;
    public float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        patrolPoints = GameObject.FindGameObjectsWithTag("patrolPoints");
        randomPoint = Random.Range(0, patrolPoints.Length);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        animator.transform.position = Vector2.MoveTowards(animator.transform.position,
            patrolPoints[randomPoint].transform.position,
            speed * Time.deltaTime);
        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < 0.1f){
            randomPoint = Random.Range(0, patrolPoints.Length);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
    }
}