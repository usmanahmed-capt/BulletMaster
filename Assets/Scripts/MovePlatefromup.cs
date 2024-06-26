using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatefromup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float amplitude;
    private float storePos;
    public float Factore;
    public float DectFactor;
    void Start()
    {
        storePos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //float y = transform.position.y;
        //float x = Mathf.Sin(Time.time) * amplitude;
        //float z = transform.position.z;
        //transform.position = new Vector3(x + storePos, y, z);

        float y = Mathf.PingPong(Time.time * amplitude, 1) * Factore - DectFactor;
        transform.localPosition = new Vector3(transform.localPosition.x, y, 0);
    }
}
