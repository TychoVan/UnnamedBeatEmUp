public interface I_Damagable
{
    int Health { get; }
    void ChangeHealth(int amount);
    void OnDeath();
}
