using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolingAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPoint = 0;
    }

 
    void Update()
    {
        float distance = Vector3.Distance(transform.position, patrolPoints[targetPoint].position);

        

        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseTargetInt();
        }
        if(distance < 0.5f)
        {
            IncreaseTargetInt();
        }
        //transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
        agent.destination = patrolPoints[targetPoint].position;
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
            int j = GetNextIndex(i);
            Gizmos.DrawWireSphere(GetWaypointPos(i), 0.4f);
            Gizmos.DrawLine(GetWaypointPos(i), GetWaypointPos(j));
        }
        
         int GetNextIndex(int i)
         {
            if (i+1 == patrolPoints.Length)
            {

                return 0;

            } return i + 1;
         }   

        Vector3 GetWaypointPos(int i)
        {

            return patrolPoints[i].position;

        }

            
            


        
    }
}
//  if (i == patrolPoints.Length) 
//  {
//      Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
//      
//      i = 0;
//
//
//
//  }
//  else {
//      
//      Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i+1].position);
//  
//  
//  }
//
//  Gizmos.DrawWireSphere(patrolPoints[i+1].position, 0.4f);
//  