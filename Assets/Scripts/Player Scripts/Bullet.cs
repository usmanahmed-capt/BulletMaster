using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public AudioClip boxHit, plankHit, groundHit, explodeHit;

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Box")
        {
            SoundManager.Instance.PlayplayBoxhitSound();
            Destroy(target.gameObject);
        }

        if (target.gameObject.tag == "Ground")
        {
            SoundManager.Instance.PlaygroundHitSound();
        }

        if (target.gameObject.tag == "Plank")
        {
            SoundManager.Instance.PlayplankHitSound();
        }
        if (target.gameObject.tag == "BoxPlank")
        {
            SoundManager.Instance.PlayMetalplankHitSound();
        }
        if (target.gameObject.tag == "Tnt")
        {
            SoundManager.Instance.PlayexplodeHitSound();
        }
        if (target.gameObject.tag == "head")
        {
          //  Debug.LogError("HeadShoot");
            SoundManager.Instance.PlayheadShootSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "head")
        //{
        //    GamePlay.Instance.player.HeadShootPlay();
        //    SoundManager.Instance.PlayheadShootSound();
        //}
    }
}
