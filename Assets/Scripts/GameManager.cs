using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool m_InEditMode = false;
    private CameraController m_CameraController;
    public BlueprintPanel BlueprintPanel;
    public Blueprint Blueprint;
    public BuildSystem BuildSystem;

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
            BlueprintPanel.OpenClosePanel(m_InEditMode);
            
            if (!m_InEditMode)
                Blueprint.SetTargetObject(BuildingsEnum.None);
        }

        if(m_InEditMode)
        {
            Blueprint.SetTargetObject();
            BuildSystem.EditMode();
        }
    }
}
