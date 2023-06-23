using UnityEngine;

namespace CameraSystem
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform followShunrald;
        [SerializeField] private Vector3 camOffset;
        [SerializeField] private float smoothSpeed, camOffsetZ;

        private Vector3 desiredPosition, smoothedPosition;

        // for camera
        private void LateUpdate()
        {
            FollowShunrald();
        }

        private void FollowShunrald()
        {
            desiredPosition = followShunrald.position + camOffset;
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
