using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
    public GameObject Human;
    public static int rounds = 1;
    private static int activeSpawners = 0;
    private static bool isSpawning = false;

    private void Start()
    {
        activeSpawners++;

        if (activeSpawners == 4) // Only one spawner should start the spawning process
        {
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        // ✅ Wait until GameStarted is true before spawning
        while (!EndGame.GameStarted)
        {
            yield return null; // Wait until the next frame to check again
        }

        yield return new WaitForSeconds(2f); // Initial delay before first wave

        while (true) // Infinite loop for continuous waves
        {
            isSpawning = true;

            for (int i = 0; i < rounds; i++)
            {
                foreach (Spawn spawner in FindObjectsOfType<Spawn>())
                {
                    StartCoroutine(spawner.SpawnEnemies(rounds));
                    yield return new WaitForSeconds(2f); // Delay between spawns
                }
            }

            isSpawning = false;
            yield return new WaitForSeconds(10f); // Delay before next wave
            rounds++; // Increase enemies per wave
        }
    }

    private IEnumerator SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(Human, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
