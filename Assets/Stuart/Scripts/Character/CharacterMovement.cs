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
		public event Action<Vector3> OnMovementChanged;
		public bool canMove;
		private Animator an;
		private float lastmoveTime;

		private void Start()
		{
			an = GetComponentInChildren<Animator>();
		}

		private void Update()
		{
			an.SetFloat("lastInput", Time.time - lastmoveTime);
			if (!canMove) return;
			if (Input.GetKey(forwardKey))
				ApplyMovement(Vector3.forward);
			else if (Input.GetKey(backKey))
				ApplyMovement(-Vector3.forward);
			else if (Input.GetKey(leftKey))
				ApplyMovement(-Vector3.right);
			else if (Input.GetKey(rightKey))
				ApplyMovement(Vector3.right);
			else
				an.SetBool("moving", false);
		}

		private void ApplyMovement(Vector3 dir)
		{
			var t = transform;
			t.rotation = Quaternion.LookRotation(dir);
			t.position += t.forward * (movementSpeed * Time.deltaTime);
			OnMovementChanged?.Invoke(dir);
			an.SetBool("moving", true);
			lastmoveTime = Time.time;
		}
	}
}