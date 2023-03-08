using Core.ScriptableObjects;
using Enemies;
using Enums;
using Events;
using UnityEngine;

namespace Core.Bard
{
    public class ComboManager : MonoBehaviour
    {
        [SerializeField] private Combo currentCombo;
        public Enemy CurrentEnemy { get; private set;}
        [SerializeField][ReadOnly] private ComboValues expectedNote;
        public int comboIndex;
        private bool _noArmour;
        private bool _hasStarted;

        public float timeFrame = 5f;
        private float _sequenceStartTime = 0f;

        private void OnEnable()
        {
            GameEvents.onNewCombo += ComboStart;
            GameEvents.onButtonPressed += CheckComboValue;
            GameEvents.onComboFinish += ComboFinished;
        }

        private void OnDisable()
        {
            GameEvents.onNewCombo -= ComboStart;
            GameEvents.onButtonPressed -= CheckComboValue;
            GameEvents.onComboFinish -= ComboFinished;
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!_hasStarted) return;
            if (Time.time - _sequenceStartTime > timeFrame)
            {
                Debug.Log("Time Up");
                GameEvents.onComboFinish?.Invoke();
            }
        }

        private void CheckComboValue(ComboValues value)
        {
            if (value == expectedNote)
            {
                //Increase and set the next note
                comboIndex++;
                if (comboIndex >= currentCombo.ComboValues.Count)
                {
                    //all notes pressed
                    _noArmour = true;
                    GameEvents.onComboFinish?.Invoke();
                }
                else
                {
                    expectedNote = currentCombo.ComboValues[comboIndex];
                }
            }
            else
            {
                //Reset Index
                comboIndex = 0;
                expectedNote = currentCombo.ComboValues[comboIndex];
            }
            //Debug.Log(comboIndex);
        }
        private void ComboStart(Combo combo)
        {
            //start timer
            _sequenceStartTime = Time.time;
            _hasStarted = true;
            comboIndex = 0;

        }

        private void ComboFinished()
        {
            if(_noArmour && CurrentEnemy != null)
            {
                CurrentEnemy.canBeDestroyed = true;
            }
            currentCombo = null;
            _hasStarted = false;
            CurrentEnemy = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy" && currentCombo == null)
            {
                CurrentEnemy = collision.GetComponent<Enemy>();
                _noArmour = false;
                if (CurrentEnemy.enemyData.Combo == null) return;
                currentCombo = CurrentEnemy.enemyData.Combo; 
                GameEvents.onNewCombo?.Invoke(currentCombo);
                expectedNote = currentCombo.ComboValues[0];
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameEvents.onComboFinish?.Invoke();
            }
        }
    }
}