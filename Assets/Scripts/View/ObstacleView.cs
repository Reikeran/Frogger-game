using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    private Vector3 moveDirection;
    public float speed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
        if(spriteRenderer != null) { spriteRenderer.flipY = moveDirection.x <0; }
    }
    public void SetSpeed(float velocity)
    {
        speed = velocity;
    }
    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public virtual void Move()
    {
        transform.position += moveDirection * Time.deltaTime * speed;
    }
}
