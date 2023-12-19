using TMPro;
using UnityEngine;
public class UIManager: Updateable
{
    [SerializeField]
    private TextMeshProUGUI _countDefeated;
    private int _enemiesDefeatedValue = 0;

    public void UpdateEnemiesDefeat()
    {
        _enemiesDefeatedValue++;
        _countDefeated.text = _enemiesDefeatedValue.ToString();
    }
}

