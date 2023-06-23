using UnityEngine;

namespace CameraSystem
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private Camera zoomCamera;
        [SerializeField] private float zoomSpeed = 1f, minZoom = 2f, maxZoom = 8f;

        // for camera
        private void LateUpdate()
        {
            ZoomWithScroll();
        }

        private void ZoomWithScroll()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0f)
            {
                var currentZoom = zoomCamera.orthographicSize;
                var newZoom = currentZoom - scroll * zoomSpeed;

                // The new zoom value is restricted to the minimum and maximum range
                newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

                zoomCamera.orthographicSize = newZoom;
            }
        }
    }
}
