using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;

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

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }


    void HitTarget()
    {
        if (target == null) return; // Ako je meta već uništena, ne nastavljamo

        Debug.Log("Pogodak!");

        Destroy(gameObject); // Uništavamo projektil
    }
}

