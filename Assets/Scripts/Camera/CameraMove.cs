using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject player;
    public JoyStick1 rotateJoystick;
    public GameObject crosshair;
    public float pLerp = 0.2f;
    public float rLerp = 0.1f;
    private Vector2 turn;
    private Vector3 offest;
    private Vector3 crosshairOffset;
    private Quaternion rotation;

    public float xSensivity = 1.0f;
    public float ySensivity = 1.0f;

    void Awake()
    {
        offest = transform.position - player.transform.position;
        crosshairOffset = crosshair.transform.position - player.transform.position;
    }
	
	void Update ()
    {
        Move();
        Rotate();
        MoveCrosshair();
    }

    void Move()
    {
        Vector3 targetPosition = player.transform.position + rotation * offest;
        transform.position = Vector3.Lerp(transform.position, targetPosition, pLerp);
    }

    void Rotate() 
    {
        turn.x += rotateJoystick.Horizontal() * xSensivity;
        turn.y += rotateJoystick.Vertical() * ySensivity;

        turn.y = Mathf.Clamp(turn.y, -20, 13);

        rotation = Quaternion.Euler(-turn.y, turn.x, 0);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, rLerp);
    }

    void MoveCrosshair() 
    {
        Vector3 crosshairTarget = player.transform.position + rotation * crosshairOffset;
        crosshair.transform.position = Vector3.Lerp(crosshair.transform.position, crosshairTarget, rLerp);
    }
}
