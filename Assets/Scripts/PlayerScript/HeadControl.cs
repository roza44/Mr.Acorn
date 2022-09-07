using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadControl : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator animator;
    public bool isActive = false;
    public Transform lookAtObject = null;
    public float lookWeight = 2f;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(animator)
        {
            if(isActive)
            {
                if(lookAtObject != null)
                {
                    //if(animator.GetBool("IsRunning")) 
                    //{
                        animator.SetLookAtWeight(lookWeight);
                        animator.SetLookAtPosition(lookAtObject.position);
                    //}
                }
            }
            else
            {
                animator.SetLookAtWeight(0);
                Debug.Log("I am not looking!");
            }
        }   
    }
}
