using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    private void Update()
    {
        var input = new Vector2();
        input += Input.GetKey(KeyCode.W) ? Vector2.up : Vector2.zero;
        input += Input.GetKey(KeyCode.S) ? Vector2.down : Vector2.zero;
        input += Input.GetKey(KeyCode.A) ? Vector2.left : Vector2.zero;
        input += Input.GetKey(KeyCode.D) ? Vector2.right : Vector2.zero;

        input.Normalize();
        var moveDirection = new Vector3(input.x, 0, input.y);

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
    }
}
