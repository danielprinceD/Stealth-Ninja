using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 7;
    void Update()
    {
        float HorizontalControl = Input.GetAxisRaw("Horizontal");
        float VerticalControl = Input.GetAxisRaw("Vertical");
        Vector3 directionVector = new Vector3( HorizontalControl , 0 , VerticalControl).normalized;
        float angle = Mathf.Atan2( directionVector.x , directionVector.z ) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;
        transform.Translate( transform.forward * speed * Time.deltaTime * directionVector.magnitude , Space.World );
    }
}
