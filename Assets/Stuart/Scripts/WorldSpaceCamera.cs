using UnityEngine;

namespace Stuart
{
	public class WorldSpaceCamera : MonoBehaviour
	{
		private Camera cam;
		private void Awake() => cam = Camera.main;

		private void LateUpdate() => transform.LookAt(transform.position - cam.transform.position,
			cam.transform.rotation * Vector3.up);
	}
}