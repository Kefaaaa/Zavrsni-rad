using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float explosionRadius = 3f;
    public int damage = 100;
    public GameObject explosionEffect; // Ovaj objekat sadrži animaciju eksplozije
    public LayerMask enemyLayer;

    private bool hasExploded = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("💥 Landmine hit: " + collision.gameObject.name + " | Layer: " + collision.gameObject.layer);

        if (hasExploded) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("🔥 Enemy detected! Triggering explosion...");
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;
        Debug.Log("💥💥💥 EXPLOSION TRIGGERED 💥💥💥");

        if (explosionEffect != null)
        {
            // Instanciraj objekat eksplozije na mestu mine
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Onda animiraj eksploziju prema dolje (ako želiš da nestane ispod scene)
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y - 1f, explosion.transform.position.z);

            // Uništi eksploziju nakon 1 sekunde (ili prema trajanju animacije)
            Destroy(explosion, 0.5f);
        }

        // Detektuj sve neprijatelje unutar radijusa eksplozije
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        Debug.Log("🔴 Hit Enemies: " + hitEnemies.Length);

        // Nanosi štetu svim pogodjenim neprijateljima
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("🟢 Damaging enemy: " + enemy.name);
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Uništiti samu minu nakon što je eksplodirala
        Destroy(gameObject, 0.1f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
