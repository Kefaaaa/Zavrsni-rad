using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public int damage = 0;

    public void Seek(Transform _target)
    {
        if (_target == null || !_target.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Strelica dobila neispravan target! Uništavam strelicu.");
            Destroy(gameObject);
            return;
        }

        target = _target;
    }

    void Start()
    {

    }


    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Meta nestala tijekom leta projektila! Uništavam projektil.");
            Destroy(gameObject);
            return;
        }

        // Izračunavamo smjer prema mete
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Ako je strelica dovoljno blizu mete, registriraj pogodak
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // **Rotacija strelice tako da njen vrh (koji u početku gleda lijevo) pokazuje prema metu**
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Kut prema metu
        transform.rotation = Quaternion.Euler(0, 0, angle + 180); // Dodajemo 180° jer sprite u početku gleda lijevo

        // **Pomicanje strelice direktno prema mete bez kruženja**
        transform.position = Vector3.MoveTowards(transform.position, target.position, distanceThisFrame);
    }





    void HitTarget()
    {
        Debug.Log("Projektil pogodio metu!");

        Health enemyHealth = target.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage); // Oduzmi 25 HP-a
        }

        Destroy(gameObject); // Uništi projektil
    }
}
