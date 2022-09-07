using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerMovement : MonoBehaviour {

    Rigidbody rig;
    Animator anim;

    public JoyStick1 moveJoystick;
    public JoyStick1 rotateJoystick;

    public float movementSpeed = 10f;
    public float rotationSpeed = 10f;
    public float xRotationSensivity = 1.0f;
    public float yRotationSensivity = 1.0f;
    
    private Quaternion rotation;
    private Quaternion joystickRotation;
    private float xRotation;
    private float yRotation;
    private Vector3 movementDirection;

    void Start ()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	void Update()
    {
        float xMovement = moveJoystick.Horizontal();
        float zMovement = moveJoystick.Vertical();

        xRotation +=  rotateJoystick.Horizontal() * xRotationSensivity;
        yRotation += rotateJoystick.Vertical() * yRotationSensivity;

        Move(xMovement,zMovement);
        Rotate(xMovement,zMovement);
        Animate(xMovement,zMovement);
    }

    void Move(float xMovement, float zMovement)
    {
        float pLerp = 0.8f;
        float moveVolumen = 10.0f;

        if(xMovement != 0 || zMovement != 0)
        {
            movementDirection.Set(xMovement, 0, zMovement);
            movementDirection.Normalize();

            Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;

            movement = joystickRotation * movement;

            Vector3 targetPosition = transform.position + movement;

            Vector3 step = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, targetPosition, pLerp), moveVolumen);

            rig.MovePosition(step);
        }
    }

    void Rotate(float xMovement, float zMovement)
    {
        Quaternion moveRotation = new Quaternion();

        if(xMovement != 0 || zMovement != 0)
        {
            Vector3 restrict = movementDirection;
            if(restrict.z < 0)
            {
                restrict.x = restrict.x * -1;
                restrict.x = Mathf.Clamp(restrict.x, -0.12f, 0.12f);
            }
            restrict.z = Mathf.Clamp(restrict.x, 0.7f, 0.7f);
            moveRotation = Quaternion.LookRotation(restrict, Vector3.up);
            joystickRotation = Quaternion.Euler(0, xRotation, 0);
            rotation = joystickRotation * moveRotation;
        }
        else 
        {
            joystickRotation = Quaternion.Euler(0, xRotation, 0);
            rotation = joystickRotation;
        }
       
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }

    void Animate(float xMovement, float zMovement)
    {
        bool walk;

        if (xMovement != 0.0 || zMovement != 0.0) 
        {
            walk = true;
            if(zMovement < 0)
            {
                anim.SetFloat("RunSpeed", -1);
            }
            else
            {
                anim.SetFloat("RunSpeed", 1);
            }
        }
        else walk = false;

        anim.SetBool("IsRunning", walk);
    }
}
