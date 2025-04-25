using UnityEngine;

[RequireComponent(typeof(Grid))]
public class BuildSystem : MonoBehaviour
{
    public LayerMask GroundLayerMask;
    private Grid m_Grid;

    private void Awake()
    {
        m_Grid = GetComponent<Grid>();
    }

    public void EditMode()
    {
        GetSelectedMapPosition();
    }

    private Vector3 GetSelectedMapPosition()
    {
        Vector3 pos = new Vector3();
        Vector3 mousPos = Input.mousePosition;
        mousPos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousPos);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

        if(Physics.Raycast(ray, out hit, 1000, GroundLayerMask))
        {
            pos = hit.point;
        }

        return pos;
    }
}
