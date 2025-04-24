using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float Speed = 5f;
    private Transform m_Target;

    private void Awake()
    {
        m_Target = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(m_Target.position.x, transform.position.y, m_Target.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Speed * Time.deltaTime);
    }
}
