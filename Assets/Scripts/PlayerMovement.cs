using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    private CharacterController m_Controller;
    private Vector3 m_Velocity;
    private bool m_IsGrounded;

    private void Awake()
    {
        m_Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        m_IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (m_IsGrounded && m_Velocity.y < 0f)
        {
            m_Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        move = IsoVectorConvert(move);

        m_Controller.Move(move * Speed * Time.deltaTime);

        m_Velocity.y += Physics.gravity.y * Time.deltaTime;
        m_Controller.Move(m_Velocity * Time.deltaTime);
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0f, 45f, 0f);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }
}
