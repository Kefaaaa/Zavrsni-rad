using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Currency currencyManager; // Referenca na Currency

    private GameObject CurrentPlacingTower;
    private int currentTowerCost = 0;

    void Start()
    {
        if (EndGame.GameEnded)
            enabled = false;

        if (EndGame.GameStarted)
            enabled = false;

        if (PlayerCamera == null)
        {
            PlayerCamera = Camera.main;
        }

        // Pronađi Currency skriptu u sceni
        if (currencyManager == null)
        {
            currencyManager = FindObjectOfType<Currency>();
        }
    }

    void Update()
    {
        if (CurrentPlacingTower != null)
        {
            MoveTowerToMouse();

            if (Input.GetMouseButtonDown(0)) // Klikom postavljamo tower
            {
                PlaceTower();
            }
        }
    }

    private void MoveTowerToMouse()
    {
        Vector2 mousePos = PlayerCamera.ScreenToWorldPoint(Input.mousePosition);
        CurrentPlacingTower.transform.position = mousePos;
    }

    private void PlaceTower()
    {
        Debug.Log("Tower placed at: " + CurrentPlacingTower.transform.position);
        CurrentPlacingTower = null;
    }

    public void SetTowerToPlace(GameObject towerPrefab)
    {
        Tower towerData = towerPrefab.GetComponent<Tower>();

        if (towerData != null)
        {
            int towerCost = towerData.cost;

            if (currencyManager.Potrosnja(towerCost)) // Ako imamo dovoljno novca
            {
                CurrentPlacingTower = Instantiate(towerPrefab);
                currentTowerCost = towerCost;
                Debug.Log("Spawning tower. Cost: " + towerCost);
            }
            else
            {
                Debug.Log("Not enough money to buy this tower!");
            }
        }
        else
        {
            Debug.LogError("Tower prefab nema Tower skriptu!");
        }
    }
}
