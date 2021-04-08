using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Comoponents")]
    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text _scoreText, _livesText;


    [Header("UI References")]
    [SerializeField] PlayerHP _playerStat;
    [SerializeField] IntVariable _playerScore;

    int maxValue = 100;

    private void Update()
    {
        _livesText.text = _playerStat.lives.ToString();
        _scoreText.text = _playerScore.value.ToString();
        healthSlider.value = (float)_playerStat.health / maxValue;
    }

}
