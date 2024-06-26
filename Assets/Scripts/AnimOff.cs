using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimOff : MonoBehaviour
{

    public ScrollRect scrollRectp;
    public Animator Anim;

    private void OnEnable()
    {
        Anim.enabled = true;
    }
    public void ActiveScrollRectAnimof() 
    {
        scrollRectp.enabled = true;
        Anim.enabled = false;
    }
}
