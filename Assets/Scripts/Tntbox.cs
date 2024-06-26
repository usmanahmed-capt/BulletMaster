using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tntbox : MonoBehaviour
{
    public GameObject explosionPrefab;

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Bullet")
        {
            GameObject exp = Instantiate(explosionPrefab);
            exp.transform.position = transform.position;
        }
       
    }

  
  
}
