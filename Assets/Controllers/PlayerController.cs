using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 7;
    public float smoothTime = .1f;
    public float turnSpeed = 5;

    float smoothInputMagnitude;
    float smoothVelocity;

    float CurrentAngle;

    void Update()
    {
        float HorizontalControl = Input.GetAxisRaw("Horizontal");
        float VerticalControl = Input.GetAxisRaw("Vertical");
        
        Vector3 directionVector = new Vector3( HorizontalControl , 0 , VerticalControl).normalized;
        float inputMagnitude = directionVector.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp( smoothInputMagnitude , inputMagnitude , ref smoothVelocity , smoothTime );
        
        float angle = Mathf.Atan2( directionVector.x , directionVector.z ) * Mathf.Rad2Deg;
        CurrentAngle = Mathf.LerpAngle( CurrentAngle , angle , Time.deltaTime * turnSpeed * inputMagnitude );
        transform.eulerAngles = Vector3.up * CurrentAngle;
        
        transform.Translate( transform.forward * speed * Time.deltaTime * smoothInputMagnitude , Space.World );
    }
}
