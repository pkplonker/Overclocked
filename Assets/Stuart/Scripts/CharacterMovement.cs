using System;
using UnityEngine;

namespace Stuart
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private KeyCode forwardKey = KeyCode.W;
        [SerializeField] private KeyCode backKey = KeyCode.S;
        [SerializeField] private KeyCode leftKey = KeyCode.A;
        [SerializeField] private KeyCode rightKey = KeyCode.D;
        [SerializeField] private float movementSpeed = 3f;
        private void Update()
        {
            if (Input.GetKey(forwardKey))
                ApplyMovement(Vector3.forward);
            else if (Input.GetKey(backKey))
                ApplyMovement(-Vector3.forward);
            else if (Input.GetKey(leftKey))
                ApplyMovement(-Vector3.right);
            else if (Input.GetKey(rightKey))
                ApplyMovement(Vector3.right);
            
        }

        private void ApplyMovement(Vector3 dir)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position -= transform.forward * (movementSpeed * Time.deltaTime);

        }
    }
}