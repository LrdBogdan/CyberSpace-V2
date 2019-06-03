public interface ITakeDamage
{
    int InternalKillPoints { get; set; }
    void OnDeath();
    void TakeDamage(int damage);

}