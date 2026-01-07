using System.Collections.Generic;
using UnityEngine;

struct AttackModuleInfo
{
    public WeaponID weaponID;
    public int index;
};

public class WeaponController : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] float range = 50f;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] int bufferSize = 64;

    [SerializeField] Transform firePoint;

    Collider[] hits;
    private List<IAttackModule> attackModules = new List<IAttackModule>();
    private List<AttackModuleInfo> attackModuleInfos = new List<AttackModuleInfo>();

    private Player player;

    // class 필드로 재사용 버퍼 준비 (GC 방지)
    IDamageable[] targetBuffer;     // size = maxTargets
    float[] distBuffer;             // size = maxTargets

    void Awake()
    {
        hits = new Collider[bufferSize];
        targetBuffer = new IDamageable[16];
        distBuffer = new float[16];
        player = GetComponentInParent<Player>();
    }

    public void AddAttackModule(IAttackModule module)
    {
        WeaponID weaponID = module.WeaponID;
        foreach (AttackModuleInfo info in attackModuleInfos)
        {
            if (info.weaponID == weaponID)
            {
                Debug.Log($"WeaponController: 이미 동일한 무기ID({weaponID})의 공격 모듈이 존재합니다.");
                Debug.Log("해당 무기의 레벨을 증가시킵니다.");


                IAttackModule existingModule = attackModules[info.index];
                existingModule.ModuleLevelUP();

                return;
            }
        }

        AttackModuleInfo newInfo = new AttackModuleInfo
        {
            weaponID = weaponID,
            index = attackModules.Count
        };
        attackModuleInfos.Add(newInfo);
        attackModules.Add(module);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        int maxTargets = 0;

        foreach (var module in attackModules)
        {
            module.Tick(dt);
            maxTargets = Mathf.Max(maxTargets, module.TargetCount);
        }

        int found = TargetingService.FindNearestDamageablesInRange(
            transform.position, range, enemyMask,
            hits, targetBuffer, distBuffer, maxTargets);

        foreach (var module in attackModules)
        {
            module.Fire(targetBuffer, found, player.CurrentStat);
        }
    }


}
