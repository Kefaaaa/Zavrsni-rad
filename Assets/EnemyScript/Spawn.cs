using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Human;
    public float SpawnRate = 2;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < SpawnRate)
            timer += Time.deltaTime;
        else
        {
            Instantiate(Human, transform.position, transform.rotation);
            timer = 0;
        }
    }
}
