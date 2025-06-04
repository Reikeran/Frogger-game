using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    private Vector3 moveDirection;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
        if(spriteRenderer != null) { spriteRenderer.flipY = moveDirection.x <0; }
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public void Move()
    {
        transform.position += moveDirection * Time.deltaTime * speed;
    }
}
