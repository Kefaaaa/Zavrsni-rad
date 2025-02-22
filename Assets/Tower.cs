using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{

    public Transform target;

    [Header("Atributi")]

    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public float range = 15f;

    [Header("Unity Setup varijable")]
    public string enemyTag = "Enemy";

    public GameObject arrowPrefab;
    public Transform firePoint;



    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    void UpdateTarget()
    {
        Debug.Log("Tražim metu...");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null || !enemy.activeInHierarchy)
            {
                Debug.LogWarning("Pronađen uništen/deaktiviran neprijatelj, preskačem...");
                continue;
            }

            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            Debug.Log("Tower sada cilja: " + target.name);
        }
        else
        {
            target = null;
            Debug.Log("Nema neprijatelja u dometu.");
        }
    }







    void Update()
    {
        if (target == null)
        {
            Debug.Log("Tower nema metu!");
            return;
        }

        Debug.Log("Ciljam: " + target.name); // Ispisuje ime mete u Console

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Pokušaj pucanja bez valjane mete! Strelica neće biti ispaljena.");
            return;
        }

        GameObject projectileGO = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Projectile arrow = projectileGO.GetComponent<Projectile>();

        if (arrow != null)
        {
            arrow.Seek(target);
        }
    }





    // Crtanje Rangea našeg tornja 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        DrawWireCircle(transform.position, range);
    }

    void DrawWireCircle(Vector3 center, float radius, int segments = 30)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(radius, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }


    void HitTarget()
    {
        Debug.Log("Projektil pogodio metu!");

        Health enemyHealth = target.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(25); // Oduzmi 25 HP-a
        }

        Destroy(gameObject); // Uništi projektil
    }
}