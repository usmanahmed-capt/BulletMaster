using UnityEngine;
namespace Ropepuzzle
{
    public class weightscript : MonoBehaviour
    {
        public float distancefromchain;
        float thrust = 7f;
        public GameObject particleeffect;
        ropecutter knife;
        Rigidbody2D rbown;
        public bool boxactive;
        BoxCollider2D box;
        public float fanspeed;
        public GameObject faneffect;
        public int direction;
        Vector2 cross, right, left, fandirection, crossleft;
        public bool destroyball = false;
        public bool stoprot = false;
        private void Start()
        {
            cross = new Vector2(0.4f, 0.4f);
            crossleft = new Vector2(-0.7f, -0.7f);
            right = Vector2.right;
            left = Vector2.left;
            if (direction == 0)
            {
                fandirection = cross;
            }
            else if (direction == 1)
            {
                fandirection = right;

            }
            else if (direction == 2)
            {
                fandirection = left;
            }
            else if (direction == 3)
            {
                fandirection = crossleft;
            }
            if (boxactive)
            {
                box = GetComponent<BoxCollider2D>();
            }

            rbown = GetComponent<Rigidbody2D>();
            //knife = GameObject.Find("knife").GetComponent<ropecutter>();
            //if (knife.lockball)
            //{

            //    rbown.constraints = RigidbodyConstraints2D.FreezePositionY;

            //}
        }
        private void Update()
        {

            //if (!knife.lockball)
            //{
            //    if (boxactive)
            //    {
            //        if (box.isTrigger)
            //        {
            //            box.isTrigger = false;
            //        }
            //    }

            //    if (rbown.isKinematic)
            //    {
            //        rbown.isKinematic = false;
            //    }
            //    rbown.constraints = RigidbodyConstraints2D.None;


            //}


        }
        public void connectendrope(Rigidbody2D endrb)
        {
            HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedBody = endrb;
            joint.anchor = Vector2.zero;
            joint.connectedAnchor = new Vector3(0f, -distancefromchain);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "can")
            {
              //  Ropepuzzle.gamemanager.Instance.collected++;
                GameObject spawn = Instantiate(particleeffect, collision.transform.position, Quaternion.identity);
                Destroy(spawn, 3f);
                Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
                BoxCollider2D box = collision.GetComponent<BoxCollider2D>();
                box.isTrigger = false;
                MeshRenderer mesh = collision.transform.GetChild(0).GetComponent<MeshRenderer>();
                mesh.materials[1].color = Color.black;
                mesh.materials[2].color = Color.white;
                rb.AddForce(new Vector2(0.4f, 0.4f) * thrust, ForceMode2D.Impulse);
            }

            if (collision.gameObject.tag == "check")
            {

                Destroy(gameObject);
             //   Ropepuzzle.gamemanager.Instance.checkwin = true;

            }
            if (collision.gameObject.tag == "joinrope")
            {
                rope joinrope = collision.GetComponent<rope>();
                CircleCollider2D circle = collision.GetComponent<CircleCollider2D>();

                if (!joinrope.enabled)
                {
                    circle.enabled = false;
                    joinrope.enabled = true;

                }


            }
            if (collision.gameObject.tag == "bomb")
            {
                rbown.AddForce(new Vector2(0.4f, 0.4f) * thrust, ForceMode2D.Impulse);

            }


        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject)
            {
                if (stoprot)
                {
                    rbown.isKinematic = false;
                    rbown.constraints = RigidbodyConstraints2D.None;
                }

            }
            if (collision.gameObject.tag == "bomb")
            {
                BoxCollider2D boxtrigger = collision.transform.GetComponent<BoxCollider2D>();
                boxtrigger.enabled = false;
                BoxCollider2D BoxColl = collision.transform.GetChild(0).GetComponent<BoxCollider2D>();
                BoxColl.enabled = true;
                rbown.isKinematic = true;
                if (destroyball)
                {
                    Destroy(gameObject);
                }

            }
            if (collision.gameObject.tag == "fan")
            {
                rbown.mass = 1;
                print("fan!!");
                rbown.AddForce(fandirection * fanspeed, ForceMode2D.Impulse);
                GameObject go = Instantiate(faneffect, collision.transform.position, collision.transform.rotation);
                Destroy(go, 2f);
                rbown.mass = 40;
            }
        }

    }
}



