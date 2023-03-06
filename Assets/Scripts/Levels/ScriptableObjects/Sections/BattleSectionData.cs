using System.Collections.Generic;
using GameSections.Platforming.ScriptableObjects;
using UnityEngine;

namespace Levels.ScriptableObjects.Sections
{
    [CreateAssetMenu(fileName = "BattleSectionData", menuName = "Levels/Sections/New Battle Section Data", order = 0)]
    public class BattleSectionData : LevelSectionData
    {
        [field:SerializeField] public List<EnemyData> Enemies { get; private set; }
    }
}