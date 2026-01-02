using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public TextMeshProUGUI staminaText;
    private Player player;

    void Start()
    {
        // 씬에서 Player를 찾습니다.
        player = Object.FindFirstObjectByType<Player>();

        if (player != null)
        {
            // Player 스크립트에서 설정된 최대 스테미나 값을 가져와 슬라이더 최대치로 설정합니다.
            // 데이터 구조에 따라 MaxStamina를 가져오는 방식이 정확해야 합니다.
            float maxStam = player.CurrentStat != null ? player.CurrentStat.MaxStamina : 50f;
            staminaSlider.maxValue = maxStam;
        }
    }

    void Update()
    {
        if (player == null || staminaSlider == null || staminaText == null) return;

        // 1. Player 스크립트의 CurrentStamina 프로퍼티를 실시간으로 읽어옵니다.
        float curStamina = player.CurrentStamina;
        float maxStamina = player.CurrentStat != null ? player.CurrentStat.MaxStamina : 50f;

        // 2. 슬라이더 값 업데이트
        staminaSlider.value = curStamina;

        // 3. 텍스트 업데이트 (소수점 첫째자리까지만 표시하거나 정수로 표시)
        staminaText.text = $"{Mathf.FloorToInt(curStamina)} / {maxStamina}";
    }
}