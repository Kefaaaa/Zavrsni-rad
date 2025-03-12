using UnityEngine;
using TMPro; // Dodaj ovo!

public class Currency : MonoBehaviour
{
    public int currency = 100; // Početni novac
    public TextMeshProUGUI currencyText; // Referenca na UI tekst

    void Start()
    {
        UpdateCurrencyUI();
    }

    public void Zarada(int iznos)
    {
        currency += iznos;
        Debug.Log("Dodano: " + iznos + " novca. Trenutni balans: " + currency);
        UpdateCurrencyUI();
    }

    public bool Potrosnja(int iznos)
    {
        if (iznos <= currency)
        {
            currency -= iznos;
            Debug.Log("Kupnja uspješna! Preostali novac: " + currency);
            UpdateCurrencyUI();
            return true;
        }
        else
        {
            Debug.Log("Nemate dovoljno novca! Potrebno: " + iznos + ", imate: " + currency);
            return false;
        }
    }

    void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = "Gold: " + currency;
        }
    }
}
