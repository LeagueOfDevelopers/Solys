using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace Lean.Touch
{
    // This script allows you to zoom a camera in and out based on the pinch gesture
    // This supports both perspective and orthographic cameras
    [ExecuteInEditMode]
    public class CameraZoomAndMove : MonoBehaviour
    {

        private int lastTool;
        [Tooltip("The camera that will be zoomed")]
        public Camera Camera;

        [Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreGuiFingers = true;

        [Tooltip("Allows you to force rotation with a specific amount of fingers (0 = any)")]
        public int RequiredFingerCount;

        [Tooltip("If you want the mouse wheel to simulate pinching then set the strength of it here")]
        [Range(-1.0f, 1.0f)]
        public float WheelSensitivity;

        private float Sensitivity = 1.5f;

        [Tooltip("The current FOV/Size")]
        protected float Zoom = 5.0f;

        [Tooltip("The minimum FOV/Size we want to zoom to")]
        private float ZoomMin = 2.5f;

        [Tooltip("The maximum FOV/Size we want to zoom to")]
        private float ZoomMax = 15.0f;

        protected bool IsHandToolActive = false;

        protected virtual void LateUpdate()
        {
            // Make sure the camera exists
            if (LeanTouch.GetCamera(ref Camera, gameObject) == true)
            {
                // Get the fingers we want to use
                var fingers = LeanTouch.GetFingers(IgnoreGuiFingers);
                if (fingers.Count > 1)
                    ChangeToolOnHand(true);

                // Get the pinch ratio of these fingers
                var pinchRatio = LeanGesture.GetPinchRatio(fingers, WheelSensitivity);

                // Modify the zoom value
                Zoom *= pinchRatio;

                
                Zoom = Mathf.Clamp(Zoom, ZoomMin, ZoomMax);

                // Get the world delta of all the fingers
                if (fingers.Count > 1)
                {
                    var worldDelta = LeanGesture.GetWorldDelta(fingers, 0, Camera);

                    // Pan the camera based on the world delta
                    var newPos = transform.position - worldDelta * Sensitivity;

                    newPos.x = Mathf.Clamp(newPos.x, -Camera.main.pixelWidth/100, Camera.main.pixelWidth/100);
                    newPos.y = Mathf.Clamp(newPos.y, -Camera.main.pixelHeight/100, Camera.main.pixelHeight/100);
                    transform.position = newPos;
                }
                // Set the new zoom
                SetZoom(Zoom);
            }
        }


        public void ChangeToolOnHand(bool hand)
        {
            var lw = GameObject.Find("LineWriter").GetComponent<LineWriter>();
            lw.SetCooldownOnChangeTool();
            IsHandToolActive = hand;
            if (hand)
            {
                lastTool = (lw.tool == 2) ? lastTool : lw.tool;
                lw.ChangeTool(2);
            }
            else
            {
                if(lw.tool == 2)
                    lw.ChangeTool(lastTool);
            }

            
        }

        protected void SetZoom(float current)
        {
            var camera = GetComponent<Camera>();

            if (camera.orthographic == true)
            {
                camera.orthographicSize = current;
            }
            else
            {
                camera.fieldOfView = current;
            }
        }
    }
}