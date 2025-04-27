using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float EditOrthographicSize;
    public float NonEditOrthographicSize = 6f;

    private float m_DebugRatio = 0.1f;
    private CameraMovement m_CameraMovement;

    private void Awake()
    {
        m_CameraMovement = GetComponent<CameraMovement>();
    }

    private void Start()
    {
        Camera.main.orthographicSize = NonEditOrthographicSize;
    }

    public void InOutEditMode(bool editMode)
    {
        StartCoroutine(RetriveEditMode(editMode));
        m_CameraMovement.FollowPlayer = !editMode;
    }

    IEnumerator RetriveEditMode(bool editMode)
    {
        if(editMode)
        {
            for (float value = EditOrthographicSize; value > Camera.main.orthographicSize + m_DebugRatio; Camera.main.orthographicSize += 0.2f)
            {
                yield return null;
            }
            Camera.main.orthographicSize = EditOrthographicSize;
        }
        else
        {
            for (float value = NonEditOrthographicSize; value < Camera.main.orthographicSize - m_DebugRatio; Camera.main.orthographicSize -= 0.4f)
            {
                yield return null;
            }
            Camera.main.orthographicSize = NonEditOrthographicSize;
        }
    }
}
