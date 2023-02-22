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
            if (warrior.Health <= 0)
            {
                //Player Died
                GameManager.instance.PlayerDied();
            }
        }

        public void HealPlayer()
        {
            Debug.Log("Healing");
            GameManager.instance.CalculateMultiplier();
            warrior.ChangeHealth(10);
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