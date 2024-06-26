using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 100;/*, bulletSpeed = 100*/
    public int ammo = 4;

    public Transform handPos;
    public Transform firePos1, firePos2;

    public LineRenderer lineRenderer;

    public GameObject bullet;
    public GameObject Bazula;
    public GameObject Gernate;

    public GameObject crosshair;

    public AudioClip gunShot;
    public Animator PlayerAnim;
    public ParticleSystem playerParticle;
    public Rigidbody2D ArmRb;
    public GameObject happyFace;
    public GameObject SadFace;
    public GameObject NormalFace;
    public HingeJoint2D hingeJoint2D;
    public GameObject Obj;
    public Animator HeadShootAnim;

    void Awake()
    {
        crosshair.SetActive(false);
        lineRenderer.enabled = false;
    }

    private void Start()
    {
        GameController.Instance.CanPlayOn = true;
        PlayerAnim.speed = Random.Range(.9f,1f);
    }

    public void OnAnimClose()
    {
        PlayerAnim.enabled = false;
        GameController.Instance.CanPlayOn = true;
    }
    public void PlayParticleHitGroundParticle() 
    {
        playerParticle.Play();
    }

    void Update()
    {
            if (GameController.Instance.CanPlayOn)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject thisButton = EventSystem.current.currentSelectedGameObject;     //Get the button on click
                    if (thisButton != null || IsMouseOverUI())
                        return;//Is click on button

                    ArmRb.simulated = false;
                Obj.SetActive(false);
                CancelInvoke(nameof(RibOnArmOff));
                GameUI.instace.DisableToturial();
            }
                if (Input.GetMouseButton(0))
                {
                    AimToRotate();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    GameObject thisButton = EventSystem.current.currentSelectedGameObject;     //Get the button on click
                    if (thisButton != null|| IsMouseOverUI())
                        return;//Is click on button

                if (ammo > 0)
                        Shoot();
                    else
                    {
                     SoundManager.Instance.PlaygunEmptySound();
                        lineRenderer.enabled = false;
                        crosshair.SetActive(false);
                    }
                }
            }
            if (!GameController.Instance.CanPlayOn)
            {
                lineRenderer.enabled = false;
            }

       
    }

    void AimToRotate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPos.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        handPos.rotation = rotation;
     //   handPos.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePos1.position);
        lineRenderer.SetPosition(1, firePos2.position);

        crosshair.SetActive(true);
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10);
    }

    void Shoot()
    {
        hingeJoint2D.useMotor = false;
        crosshair.SetActive(false);
        lineRenderer.enabled = false;

        GameObject b = Instantiate(bullet, firePos1.position, Quaternion.identity);

        if (transform.localScale.x > 0)
            b.GetComponent<Rigidbody2D>().AddForce(firePos1.right * GameController.Instance.bulletSpeed, ForceMode2D.Impulse);
        else
            b.GetComponent<Rigidbody2D>().AddForce(-firePos1.right * GameController.Instance.bulletSpeed, ForceMode2D.Impulse);

        ammo--;
        GameUI.instace.gameManager.CheckBullets();
        GameUI.instace.niceVibrations.TriggerVibrate();
        SoundManager.Instance.PlaygunShotSound();
        Invoke(nameof(RibOnArm),.2f);
        Destroy(b, 2f);
    }

    void RibOnArm() 
    {
        ArmRb.simulated = true;
        Invoke(nameof(RibOnArmOff), 2f);
    }
    void RibOnArmOff()
    {
        Obj.SetActive(true);
    }
    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
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

    internal void HeadShootPlay() 
    {
        HeadShootAnim.SetTrigger("head");
    }
}
