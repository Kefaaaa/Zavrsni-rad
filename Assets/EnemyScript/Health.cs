using UnityEngine;

public class Health : MonoBehaviour
{
    public int HumanHealth = 100;

    void Update()
    {
        // Uništava objekt ako zdravlje padne na 0 ili ispod
        if (HumanHealth <= 0)
        {
            Destroy(gameObject);
            return; // Sprječava daljnje izvođenje koda
        }

        // Deaktivira objekt ako pređe određenu poziciju
        if (transform.position.x > 13)
        {
            gameObject.SetActive(false);
        }
    }

    // Funkcija za oduzimanje zdravlja
    public void TakeDamage(int damage)
    {
        HumanHealth -= damage;
        Debug.Log(gameObject.name + " primio " + damage + " štete. Preostalo zdravlje: " + HumanHealth);
    }
}
