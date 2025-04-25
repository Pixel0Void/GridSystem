using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class BuildSystem : MonoBehaviour
{
    public int GridSize;
    public LayerMask GroundLayerMask;
    public LayerMask BuildingsLayerMask;
    public Blueprint Blueprint;
    public Transform BuildingsParent;
    private Grid m_Grid;

    [Space(20)]
    public GameObject FloorPrefab;
    public GameObject WallPrefab;
    public GameObject WindowPerfab;
    public GameObject DoorPrefab;

    private bool m_IsAccuratePosition;
    private Boundary m_Boundary;
    private MeshRenderer m_LastSelectedObject;
    private int m_LastSelectedObjID;

    private void Awake()
    {
        m_Grid = GetComponent<Grid>();
        m_Boundary = new Boundary(GridSize, (int)m_Grid.cellSize.x);
    }

    public void EditMode()
    {
        if (Blueprint.Buildings == BuildingsEnum.None)
            return;

        if (Blueprint.Buildings == BuildingsEnum.Remove)
            Remove();

        Build();
    }

    private void Build()
    {
        Vector3 selectedPosition = GetSelectedMapPosition();
        Vector3Int cellPosition = m_Grid.WorldToCell(selectedPosition);
        m_IsAccuratePosition = IsAcceptableCell(Blueprint.SampleObject.transform.position);
        Blueprint.SampleObject.SetPosition(m_Grid.GetCellCenterWorld(cellPosition), m_IsAccuratePosition);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject targetObj = null;
            switch (Blueprint.Buildings)
            {
                case BuildingsEnum.Wall:
                    targetObj = WallPrefab;
                    break;
                case BuildingsEnum.Window:
                    targetObj = WindowPerfab;
                    break;
                case BuildingsEnum.Door:
                    targetObj = DoorPrefab;
                    break;
                case BuildingsEnum.Floor:
                    targetObj = FloorPrefab;
                    break;
            }

            if (targetObj != null)
            {
                Instantiate(targetObj, Blueprint.SampleObject.transform.position, Blueprint.SampleObject.transform.rotation, BuildingsParent);
            }
        }
    }

    private void Remove()
    {
        Vector3 mousPos = Input.mousePosition;
        mousPos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousPos);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

        if (Physics.Raycast(ray, out hit, 1000, BuildingsLayerMask))
        {
            if (m_LastSelectedObjID != hit.transform.parent.GetInstanceID())
            {
                if (m_LastSelectedObject != null)
                {
                    m_LastSelectedObject.material.color = Color.white;
                    m_LastSelectedObject = null;
                }
                m_LastSelectedObject = hit.transform.GetComponentInChildren<MeshRenderer>();
                m_LastSelectedObject.material.color = Color.red;
                m_LastSelectedObjID = hit.transform.parent.GetInstanceID();
            }
        }
        else
        {
            if (m_LastSelectedObject != null)
            {
                m_LastSelectedObject.material.color = Color.white;
                m_LastSelectedObject = null;
            }
            m_LastSelectedObjID = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Destroy(hit.transform.parent.gameObject);
        }
    }

    private bool IsAcceptableCell(Vector3 cellPos)
    {
        if (Blueprint.Buildings == BuildingsEnum.Floor)
        {
            return IsOutOfFloor(cellPos) && m_Boundary.IsInBound(cellPos);
        }

        if (m_Boundary.IsInBound(cellPos))
            return IsEmptyNeighborCell(cellPos) && IsEmptyCell(cellPos);
        else
            return m_Boundary.IsInBound(GetNeighborCellPosition(cellPos, Blueprint.SampleObject.transform.rotation)) && IsEmptyCell(cellPos);
    }

    private bool IsEmptyCell(Vector3 cellPos)
    {
        Transform[] ts = BuildingsParent.GetComponentsInChildren<Transform>().Where(x => !x.CompareTag("Floor") && new Vector3(x.localPosition.x, 0f, x.localPosition.z) == cellPos).ToArray();
        if (ts.Length == 0)
            return true;

        foreach (var item in ts)
        {
            if (Mathf.RoundToInt(item.eulerAngles.y) == Mathf.RoundToInt(Blueprint.SampleObject.transform.eulerAngles.y))
                return false;
        }

        return true;
    }

    private bool IsEmptyNeighborCell(Vector3 cellPos)
    {
        Vector3 neighborCellPos = GetNeighborCellPosition(cellPos, Blueprint.SampleObject.transform.rotation);
        Transform[] neighborCell = BuildingsParent.GetComponentsInChildren<Transform>().Where(x => !x.CompareTag("Floor") && new Vector3(x.localPosition.x, 0f, x.localPosition.z) == neighborCellPos).ToArray();

        foreach (var item in neighborCell)
        {
            if (Mathf.RoundToInt(item.eulerAngles.y) == Mathf.RoundToInt(InversedRotation(Blueprint.SampleObject.transform.rotation).eulerAngles.y))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsOutOfFloor(Vector3 cellPos)
    {
        Transform ts = BuildingsParent.GetComponentsInChildren<Transform>().Where(x => x.CompareTag("Floor") && new Vector3(x.localPosition.x, 0f, x.localPosition.z) == cellPos).FirstOrDefault();
        return ts == null;
    }

    private Vector3 GetSelectedMapPosition()
    {
        Vector3 pos = new Vector3();
        Vector3 mousPos = Input.mousePosition;
        mousPos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousPos);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

        if (Physics.Raycast(ray, out hit, 1000, GroundLayerMask))
        {
            pos = hit.point;
        }

        return pos;
    }

    private Quaternion InversedRotation(Quaternion rot)
    {
        float yRotation = rot.eulerAngles.y;
        Quaternion res = Quaternion.identity;
        switch (yRotation)
        {
            case 0f:
                res = Quaternion.Euler(Vector3.up * 180f);
                break;
            case 90f:
                res = Quaternion.Euler(Vector3.up * -90f);
                break;
            case 180f:
                res = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 270f:
                res = Quaternion.Euler(Vector3.up * 90f);
                break;
        }
        return res;
    }

    private Vector3 GetNeighborCellPosition(Vector3 cellPosition, Quaternion rotation)
    {
        float dir = rotation.eulerAngles.y;
        Vector3 neighborCellPos = Vector3.zero;
        switch (dir)
        {
            case 0f:
                neighborCellPos = cellPosition + (Vector3.forward * m_Grid.cellSize.z);
                break;
            case 90f:
                neighborCellPos = cellPosition + (Vector3.right * m_Grid.cellSize.x);
                break;
            case 180f:
                neighborCellPos = cellPosition + (Vector3.back * m_Grid.cellSize.z);
                break;
            case 270f:
                neighborCellPos = cellPosition + (Vector3.left * m_Grid.cellSize.x);
                break;
        }
        return neighborCellPos;
    }
}
