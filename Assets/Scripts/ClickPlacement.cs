using UnityEngine;

public class ClickPlacement : MonoBehaviour
{
    public GameObject prefabToPlace;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Place the prefab at the hit point
                Instantiate(prefabToPlace, hit.point, Quaternion.identity);
            }
        }
    }
}
