using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private TMP_Text txtGold;
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private GameObject btnStart;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateGold(int amount)
    {
        txtGold.text = $"Gold: {amount}";
    }

    public void UpdateHealth(int amount)
    {
        txtHealth.text = $"HP: {amount}";
    }

    public void SetStartButtonActive(bool active)
    {
        btnStart.SetActive(active);
    }
}
