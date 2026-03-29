using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
[Header("Health Settings")]
[SerializeField] private float maxHealth = 100f;
[SerializeField] private float currentHealth;
[Header("Regeneration")]
[SerializeField] private bool canRegenerate = false;
[SerializeField] private float regenRate = 5f; // HP/seconde
[SerializeField] private float regenDelay = 3f; // Délai après dégât
[Header("Effects")]
[SerializeField] private GameObject hitEffectPrefab;
[SerializeField] private GameObject deathEffectPrefab;
[Header("Events")]
public UnityEvent<float> OnHealthChanged;
public UnityEvent<float> OnDamageTaken;
public UnityEvent OnDeath;
public UnityEvent OnHeal;
private float lastDamageTime;
private bool isDead = false;
public float CurrentHealth => currentHealth;
public float MaxHealth => maxHealth;
public float HealthPercentage => currentHealth / maxHealth;
public bool IsDead => isDead;
public bool IsFullHealth => currentHealth >= maxHealth;
public bool IsCriticalHealth => HealthPercentage <= 0.3f;

void Start()
{
currentHealth = maxHealth;
lastDamageTime = -regenDelay;
}
void Update()
{
if (canRegenerate && !isDead && currentHealth < maxHealth)
if (Time.time - lastDamageTime >= regenDelay)
Heal(regenRate * Time.deltaTime);
}
public void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
{
if (isDead) return;
damage = Mathf.Max(0, damage);
currentHealth -= damage;
lastDamageTime = Time.time;
OnDamageTaken?.Invoke(damage);
OnHealthChanged?.Invoke(currentHealth);
if (hitEffectPrefab != null)
{
GameObject effect = Instantiate(hitEffectPrefab, hitPoint,
Quaternion.LookRotation(hitNormal));
Destroy(effect, 2f);
}
if (currentHealth <= 0) Die();
}
public void TakeDamage(float damage)
=> TakeDamage(damage, transform.position, Vector3.up);
public void Heal(float amount)
{
if (isDead) return;
float prev = currentHealth;
currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
if (currentHealth > prev) { OnHeal?.Invoke();
OnHealthChanged?.Invoke(currentHealth); }
}
private void Die()
{
if (isDead) return;
isDead = true;
currentHealth = 0;
OnDeath?.Invoke();
if (deathEffectPrefab != null)

Instantiate(deathEffectPrefab, transform.position,
Quaternion.identity);
}
public void Revive(float healthAmount)
{
isDead = false;
currentHealth = Mathf.Min(healthAmount, maxHealth);
OnHealthChanged?.Invoke(currentHealth);
}
void OnDrawGizmos()
{
if (!Application.isPlaying) return;
Vector3 pos = transform.position + Vector3.up * 2.5f;
Gizmos.color = Color.red;
Gizmos.DrawCube(pos, new Vector3(2f, 0.3f, 0.1f));
Gizmos.color = Color.green;
float w = 2f * HealthPercentage;
Vector3 hp = pos - new Vector3((2f - w) / 2f, 0, 0);
Gizmos.DrawCube(hp, new Vector3(w, 0.3f, 0.1f));
}
}
