using UnityEngine;

namespace CodeBase.Utils
{
    public static class Extentions
    {
        public static void ResetChildLocalTransform(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;
        }
    }
}