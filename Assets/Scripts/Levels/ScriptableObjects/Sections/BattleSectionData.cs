using UnityEngine;

namespace Levels.ScriptableObjects.Sections
{
    [CreateAssetMenu(fileName = "BattleSectionData", menuName = "Levels/Sections/New Battle Section Data", order = 0)]
    public class BattleSectionData : LevelSectionData
    {
        [field:SerializeField] public Enemy EnemyToBattle { get; private set; }
    }
}