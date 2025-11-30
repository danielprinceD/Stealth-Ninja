using System.Collections;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    public int speed = 5;
    public float delayTime = 1;
    public Transform pathObject;
    void Start()
    {
        Vector3[] waypoints = new Vector3[pathObject.childCount];
        for(int i = 0; i < pathObject.childCount; i++) {
            waypoints[i] = pathObject.GetChild(i).position;
        }
        StartCoroutine(ReachPoints(waypoints));
    }

    IEnumerator ReachPoints(Vector3[] reachPoints)
    {
        int targetIndex = 1;
        while (true)
        {
            Vector3 reachPoint = reachPoints[targetIndex] + Vector3.up * transform.localScale.y;
            float angle = 90-Mathf.Atan2(reachPoint.z , reachPoint.x) * Mathf.Rad2Deg;
            transform.position = Vector3.MoveTowards(transform.position , reachPoint  , speed * Time.deltaTime);
            transform.eulerAngles = transform.TransformDirection(Vector3.up * angle);
            if(transform.position == reachPoint)
            {
                targetIndex += 1;
                targetIndex = targetIndex % reachPoints.Length;
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
