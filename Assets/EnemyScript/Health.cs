using UnityEngine;

public class Health : MonoBehaviour
{
    public int HumanHealth = 100;
    public GameObject DeathEffect;
    [SerializeField] public int Vrijednost;

    private Currency currencyManager; // Reference to the Currency script

    void Start()
    {
        currencyManager = FindObjectOfType<Currency>();
        if (currencyManager == null)
        {
            Debug.LogError("No Currency script found in the scene!");
        }
    }

    void Update()
    {
        if (HumanHealth <= 0)
        {
            GameObject effect = Instantiate(DeathEffect, transform.position, transform.rotation);
            effect.GetComponent<Renderer>().sortingLayerName = "Foreground";
            effect.GetComponent<Renderer>().sortingOrder = 10;

            Destroy(effect, 1f);

            if (currencyManager != null)
            {
                currencyManager.Zarada(Vrijednost); // ✅ Corrected call
            }
            else
            {
                Debug.LogError("currencyManager is null, cannot add money.");
            }

            Destroy(gameObject);
        }

        if (transform.position.x >= 10)
        {
            Destroy(gameObject);
            PlayerScript.Lives -= 1;
        }
    }

    public void TakeDamage(int damage)
    {
        HumanHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining health: " + HumanHealth);
    }
}
