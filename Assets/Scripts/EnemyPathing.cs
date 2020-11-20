using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //referemce to the crrent wave asset
    [SerializeField] WaveConfig waveConfig;
    // Start is called before the first frame update

    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float enemyMoveSpeed = 2f;

    int wayPointIndex = 0;
    void Start()
    {
        wayPoints = waveConfig.GetWaypoints();
        // we are setting the current enemy to place in the position of the first waypoint
        transform.position = wayPoints[wayPointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        if(wayPointIndex < wayPoints.Count)
            //if(wayPointIndex <= wayPoints.Count - 1)
        {
            /*
             Since the enemy movement method wil be called every frame,the movements speed will be frame dependant. just like we did for the player, we are using Time.deltaTime to make the speed frame independendt
            */
            var movementThisFrame = enemyMoveSpeed * Time.deltaTime;
            var targetPosition = wayPoints[wayPointIndex].position;

            /*
             */

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
                wayPointIndex++;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
