using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;
    private CharacterController m_Controller;

    private void Awake()
    {
        m_Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        m_Controller.Move(move * Speed * Time.deltaTime);
    }
}
