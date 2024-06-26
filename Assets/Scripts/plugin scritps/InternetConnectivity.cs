using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetConnectivity : MonoBehaviour {

    public static InternetConnectivity _instance;
    public float timeOut=1f;
    public int ping=130;
    float tempTime;
    bool busy = false;
    public bool CanCheckInterConnection;
    
    public void Awake()
    {
           
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void CheckInternetConnection(Action<bool,Ping> action)
    {
        if (!busy)
        {
            busy = true;
            CheckPing("8.8.8.8", action);
        }
      
    }

    void CheckPing(string ip, Action<bool, Ping> action)
    {
        StartCoroutine(StartPing(ip,action));
    }

    IEnumerator StartPing(string ip, Action<bool, Ping> action)
    {
        WaitForSeconds f = new WaitForSeconds(0.05f);
        Ping p = new Ping(ip);
        while (p.isDone == false && tempTime<timeOut)
        {
            tempTime += Time.deltaTime;
            yield return f;
        }
        PingFinished(p,action);
    }


    void PingFinished(Ping p, Action<bool, Ping> action)
    {
        busy = false;
        Debug.Log("Ping is : "+p.time);


        //***Original Ping Code ***//

        if (tempTime < timeOut)
        {
            tempTime = 0;
            if (p.time < ping)
            {
                action(true, p);
            }
            else
            {
                action(false, p);
            }
        }
        else
        {
            tempTime = 0;
            action(false, p);
        }

    }
}
