using System.Linq;
using UnityEngine;

public class SampleObject : MonoBehaviour
{
    [SerializeField] private GameObject m_Floor;
    [SerializeField] private GameObject m_Wall;
    [SerializeField] private GameObject m_Window;
    [SerializeField] private GameObject m_Door;

    public void SetActive(BuildingsEnum building)
    {
        m_Floor.SetActive(false);
        m_Wall.SetActive(false);
        m_Window.SetActive(false);
        m_Door.SetActive(false);

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
                break;
        }
    }
}
