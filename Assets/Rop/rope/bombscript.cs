using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ropepuzzle
{
    public class bombscript : MonoBehaviour
    {
        public GameObject blasteffect;
        BoxCollider2D ownbox;
        void Start()
        {
            ownbox = GetComponent<BoxCollider2D>();
            blasteffect.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "can")
            {
                print("cans!!");
              //  Ropepuzzle.gamemanager.Instance.collected++;
                Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
                BoxCollider2D box = collision.GetComponent<BoxCollider2D>();
                box.isTrigger = false;
                MeshRenderer mesh = collision.transform.GetChild(0).GetComponent<MeshRenderer>();
                mesh.materials[1].color = Color.red;
                mesh.materials[2].color = Color.white;
                rb.AddForce(new Vector2(0.4f, 0.4f) * 10f, ForceMode2D.Impulse);
                blasteffect.SetActive(true);
                Destroy(transform.GetChild(0).gameObject);
                Destroy(ownbox);
            }



        }



    }

}