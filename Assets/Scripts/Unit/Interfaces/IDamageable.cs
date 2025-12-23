public interface IDamageable
{
    void TakeDamage(float damage);
    float InvincibleLength { get; }
    float InvincibleCounter { get; set; }
    bool IsInvincible { get; }
    bool IsDead { get; }
}