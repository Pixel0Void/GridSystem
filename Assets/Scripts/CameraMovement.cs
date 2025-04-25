using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float FollowSpeed = 5f;
    public float EditSpeed = 10f;
    private Transform m_Target;
    public bool FollowPlayer = true;

    private void Awake()
    {
        m_Target = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (FollowPlayer)
            FollowingPlayer();
        else
            Move();
    }

    private void FollowingPlayer()
    {
        Vector3 targetPos = new Vector3(m_Target.position.x, 0f, m_Target.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, FollowSpeed * Time.deltaTime);
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.up * z;

        transform.position = Vector3.Lerp(transform.position, transform.position + move, EditSpeed * Time.deltaTime);
    }
}
