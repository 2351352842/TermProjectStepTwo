using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Invector.vCharacterController
{
    public class PlayerAnimaController : MonoBehaviour
    {
        public static PlayerAnimaController instance;
        private void Awake()
        {
            instance=this;
        }
        Animator anim;
        Rigidbody rb;
        Text placeName;
        string LastName = "";
        IEnumerator playstop;
        // Start is called before the first frame update
        void Start()
        {
            playstop = PlayerStop();
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            placeName = GameObject.Find("PlaceName").GetComponent<Text>();
            placeName.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("IsJump", true);
            }
            else
            {
                anim.SetBool("IsJump", false);
            }

            UpdateAnim();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "door")
            {
                anim.SetBool("IsOpen", true);
                StartCoroutine(playstop);
                if (LastName != other.gameObject.name)
                {
                    ShowPlaceName(other);
                }
            }
            else if (other.tag == "PlayGround")
            {
                ShowPlaceName(other);
            }
        }
        public void CloseIE()
        {
            StopCoroutine(playstop);
        }
        public void AnimationPlayOver()
        {
            anim.SetBool("IsOpen", false);
        }
        void UpdateAnim()
        {
            anim.SetFloat("Speed", rb.velocity.magnitude);
        }
        public void ShowPlaceName(Collider other)
        {
            placeName.text = other.gameObject.name;
            placeName.gameObject.SetActive(true);
            LastName = other.gameObject.name;
        }
        IEnumerator PlayerStop()
        {
            while (true)
            {
                vThirdPersonMotor.instance.moveSpeed= 0;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}

