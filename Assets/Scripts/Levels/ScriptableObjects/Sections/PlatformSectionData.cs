using System.Collections.Generic;
using Enemies.ScriptableObjects;
using UnityEngine;

namespace Levels.ScriptableObjects.Sections
{
    [CreateAssetMenu(fileName = "PlatformSectionData", menuName = "Levels/Sections/New Platform Section Data", order = 0)]
    public class PlatformSectionData : SectionData
    {
        [field:SerializeField] public List<EnemyData> Enemies { get; private set; }
    }
}