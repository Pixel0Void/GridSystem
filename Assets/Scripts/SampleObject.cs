using System.Linq;
using UnityEngine;

public class SampleObject : MonoBehaviour
{
    public GameObject m_Floor;
    public GameObject m_Wall;
    public GameObject m_Window;
    public GameObject m_Door;

    private bool m_IsEnable;

    public void SetActive(BuildingsEnum building)
    {
        m_Floor.SetActive(false);
        m_Wall.SetActive(false);
        m_Window.SetActive(false);
        m_Door.SetActive(false);

        m_IsEnable = true;

        switch (building)
        {
            case BuildingsEnum.Floor:
                m_Floor.SetActive(true);
                break;
            case BuildingsEnum.Wall:
                m_Wall.SetActive(true);
                break;
            case BuildingsEnum.Window:
                m_Window.SetActive(true);
                break;
            case BuildingsEnum.Door:
                m_Door.SetActive(true);
                break;
            case BuildingsEnum.Remove:
                m_IsEnable = false;
                break;
            case BuildingsEnum.None:
                m_IsEnable = false;
                break;
        }
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
