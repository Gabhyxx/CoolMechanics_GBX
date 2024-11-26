using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IHasCooldown
{
    int id;
    float cooldownDuration;

    public int Id => id;
    public float CooldownDuration => cooldownDuration;

    [Header("Player")]
    [SerializeField] private GameObject player;
    float cd1;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI abilityOneText;

    private void Update()
    {
        cd1 = player.GetComponent<CooldownSystem>().GetRemainingDuration(1);

        cd1 = Mathf.FloorToInt(cd1);

        abilityOneText.text = "Ability 1: " + cd1;
    }
}
