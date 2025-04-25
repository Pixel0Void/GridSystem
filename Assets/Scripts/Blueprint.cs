using UnityEngine;

public enum BuildingsEnum
{
    Floor, Wall, Window, Door, Remove, None
}

public class Blueprint : MonoBehaviour
{
    public BuildingsEnum Buildings;
    public KeyCode FloorHotkey;
    public KeyCode WallHotkey;
    public KeyCode WindowHotkey;
    public KeyCode DoorHotkey;
    public KeyCode RemoveHotkey;

    public SampleObject SampleObject;

    private void Start()
    {
        SetTargetObject(BuildingsEnum.None);
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

        SampleObject.Rotate();
    }

    public void SetTargetObject(BuildingsEnum building)
    {
        Buildings = building;
        SampleObject.SetActive(building);
    }
}
