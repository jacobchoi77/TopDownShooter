using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour{
    private GameObject player;

    public float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        if (player != null){
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.transform.position,
                speed * Time.deltaTime);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}