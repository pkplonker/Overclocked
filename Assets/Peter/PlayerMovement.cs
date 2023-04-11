using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    private Rigidbody rb;
    private Animator an;
    private float lastmoveTime;
    private bool itemToggle = false;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        an = this.GetComponent<Animator>();
    }

    void Update()
    {
        an.SetFloat("lastInput", Time.time - lastmoveTime);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            an.SetBool("moving", false); 
        }
        else
        {
            an.SetBool("moving", true);
            lastmoveTime = Time.time;
        }
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, 0, Input.GetAxis("Vertical") * movementSpeed);
        if(rb.velocity != new Vector3(0, 0, 0))
        {
            rb.rotation = Quaternion.LookRotation(rb.velocity, new Vector3(0, 1, 0));
        }

        if (Input.GetKey("space"))
        {
            if (itemToggle == false)
            {
                an.SetTrigger("pickup");
            }
            else
            {
                an.SetTrigger("putDown");
            }
            itemToggle = !itemToggle;
        }

        if (Input.GetKey("e"))
        {
            an.SetBool("interacting", true);
        }
        else
        {
            an.SetBool("interacting", false);
        }
    }
}
