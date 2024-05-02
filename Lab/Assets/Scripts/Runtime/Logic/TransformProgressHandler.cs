using System.Linq;
using Runtime.Data;
using Runtime.Services.SaveSystem;
using Runtime.Services.SaveSystem.ProgressService;
using UnityEngine;

namespace Runtime.Logic
{
    /// <summary>
    /// Component for save and load transform data.
    /// </summary>
    [RequireComponent(typeof(UniqueID))]
    public class TransformProgressHandler : MonoBehaviour, IProgressWriter
    {
        private UniqueID _uniqueID;

        private void Awake() => _uniqueID = GetComponent<UniqueID>();

        public void LoadProgress(GameProgress progress)
        {
            if (SaveDataExist(progress, out TransformData transformData))
            {
                transform.position = transformData.m_Position;
                transform.rotation = transformData.m_Rotation;
            }
        }

        public void UpdateProgress(GameProgress progress)
        {
            if (SaveDataExist(progress, out TransformData transformData))
            {
                transformData.m_Position = transform.position;
                transformData.m_Rotation = transform.rotation;
            }
            else CreateSaveData(progress);
        }

        private bool SaveDataExist(GameProgress progress, out TransformData transformData)
        {
            transformData = progress.m_WorldData.m_TransformDataList.FirstOrDefault(data => data.m_UniqueID == _uniqueID.m_ID);
            return transformData != null;
        }

        private void CreateSaveData(GameProgress progress) =>
            progress.m_WorldData.m_TransformDataList.Add(new TransformData(_uniqueID.m_ID, transform.position, transform.rotation));
    }
}