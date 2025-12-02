using System;
using System.Collections;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    public int speed = 5;
    public int WatcherRotationSpeed = 10;
    public float turnDelay = 0.5f;
    public Transform pathObject;
    void Start()
    {
        Vector3[] waypoints = new Vector3[pathObject.childCount];
        for(int i = 0; i < pathObject.childCount; i++) {
            waypoints[i] = pathObject.GetChild(i).position;
        }
        StartCoroutine(ReachPoints(waypoints));
    }
    IEnumerator TurnAngle(float angle)
    {
        
        while (Mathf.DeltaAngle(transform.eulerAngles.y, angle) != 0)
        {
            float changeInAngle = Mathf.MoveTowardsAngle( transform.eulerAngles.y , angle , WatcherRotationSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * changeInAngle;
            yield return null;
        }
        yield return null;
    }
    IEnumerator ReachPoints(Vector3[] reachPoints)
    {
        int targetIndex = 1;
        transform.LookAt(reachPoints[targetIndex] + Vector3.up * transform.localScale.y);
        while (true)
        {
            Vector3 reachPoint = reachPoints[targetIndex] + Vector3.up * transform.localScale.y;
            transform.position = Vector3.MoveTowards(transform.position , reachPoint  , speed * Time.deltaTime);
            if(transform.position == reachPoint)
            {
                targetIndex += 1;
                targetIndex = targetIndex % reachPoints.Length;
                reachPoint = reachPoints[targetIndex];
                Vector3 distanceVector = (reachPoint - transform.position).normalized;
                float angle = 90-Mathf.Atan2(distanceVector.z , distanceVector.x) * Mathf.Rad2Deg;
                yield return StartCoroutine(TurnAngle(angle));
            }
            yield return null;
        }
    }
    

    void OnDrawGizmos()
    {
        Vector3 startPosition = pathObject.GetChild(0).position;
        Vector3 prevPosition = startPosition;

        foreach(Transform waypoint in pathObject)
        {
            Gizmos.color = Color.darkBlue;
            Gizmos.DrawSphere(waypoint.position , .1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(prevPosition , waypoint.position);
            prevPosition = waypoint.position;
        }
        Gizmos.DrawLine(prevPosition , startPosition);
    }

    void Update()
    {

    }
}
