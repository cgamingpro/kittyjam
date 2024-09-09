using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolingAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    void Start()
    {
        targetPoint = 0;
    }

 
    void Update()
    {
        if(transform.position == patrolPoints[targetPoint].position)
        {
            IncreaseTargetInt();
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    void IncreaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length) {

            targetPoint = 0;
        
        
        }


    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            if (i >= patrolPoints.Length) 
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
                
                i = 0;



            }
            else {
                
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i+1].position);
            
            
            }

            Gizmos.DrawWireSphere(patrolPoints[i+1].position, 0.4f);
            
        }
        
    }
}
