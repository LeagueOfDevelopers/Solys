using UnityEngine;
using System.Collections;

namespace Lean.Touch
{
	// This modifies LeanCameraZoom to be smooth
	public class CameraLogic : CameraZoomAndMove
	{
        public bool isActive;

		[Tooltip("How quickly the zoom reaches the target value")]
		public float Dampening = 10.0f;

		private float currentZoom;

		protected virtual void OnEnable()
		{
			currentZoom = Zoom;
            LeanTouch.OnFingerUp += OnFingerUp;
		}

        protected virtual void OnDisable()
        {
            LeanTouch.OnFingerUp -= OnFingerUp;
        }

        public void OnFingerUp(LeanFinger finger)
        {
            if (IsHandToolActive) StartCoroutine(CheckZoomStop());       
            Debug.Log("FingerUp " + LeanTouch.Fingers.Count + "IsHandToolActive "+ IsHandToolActive);
        }

        IEnumerator CheckZoomStop()
        {
            yield return new WaitForSeconds(Time.deltaTime*4);
            if (IsHandToolActive && LeanTouch.Fingers.Count == 0)
                ChangeToolOnHand(false);
        }

		protected override void LateUpdate()
		{
			// Make sure the camera exists
			if (LeanTouch.GetCamera(ref Camera, gameObject) == true)
			{
				// Use the LateUpdate code from LeanCameraZoom
				base.LateUpdate();

				// Get t value
				var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);

				// Lerp the current value to the target one
				currentZoom = Mathf.Lerp(currentZoom, Zoom, factor);

				// Set the new zoom
				SetZoom(currentZoom);
			}
		}
	}
}