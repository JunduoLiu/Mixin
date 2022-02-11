using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemypatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    private float alertTime;
    public float startWaitTime;
    public float alertDistance;
    public float startAlertTime;
    public Transform player;
    public Transform[] patrolPoints;
    public int currentPoint;
    public Transform currentGoal;
    public Animator anim;

    private void ChangeGoal(){
        if(currentPoint == patrolPoints.Length - 1){
            currentPoint = 0;
            currentGoal = patrolPoints[0];
            Debug.Log("no more points");
        }
        else
        {
            currentPoint++;
            Debug.Log("currentPoint:" + currentPoint);
            currentGoal = patrolPoints[currentPoint];
        }
    }

    void Start(){
        waitTime = startWaitTime;
        alertTime = startAlertTime;
        currentPoint = 0;
        anim = GetComponent<Animator>();
    }

    public void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX",setVector.x);
        anim.SetFloat("moveY",setVector.y);
    }

    public void changeAnim(Vector2 direction){
        if (direction.x>0){
            SetAnimFloat(Vector2.right);
        }else if (direction.x<0){
            SetAnimFloat(Vector2.left);
        }
    }

    void Update(){

      float distance_bt_goose_player = Vector2.Distance(transform.position, player.position);
      
      if (distance_bt_goose_player <= alertDistance){// if the goose and the player is smaller than alertDistance
            //the goose will not move and will countdown from alerttime
            //if alertime reaches 0, the game is over
            //Debug.Log("within alert distance");
            if (alertTime <= 0)
            {
                //Destroy(player); //?
                alertTime = startAlertTime;
                Debug.Log("Destroy player");
            }else{
                alertTime -= Time.deltaTime;
            }
      }
      else{
            //goose move to next position 
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);
            float distance_bt_goose_nextP = Vector2.Distance(transform.position, patrolPoints[currentPoint].position);
            if (distance_bt_goose_nextP < 0.2f) //if arrived at the position 
            {
                // wait for startWaitTime 
                if (waitTime <= 0)
                {
                    //Debug.Log("change goal");
                    ChangeGoal();
                    waitTime = startWaitTime;
                }else{
                    waitTime -= Time.deltaTime;
                }
            }
      }
      
    }
}
