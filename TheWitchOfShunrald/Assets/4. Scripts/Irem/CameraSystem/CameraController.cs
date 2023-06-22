using UnityEngine;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform followShunrald;
        [SerializeField] private Vector3 camOffset;
        [SerializeField] private float smoothSpeed, camOffsetZ;

        private Vector3 desiredPosition, smoothedPosition, targetPos;

        private void Awake()
        {
            GetRequiredComponent();
        }

        // for camera
        private void LateUpdate()
        {
            desiredPosition = followShunrald.position + camOffset;
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

        private void GetRequiredComponent()
        {

        }
    }
}