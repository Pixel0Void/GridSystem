using UnityEngine;

public class SampleObject : MonoBehaviour
{
    public GameObject Floor;
    public GameObject Wall;
    public GameObject Window;
    public GameObject Door;

    private bool m_IsEnable;

    public void SetActive(BuildingsEnum building)
    {
        Floor.SetActive(false);
        Wall.SetActive(false);
        Window.SetActive(false);
        Door.SetActive(false);

        m_IsEnable = true;

        switch (building)
        {
            case BuildingsEnum.Floor:
                Floor.SetActive(true);
                break;
            case BuildingsEnum.Wall:
                Wall.SetActive(true);
                break;
            case BuildingsEnum.Window:
                Window.SetActive(true);
                break;
            case BuildingsEnum.Door:
                Door.SetActive(true);
                break;
            case BuildingsEnum.Remove:
                m_IsEnable = false;
                break;
            case BuildingsEnum.None:
                m_IsEnable = false;
                break;
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Rotate()
    {
        if (m_IsEnable)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateTargetObjByAngle(Vector3.up, 90f);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateTargetObjByAngle(Vector3.up, -90f);
            }
        }
    }

    private void RotateTargetObjByAngle(Vector3 axis, float angle)
    {
        transform.Rotate(axis * angle);
    }
}
