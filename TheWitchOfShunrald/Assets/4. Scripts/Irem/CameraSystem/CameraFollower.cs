using UnityEngine;

namespace CameraSystem
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform followShunrald;
        [SerializeField] private float smoothSpeed, camOffsetZ;

        private Vector3 smoothedPosition;

        // for camera
        private void LateUpdate()
        {
            FollowShunrald();
        }

        private void FollowShunrald()
        {
            // just follow the target
            smoothedPosition = Vector3.Lerp(transform.parent.position, followShunrald.position, smoothSpeed);
            transform.parent.position = smoothedPosition;
        }
    }
}
