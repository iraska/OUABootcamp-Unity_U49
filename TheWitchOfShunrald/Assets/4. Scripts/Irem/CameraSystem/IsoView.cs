using UnityEngine;

namespace CameraSystem
{
    public static class IsoView
    {
        // change to cooridinat system (like up direction)
        private static Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        // reusable extention method
        public static Vector3 ToIso(this Vector3 input) => isoMatrix.MultiplyPoint3x4(input);
    }
}