using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopArm : MonoBehaviour
{

    public HingeJoint2D hingeJoint2D;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.transform.name == "LeftArm")
        {
           
            hingeJoint2D.useMotor = true;
            gameObject.SetActive(false);
        }
    }
}
