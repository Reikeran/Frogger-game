using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public AudioClip stepclip;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private SpriteRenderer spriteRenderer;
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void UpdateSprite(MoveDir dir)
    {
        SoundManager.Instance.PlaySFX(stepclip);
        switch (dir)
        {
            case MoveDir.Up:
                spriteRenderer.sprite = upSprite;
                break;
            case MoveDir.Down:
                spriteRenderer.sprite = downSprite;
                break;
            case MoveDir.Left:
                spriteRenderer.sprite = leftSprite;
                break;
            case MoveDir.Right:
                spriteRenderer.sprite = rightSprite;
                break;
        }
    }
}
