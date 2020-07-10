using TMPro;
using UnityEngine;

public class UICoinsText : MonoBehaviour
{
    private TextMeshProUGUI tmproText;

    private void Awake()
    {
        tmproText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnCoinsChange += HandleCoinsChange; 
    }

    private void HandleCoinsChange(int coins)
    {
        tmproText.text = coins.ToString();
    }
}
