using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatefrom : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float amplitude;
    private float storePos;
    public float Factore;
    public float DectFactor;
    void Start()
    {
        storePos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //float y = transform.position.y;
        //float x = Mathf.Sin(Time.time) * amplitude;
        //float z = transform.position.z;
        //transform.position = new Vector3(x + storePos, y, z);

        float x = Mathf.PingPong(Time.time * amplitude, 1) * Factore - DectFactor;
        transform.localPosition = new Vector3(x, transform.localPosition.y, 0);
    }
}
