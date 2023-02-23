using UnityEngine;

namespace Core.Player
{
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager instance;
        public CharacterData warrior;
        public HealthBarController hc;

        private void Start()
        {
            instance = this;
            hc.UpdateHealthBar(warrior.Health, warrior.MaxHealth);
        }

        private void Update()
        { 
            //IF PLAYER DIED
            if (warrior.Health <= 0)
            { 
                warrior.ChangeLives(-1);
                HealPlayer(100);
                GameManager.instance.PlayerDied();
            }
        }

        public void HealPlayer(int healingAmount)
        {
            Debug.Log("Healing");
            GameManager.instance.CalculateMultiplier();
            warrior.ChangeHealth(healingAmount);
            hc.UpdateHealthBar(warrior.Health, warrior.MaxHealth);
        }

        public void DamagePlayer(int damageAmount)
        {
            Debug.Log("Damage Player =" + damageAmount);
            GameManager.instance.ResetMultiplier();
            warrior.ChangeHealth(-damageAmount);
            hc.UpdateHealthBar(warrior.Health, warrior.MaxHealth);
        }

    }
}