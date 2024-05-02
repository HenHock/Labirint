using NaughtyAttributes;
using UnityEngine;

namespace Runtime.Logic
{
    /// <summary>
    /// Unique id for save system to identify different instance of one prefab or component. For example, different enemies in the level. 
    /// </summary>
    public class UniqueID : MonoBehaviour
    {
        [ReadOnly] public string m_ID;
    }
}