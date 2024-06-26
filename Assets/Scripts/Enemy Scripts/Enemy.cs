using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AudioClip death;
    public GameObject bloodParticlePrefab; // Assign your blood particle prefab in the inspector
    public Rigidbody2D[] Rb;
    private bool canDiy;
    public GameObject happyFace;
    public GameObject SadFace;
    public GameObject NormalFace;
    public Animator Anim;
    public GameObject Head;
    void Death()
    {
        if (!canDiy)
        {
            gameObject.tag = "Untagged";
            Anim.enabled = false;
            SadFaceFun();
            GameUI.instace.gameManager.CheckEnemyCount();
            SoundManager.Instance.PlayEnemyDeath();
            GameUI.instace.gameManager.EnemyAll.Remove(this);
            foreach (Rigidbody2D rb in Rb)
            {
                rb.gravityScale = 1;
            }
            canDiy = true;
        }
    }

    private void Start()
    {
        Anim.speed = Random.Range(.9f, 1f);
        NormalFaceFun();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
      

        Vector2 direction = transform.position - target.transform.position;
       
        if (target.tag == "Bullet")
        {
            Head.SetActive(false);
            Vector2 hitPoint = target.ClosestPoint(transform.position);
            ShowBlood(hitPoint);
            GameUI.instace.gameManager.EnemyAllEnmy.Remove(this);
            if (Rb[1].gravityScale < 1)
                Death();

            Rb[0].AddForce(new Vector2((direction.x > 0 ? 1 : -1) * 10, direction.y > 0 ? .3f : -.1f),
                ForceMode2D.Impulse);
        }

        if(target.tag == "Plank" || target.tag == "BoxPlank")
        {
           
            if (target.GetComponent<Rigidbody2D>().velocity.magnitude > 1.5f)
            {
                Head.SetActive(false);
                GameUI.instace.gameManager.EnemyAllEnmy.Remove(this);
                Death();
            }
        }
        if (target.tag == "Obstacle")
        {
           
            if (Rb[1].gravityScale < 1)
            {
                Head.SetActive(false);
                GameUI.instace.gameManager.EnemyAllEnmy.Remove(this);
                Death();
            }
        }

        if (target.tag == "Ground" )
        {
            //  Head.SetActive(false);
            if (Rb[0].velocity.magnitude > 2)
            {
                GameUI.instace.gameManager.EnemyAllEnmy.Remove(this);
                Death();
            }
        }


        if (target.tag == "UpWall")
        {
            Head.SetActive(false);
            GameUI.instace.gameManager.EnemyAllEnmy.Remove(this);
            if (Rb[0].gravityScale < 3)
                Death();

           // Debug.LogError("UpWall");
            Rb[0].AddForce(new Vector2((direction.x > 0 ? 1 : 1) * 10, direction.y > 0 ? 1f : 1f),
                ForceMode2D.Impulse);
        }
    }

    void ShowBlood(Vector2 position)
    {
        // Instantiate the blood particle effect at the specified position
        GameObject boodPart=    Instantiate(bloodParticlePrefab, position, bloodParticlePrefab.transform.rotation);
        boodPart.transform.SetParent(transform);
    }

    internal void HappyFaceFun()
    {

        happyFace.SetActive(true);
        SadFace.SetActive(false);
        NormalFace.SetActive(false);
    }

    internal void SadFaceFun()
    {

        happyFace.SetActive(false);
        SadFace.SetActive(true);
        NormalFace.SetActive(false);
    }

    internal void NormalFaceFun()
    {
        happyFace.SetActive(false);
        SadFace.SetActive(false);
        NormalFace.SetActive(true);
    }
}
