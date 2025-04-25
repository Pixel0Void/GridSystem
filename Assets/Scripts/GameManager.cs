using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool m_InEditMode = false;
    private CameraController m_CameraController;
    private PlayerMovement m_Player;
    public BlueprintPanel BlueprintPanel;
    public Blueprint Blueprint;
    public BuildSystem BuildSystem;
    public GameObject GridPlane;

    private void Awake()
    {
        m_CameraController = Camera.main.GetComponent<CameraController>();
        m_Player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            m_InEditMode = !m_InEditMode;
            m_CameraController.InOutEditMode(m_InEditMode);
            BlueprintPanel.OpenClosePanel(m_InEditMode);

            GridPlane.SetActive(m_InEditMode);
            m_Player.Moving(!m_InEditMode);
            
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
