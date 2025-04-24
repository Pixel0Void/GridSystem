using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public GameObject Floor;
    public KeyCode FloorHotkey;

    public GameObject Wall;
    public KeyCode WallHotkey;

    public GameObject Window;
    public KeyCode WindowHotkey;

    public GameObject Door;
    public KeyCode DoorHotkey;

    public KeyCode RemoveHotkey;

    [SerializeField] private GameObject m_TargetObj;

    public void SetTargetObject()
    {
        if(Input.GetKeyDown(FloorHotkey))
        {
            SetTargetObject(Floor);
        }
        else if (Input.GetKeyDown(WallHotkey))
        {
            SetTargetObject(Wall);
        }
        else if (Input.GetKeyDown(WindowHotkey))
        {
            SetTargetObject(Window);
        }
        else if (Input.GetKeyDown(DoorHotkey))
        {
            SetTargetObject(Door);
        }
        else if(Input.GetKeyDown(RemoveHotkey))
        {
            SetTargetObject(null);
        }
    }

    private void SetTargetObject(GameObject obj)
    {
        m_TargetObj = obj;
    }
}
