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

        [Header("SlowMo")]
        [SerializeField] private float slowMotionTimeScale;
        private float _startTimeScale;
        private float _startFixedDeltaTime;


        // Start is called before the first frame update
        void Start()
        {
            _startTimeScale = Time.timeScale;
            _startFixedDeltaTime = Time.fixedDeltaTime;
        }

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

        // Update is called once per frame
        void Update()
        {
            if (!_hasStarted) return;
            //if (Time.time - _sequenceStartTime > timeFrame)
            //{
            //    Debug.Log("Time Up");
            //    GameEvents.onComboFinish?.Invoke();
            //}
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
            SlowDownTime();
        }

        private void ComboFinished()
        {
            ResetTime();

            if (_noArmour && CurrentEnemy != null)
            {
                CurrentEnemy.canBeDestroyed = true;
            }
            currentCombo = null;
            _hasStarted = false;
            CurrentEnemy = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy enemyComponent) && currentCombo == null) 
            {
                CurrentEnemy = enemyComponent;
                _noArmour = false;
                if (enemyComponent.enemyData == null) return;
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

        private void SlowDownTime()
        {
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime * slowMotionTimeScale;
        }
        private void ResetTime()
        {
            Time.timeScale = _startTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime;
        }
    }
}