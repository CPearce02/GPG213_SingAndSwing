using Levels.ScriptableObjects;
using Levels.ScriptableObjects.Sections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes
{
    public class SectionLoader : MonoBehaviour
    {
        // This is a prototype of the functionality, this will be more robust
        [field: SerializeField] public LevelData Level { get; private set; }
        [field: SerializeField] public LevelSectionData SectionToLoad { get; private set; }
        [SerializeField] private GameObject currentSegment;
        [SerializeField] private GameObject nextSegment;
        
        bool isLoaded;
        private Mouse mouse;
        
        private void Awake() => mouse = Mouse.current;

        // This script will need some events that will be called to initiate the loading of the next section.
        // And it will be fired by a trigger area when the player enters it.
        
        void Start()
        {
            if (Level == null)
            {
                Debug.LogError("No level data found!");
                return;
            }
            
            Level.Init();

            SectionToLoad = Level.NextSection;
            nextSegment = SectionToLoad.SegmentPrefab;

        }

        private void Update()
        {
            // This should be removed eventually because it wont be a click to load
            if (mouse.leftButton.wasPressedThisFrame && !isLoaded) 
            {
                var test = Level.InstantiateSection(currentSegment.transform, nextSegment);
                isLoaded = true;
                Level.Step();
            }
        }
    }
}
