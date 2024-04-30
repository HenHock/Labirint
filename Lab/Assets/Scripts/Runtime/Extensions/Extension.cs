using UnityEngine;

namespace Runtime.Extensions
{
    public static class Extension
    {
        public static string ToJson<T>(this T source) => JsonUtility.ToJson(source);
        public static T FromJson<T>(this string jsonStr) => JsonUtility.FromJson<T>(jsonStr);

        public static Vector2 ToVector2(this Vector3 source) => source;
        public static Vector3 ToVector3(this Vector2 source) => source;
        
        public static GameObject FindObjectWithTag(this Component transform, string tag)
        {
            // Check if parentTransform is null
            if (transform == null)
            {
                Debug.LogWarning("Parent transform is null.");
                return null;
            }

            // Search for a child object with the specified tag
            foreach (Transform child in transform.transform)
            {
                if (child.CompareTag(tag))
                {
                    return child.gameObject;
                }
                
                // Recursively search in children if not found in this child
                GameObject foundObject = FindObjectWithTag(child, tag);
                
                if (foundObject != null)
                {
                    return foundObject;
                }
            }

            // If no child with the specified tag found, return null
            return null;
        }
    }
}
