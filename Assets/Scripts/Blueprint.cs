using UnityEngine;

public enum BuildingsEnum
{
    Floor, Wall, Window, Door, Remove
}

public class Blueprint : MonoBehaviour
{
    private BuildingsEnum m_Buildings;
    public KeyCode FloorHotkey;
    public KeyCode WallHotkey;
    public KeyCode WindowHotkey;
    public KeyCode DoorHotkey;
    public KeyCode RemoveHotkey;

    public SampleObject SampleObject;

    private void Start()
    {
        SetTargetObject(BuildingsEnum.Floor);
    }

    public void SetTargetObject()
    {
        if (Input.GetKeyDown(FloorHotkey))
        {
            SetTargetObject(BuildingsEnum.Floor);
        }
        else if (Input.GetKeyDown(WallHotkey))
        {
            SetTargetObject(BuildingsEnum.Wall);
        }
        else if (Input.GetKeyDown(WindowHotkey))
        {
            SetTargetObject(BuildingsEnum.Window);
        }
        else if (Input.GetKeyDown(DoorHotkey))
        {
            SetTargetObject(BuildingsEnum.Door);
        }
        else if (Input.GetKeyDown(RemoveHotkey))
        {
            SetTargetObject(BuildingsEnum.Remove);
        }
    }

    private void SetTargetObject(BuildingsEnum building)
    {
        SampleObject.SetActive(building);
    }
}
