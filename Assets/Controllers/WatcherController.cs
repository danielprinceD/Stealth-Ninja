using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    public Transform pathObject;
    void Start()
    {
        
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
