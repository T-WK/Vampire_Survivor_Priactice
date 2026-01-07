using System;
using UnityEngine;

public static class TargetingService
{
    public static int FindNearestDamageablesInRange(
        Vector3 origin, float range, LayerMask mask,
        Collider[] hitsBuffer,
        IDamageable[] outTargets, float[] outDistSqr,
        int targetCount)
    {
        if (targetCount <= 0)
        {
            return 0;
        }

        int count = Physics.OverlapSphereNonAlloc(
            origin,
            range,
            hitsBuffer,
            mask,
            QueryTriggerInteraction.Ignore
        );

        int k = Mathf.Min(targetCount, outTargets.Length);
        int filled = 0;

        for (int i = 0; i < count; i++)
        {
            var col = hitsBuffer[i];
            if (col == null) continue;

            var d = col.GetComponentInParent<IDamageable>();
            if (d == null) { hitsBuffer[i] = null; continue; }
            var comp = d as Component;
            if (comp == null) { hitsBuffer[i] = null; continue; }

            float ds = (comp.transform.position - origin).sqrMagnitude;

            // Top-K 삽입 (거리 오름차순 유지, K가 작을 때 매우 효율적)
            InsertTopK(d, ds, k, outTargets, outDistSqr, ref filled);

            hitsBuffer[i] = null;
        }

        return filled;
    }

    static void InsertTopK(
        IDamageable d, float ds, int k,
        IDamageable[] targets, float[] dist,
        ref int filled)
    {
        if (k <= 0) return;
        // 아직 덜 찼으면 끝에 넣고 정렬 위치로 땡김
        if (filled < k)
        {
            targets[filled] = d;
            dist[filled] = ds;
            int idx = filled;
            filled++;

            while (idx > 0 && idx < dist.Length && dist[idx] < dist[idx - 1])
            {
                (dist[idx], dist[idx - 1]) = (dist[idx - 1], dist[idx]);
                (targets[idx], targets[idx - 1]) = (targets[idx - 1], targets[idx]);
                idx--;
            }
            return;
        }

        // 이미 K개 꽉 찼으면, 가장 먼 것보다 가까울 때만 삽입
        if (ds >= dist[k - 1]) return;

        // 맨 뒤(가장 먼 자리)에 넣고 위로 올림
        targets[k - 1] = d;
        dist[k - 1] = ds;

        int j = k - 1;
        while (j > 0 && j < dist.Length && dist[j] < dist[j - 1])
        {
            (dist[j], dist[j - 1]) = (dist[j - 1], dist[j]);
            (targets[j], targets[j - 1]) = (targets[j - 1], targets[j]);
            j--;
        }
    }
}
