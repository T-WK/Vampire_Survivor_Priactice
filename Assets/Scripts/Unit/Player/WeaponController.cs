using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] float range = 50f;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] int bufferSize = 64;

    [SerializeField] Transform firePoint;

    Collider[] hits;
    private List<IAttackModule> attackModules = new List<IAttackModule>();

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
