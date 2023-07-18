using Shunrald;
using UnityEngine;

namespace CameraSystem
{
    [RequireComponent(typeof(CameraZoom))]
    [RequireComponent(typeof(CameraRotator))]
    [RequireComponent(typeof(CameraFollower))]
    public class CameraController : MonoBehaviour
    {
        public CameraFollower Follower { get; private set; }
        public CameraZoom Zoom { get; private set; }

        private void Awake()
        {
            GetRequiredComponent();
        }

        private void GetRequiredComponent()
        {
            Follower = GetComponent<CameraFollower>();
            Zoom = GetComponent<CameraZoom>();
        }
    }
}