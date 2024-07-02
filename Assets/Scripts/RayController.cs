using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Invector.vCharacterController
{
    public class RayController : MonoBehaviour
    {
        public LayerMask mask;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,mask))
            {
                if (hit.collider.gameObject.tag == "door1")
                {
                    PlayerAnimaController.instance.ShowPlaceName(hit.collider);
                }
            }
        }
    }
}