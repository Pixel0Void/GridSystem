using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool m_InEditMode = false;
    private CameraController m_CameraController;

    private void Awake()
    {
        m_CameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            m_InEditMode = !m_InEditMode;
            m_CameraController.InOutEditMode(m_InEditMode);
        }
    }
}
