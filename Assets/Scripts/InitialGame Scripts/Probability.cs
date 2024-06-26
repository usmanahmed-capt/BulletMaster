using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probability : MonoBehaviour
{
    public static Probability Instance;
    //public float[] probability;    //40% could be 0.4f

    public List<float> probability = new List<float>();
    public List<float> cumulative = new List<float>();

   // public float[] cumulative;

    private void Start()
    {
        MakeCumulative();
    }

    private void Awake()
    {
        MakeCumulative();
        Instance = this;

    }
    void MakeCumulative()    //this function creates the cumulative array
    {
        float current = 0;
        int itemCount = probability.Count;
     
        for (int i = 0; i < itemCount; i++)
        {
            current += probability[i];
            cumulative[i] = current;
        }

        if (current > 1.0f)
        {
            Debug.Log("Probabilities exceed 100%");
        }

    }

    public void EachimeAccumulate() 
    {
        MakeCumulative();
    }

    public int GetRandomFruit()
    {
      
        float rnd = Random.Range(0, 1.0f);
        int itemCount = cumulative.Count;

        for (int i = 0; i < itemCount; i++)
        {
            if (rnd <= cumulative[i])
            {
                return i;
            }
        }

        return 0;
    }
}
