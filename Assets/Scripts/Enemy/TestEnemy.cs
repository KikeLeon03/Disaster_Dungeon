using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;
    private Renderer rend;
    private Color originalColor;
    private float flashTimer;

    void Awake()
    {
        currentHealth = maxHealth;
        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    void Update()
    {
        if (flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0f && rend != null)
                rend.material.color = originalColor;
        }
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0f) return;

        currentHealth = Mathf.Max(0f, currentHealth - amount);

        if (rend != null)
        {
            rend.material.color = Color.red;
            flashTimer = 0.12f;
        }

        if (currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        Debug.Log($"[Enemy] {name} died!");
        gameObject.SetActive(false);
    }

    void OnGUI()
    {
        if (Camera.main == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2.2f);
        if (screenPos.z < 0f) return;

        float barW = 80f;
        float barH = 8f;
        float x = screenPos.x - barW * 0.5f;
        float y = Screen.height - screenPos.y;
        float fillRatio = currentHealth / maxHealth;

        // Background
        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(x - 1, y - 1, barW + 2, barH + 2), Texture2D.whiteTexture);

        // Empty bar
        GUI.color = new Color(0.3f, 0f, 0f);
        GUI.DrawTexture(new Rect(x, y, barW, barH), Texture2D.whiteTexture);

        // Health fill
        GUI.color = Color.Lerp(Color.red, Color.green, fillRatio);
        GUI.DrawTexture(new Rect(x, y, barW * fillRatio, barH), Texture2D.whiteTexture);

        // Label
        GUI.color = Color.white;
        GUI.Label(new Rect(x, y - 16f, barW, 16f), $"{name}  {currentHealth:0}/{maxHealth:0}");
    }
}
