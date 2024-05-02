#if UNITY_EDITOR
using System;
using System.Linq;
using Runtime.Logic;
using Runtime.Services.SaveSystem;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(UniqueID))]
    public class UniqueIDInspector : UnityEditor.Editor
    {
        private void Reset() => OnEnable();

        private void OnEnable()
        {
            var uniqueID = (UniqueID)target;

            if (string.IsNullOrEmpty(uniqueID.m_ID)) 
                Generate(uniqueID);
            else
            {
                var uniqueIds = FindObjectsByType<UniqueID>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                if (uniqueIds.Any(other => other != uniqueID && other.m_ID == uniqueID.m_ID)) 
                    Generate(uniqueID);
            }
        }

        private void Generate(UniqueID uniqueID)
        {
            uniqueID.m_ID = $"{uniqueID.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueID);
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }
        }
    }
}
#endif