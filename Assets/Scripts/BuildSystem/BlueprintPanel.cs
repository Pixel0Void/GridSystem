using UnityEngine;

public class BlueprintPanel : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void OpenClosePanel(bool value)
    {
        m_Animator.SetBool("Open", value);
        m_Animator.SetBool("Close", !value);
    }
}
