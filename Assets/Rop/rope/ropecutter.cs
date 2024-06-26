using UnityEngine;
namespace Ropepuzzle
{
    public class ropecutter : MonoBehaviour
    {
        public Transform ropeparent;
        public Transform ball;
        public bool lockball;


        void Update()
        {

            Destroyjoint();
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


                if (hit.collider != null)
                {

                    if (hit.collider.tag == "link")
                    {

                        lockball = false;
                        int num;
                        ropeparent = hit.transform.parent;
                        rope ropescript = ropeparent.GetComponent<rope>();
                        ropescript.line = true;
                        //  Destroy(hit.transform.parent.gameObject);
                        num = hit.transform.GetSiblingIndex();
                        Destroy(hit.collider.gameObject);
                        LineRenderer lr = ropeparent.GetChild(0).GetComponent<LineRenderer>();


                        for (int i = num; i < ropeparent.childCount; i++)
                        {


                            Destroy(ropeparent.GetChild(i).gameObject);



                        }
                        ropeparent.GetComponent<rope>().links = num;
                    }

                }
            }
        }


        void Destroyjoint()
        {
            if (ball != null)
            {
                HingeJoint2D[] hj2d = ball.GetComponents<HingeJoint2D>();
                for (int i = 0; i < hj2d.Length; i++)
                {
                    if (hj2d[i].connectedBody == null)
                        Destroy(hj2d[i]);


                }


            }

        }
    }
}
