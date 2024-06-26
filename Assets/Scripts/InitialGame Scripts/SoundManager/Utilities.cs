using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public static class Utilities
{
    //Play sound effect function;
    public static void PlaySFX(AudioSource source, AudioClip clip, float volume, bool loop = false)
    {

      
        if (source)
        {
            source.loop = loop;
            source.volume = volume;
            source.clip = clip;
            source.Play();
      
        }
           

            
    }


    //Shuffle list function;
    public static void Shuffle<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count-50;
       
        
        while (n > 1)
        {
            //Debug.Log("out" + n);
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        //Debug.Log(list.Count);
        for (int i =0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = UnityEngine.Random.Range(1, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
 

    //Player prefs bool implementation;
    public static void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
    }
    public static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }
}
