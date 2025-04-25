using UnityEngine;

public class SampleObject : MonoBehaviour
{
    public GameObject Floor;
    public GameObject Wall;
    public GameObject Window;
    public GameObject Door;

    public Color AccurateColor;
    public Color UnaccurateColor;

    private bool m_IsEnable;
    private GameObject m_ActiveObject;
    private Material m_ActiveObjectMaterial;

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
                m_ActiveObject = Floor;
                break;
            case BuildingsEnum.Wall:
                Wall.SetActive(true);
                m_ActiveObject = Wall;
                break;
            case BuildingsEnum.Window:
                Window.SetActive(true);
                m_ActiveObject = Window;
                break;
            case BuildingsEnum.Door:
                Door.SetActive(true);
                m_ActiveObject = Door;
                break;
            case BuildingsEnum.Remove:
                m_IsEnable = false;
                m_ActiveObject = null;
                break;
            case BuildingsEnum.None:
                m_IsEnable = false;
                m_ActiveObject = null;
                break;
        }

        m_ActiveObjectMaterial = m_ActiveObject?.GetComponent<Renderer>().material;
    }

    public void SetPosition(Vector3 position, bool isAccurate)
    {
        transform.position = position;
        if (m_ActiveObjectMaterial != null)
            m_ActiveObjectMaterial.color = (isAccurate ? AccurateColor : UnaccurateColor);
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
