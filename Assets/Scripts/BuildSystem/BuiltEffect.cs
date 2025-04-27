using UnityEngine;

public class BuiltEffect : MonoBehaviour
{
    private float m_Height = 1f;
    private float m_Speed = 3f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, m_Height, transform.position.z);
        InvokeRepeating(nameof(ShowEffect), 0f, 0.01f);
    }

    private void ShowEffect()
    {
        transform.Translate(Vector3.down * m_Speed * Time.deltaTime);
        if(transform.position.y <= 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            CancelInvoke(nameof(ShowEffect));
            Destroy(this);
        }
    }
}
