using System.Collections;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    public int speed = 5;
    public float delayTime = 1;
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
        Vector3 angleVector = Vector3.up * angle;
        while (true)
        {
            Vector3.Angle(transform.eulerAngles , angleVector);
            print(transform.eulerAngles);
            if(transform.eulerAngles == angleVector)
            {
                yield return null;
            }
        }
    }

    IEnumerator ReachPoints(Vector3[] reachPoints)
    {
        int targetIndex = 1;
        Vector3 reachPoint = reachPoints[targetIndex] + Vector3.up * transform.localScale.y;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position , reachPoint  , speed * Time.deltaTime);
            
            if(transform.position == reachPoint)
            {
                targetIndex += 1;
                targetIndex = targetIndex % reachPoints.Length;
                reachPoint = reachPoints[targetIndex] + Vector3.up * transform.localScale.y;
                Vector3 distanceVector = reachPoint - gameObject.transform.position;
                float angle = 90-Mathf.Atan2(distanceVector.z , distanceVector.x) * Mathf.Rad2Deg;
                yield return StartCoroutine(TurnAngle(angle));
                yield return new WaitForSeconds(delayTime);
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
            Gizmos.color = Color.black;
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
