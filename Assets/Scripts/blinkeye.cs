using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkeye : MonoBehaviour
{
    public GameObject eye;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(openeyes());
    }

    IEnumerator openeyes()
    {
        eye.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(closeeyes());
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

    }
    IEnumerator closeeyes()
    {
        eye.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(openeyes());
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    public void stopco()
    {
        StopAllCoroutines();
    }
    public void startco()
    {
        StartCoroutine(openeyes());
    }
}
