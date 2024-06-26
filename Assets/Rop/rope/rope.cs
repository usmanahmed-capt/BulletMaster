using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ropepuzzle
{
    public class rope : MonoBehaviour
    {

        public Rigidbody2D hook;
        public GameObject linkprefab;
        public weightscript weight;
        LineRenderer lr;
        public int links = 7;
        public bool line = false;
        public bool joinrope = false, anim = false;
        public Animator circle;


        private void Awake()
        {

            lr = transform.GetChild(0).GetComponent<LineRenderer>();
            if (!joinrope)
                generaterope();


        }
        private void Start()
        {

            if (joinrope)
            {
                circle.SetBool("zoom", true);
                generaterope();

            }

        }

        private void Update()
        {


            drawline();







        }

        void drawline()
        {

            lr.SetVertexCount(links);
            int i;
            if (line == false)
            {
                for (i = 0; i < transform.childCount - 1; i++)
                {
                    if (transform.GetChild(i).gameObject != null)
                    {
                        lr.SetPosition(i, transform.GetChild(i).transform.position);
                    }


                }

            }
            else
            {
                
                for (i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject != null)
                    {
                        lr.SetPosition(i, transform.GetChild(i).transform.position);
                    }
                }
            }

        }
        void generaterope()
        {
            Rigidbody2D previousrb = hook;
            for (int i = 0; i < links; i++)
            {
                GameObject link = Instantiate(linkprefab, transform);
                HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
                joint.connectedBody = previousrb;
                if (i < links - 1)
                {
                    previousrb = link.GetComponent<Rigidbody2D>();

                }
                else
                {

                    weight.connectendrope(link.GetComponent<Rigidbody2D>());
                }


            }

        }

    }
}
