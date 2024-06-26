using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatefromDrop : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D box;
    public SpriteRenderer sb;
    public GameObject[] halfObject;
    private bool ColliderOnlyOnce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("BoxPlank")) 
        {
            if (!ColliderOnlyOnce)
            {
                SoundManager.Instance.PlayGlassHitSound();
                sb.enabled = false;
                rb.isKinematic = false;
                for (int i = 0; i < halfObject.Length; i++)
                {
                    halfObject[i].SetActive(true);
                }
                ColliderOnlyOnce = true;
            }
        }
    }
}
