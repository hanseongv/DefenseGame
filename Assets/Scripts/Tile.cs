using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 GetPosition => transform.position;

    public bool IsOccupied => currentOccupant != null;

    private GameObject currentOccupant;

    public void SetOccupant(GameObject unit)
    {
        currentOccupant = unit;
    }

    public void ClearOccupant()
    {
        currentOccupant = null;
    }
}