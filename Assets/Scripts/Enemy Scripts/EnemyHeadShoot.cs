using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadShoot : MonoBehaviour
{
    public GameObject Parent;
    public Animator HeadShootAnim;


    private void Start()
    {
        if (transform.parent.localScale.x > 0) 
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.PlayheadShootSound();
            //   Debug.LogError("Bullet");
            Invoke(nameof(HeadShootPlay),.05f);
            HeadShootPlayAnim();


        }
    }

    internal void HeadShootPlay()
    {
        // Start slow-motion effect
        int enemyCount = GameUI.instace.gameManager.EnemyAllEnmy.Count;
       // Debug.LogError(enemyCount);
        if (enemyCount <= 0)
            GameUI.instace.SlowEffects();
    }
    internal void HeadShootPlayAnim()
    {
        Parent.transform.SetParent(null);
        HeadShootAnim.SetTrigger("head");
    }

}
