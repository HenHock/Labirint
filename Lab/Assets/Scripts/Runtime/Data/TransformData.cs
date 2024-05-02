using System;
using UnityEngine;

namespace Runtime.Data
{
    [Serializable]
    public class TransformData
    {
        public string m_UniqueID;
        public Vector3 m_Position;
        public Quaternion m_Rotation;

        public TransformData(string uniqueID, Vector3 position, Quaternion rotation)
        {
            m_UniqueID = uniqueID;
            m_Position = position;
            m_Rotation = rotation;
        }
    }
}