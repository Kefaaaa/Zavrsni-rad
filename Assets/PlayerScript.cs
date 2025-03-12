using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int Lives;
    public int StartLives = 10;
    public static int rounds;

    void Start()
    {
        Lives = StartLives;
    }

    void Update()
    {
        rounds = Spawn.rounds; // Update the rounds from the Spawn script
    }
}
