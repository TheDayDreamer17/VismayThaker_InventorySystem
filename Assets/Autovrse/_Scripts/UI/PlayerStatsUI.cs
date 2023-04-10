
using UnityEngine;
using TMPro;
namespace Autovrse
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        private void OnEnable()
        {
            GameEvents.OnPlayerHealthChanged += OnPlayerHealthChanged;
        }
        private void OnDisable()
        {
            GameEvents.OnPlayerHealthChanged -= OnPlayerHealthChanged;
        }

        private void OnPlayerHealthChanged(float newHealthValue)
        {
            _healthText.text = newHealthValue.ToString();
        }
    }
}
