using UnityEngine;

public class PathMovement : MonoBehaviour
{
    public PathContainer pathContainer; // Referenca na PathContainer objekt
    private Transform[] path; // Interna lista točaka
    private int curPointIndex = 0; // Trenutni indeks
    private int nextPointIndex = 1; // Sljedeći indeks
    private float percent = 0f; // Progres između točaka
    public float speed = 2f; // Brzina kretanja

    void Start()
    {
        if (pathContainer == null)
        {
            // Pokušaj pronaći PathContainer u sceni prema imenu ili tagu
            pathContainer = GameObject.FindFirstObjectByType<PathContainer>();

            if (pathContainer == null)
            {
                Debug.LogError("PathContainer nije pronađen u sceni! Postavi ga ručno ili koristi GameObject.Find.");
                return;
            }
        }

        // Prekopiraj točke iz PathContainera
        path = pathContainer.pathPoints;

        if (path == null || path.Length < 2)
        {
            Debug.LogError("PathContainer mora imati barem dvije točke!");
            return;
        }

        // Postavi objekt na prvu točku
        transform.position = path[0].position;
    }

    void Update()
    {
        if (path == null || path.Length < 2) return; // Treba barem dvije točke za kretanje

        percent += speed * Time.deltaTime / Vector3.Distance(path[curPointIndex].position, path[nextPointIndex].position);

        // Move between current and next points
        transform.position = Vector3.Lerp(path[curPointIndex].position, path[nextPointIndex].position, percent);

        if (percent >= 1f)
        {
            percent = 0f; // Reset progress
            curPointIndex = nextPointIndex; // Update indices
            nextPointIndex = (nextPointIndex + 1) % path.Length; // Loop if at the end
        }
    }
}
