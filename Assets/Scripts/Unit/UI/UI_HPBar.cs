using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_HPBar : MonoBehaviour
{
    private Unit targetUnit;
    private Animator unitAnim;

    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    void Start()
    {
        // [수정] 태그를 사용하여 'Player' 태그가 붙은 진짜 캐릭터를 찾습니다.
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            // 1. 그 오브젝트에서 Unit(또는 Player) 스크립트를 가져옵니다.
            targetUnit = playerObj.GetComponent<Unit>();
            if (targetUnit == null) targetUnit = playerObj.GetComponentInChildren<Unit>();

            // 2. 그 오브젝트나 자식에게서 Animator를 가져옵니다.
            unitAnim = playerObj.GetComponentInChildren<Animator>();

            // 3. 슬라이더 최대치 설정
            if (hpSlider != null && targetUnit != null)
                hpSlider.maxValue = targetUnit.CurrentStat.MaxHp;

            Debug.Log($"[자동 연결 성공] 찾은 오브젝트: {playerObj.name}, 애니메이터: {unitAnim != null}");
        }
        else
        {
            // 만약 태그 설정을 안 했다면 예전 방식으로 한 번 더 시도
            targetUnit = Object.FindFirstObjectByType<Player>();
            if (targetUnit != null) unitAnim = targetUnit.GetComponentInChildren<Animator>();

            Debug.LogWarning("[주의] Player 태그를 찾지 못해 이름으로 검색했습니다.");
        }
    }

    void Update()
    {
        if (targetUnit == null || hpSlider == null || hpText == null) return;

        float curHp = Mathf.Max(0, targetUnit.currentHp);
        float maxHp = targetUnit.CurrentStat.MaxHp;

        hpSlider.value = curHp;
        int percent = maxHp > 0 ? Mathf.FloorToInt((curHp / maxHp) * 100) : 0;
        hpText.text = $"{curHp} / {maxHp} ({percent}%)";

        if (unitAnim != null)
        {
            // 파트장님 설정: 애니메이터의 HP 파라미터 업데이트
            unitAnim.SetFloat("HP", curHp);
        }
    }
}