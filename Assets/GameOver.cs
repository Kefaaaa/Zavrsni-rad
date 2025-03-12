using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI rounds;

    void Update()
    {
        rounds.text = PlayerScript.rounds.ToString(); // Display the current round
    }
}
