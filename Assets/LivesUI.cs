using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LivesUI : MonoBehaviour
{

    public TextMeshProUGUI LiveText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LiveText.text = PlayerScript.Lives + "  LIVES";
    }
}
