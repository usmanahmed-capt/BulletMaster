using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffects : MonoBehaviour
{

    public Rigidbody2D[] ChildRigibody;
    public float power = 10f;
    void Start()
    {
        Explode();
    }
    void Explode()
    {
        Vector2 explodeDir = Random.insideUnitCircle.normalized; // Randomize explosion direction
        foreach (Rigidbody2D hit in ChildRigibody)
        {
                hit.AddForce(power * explodeDir, ForceMode2D.Impulse);
        }
    }

}
