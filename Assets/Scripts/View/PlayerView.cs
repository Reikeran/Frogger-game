using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public AudioClip stepclip;

    private SpriteRenderer spriteRenderer;
    private PlayerPresenter presenter;

    public void Init(PlayerPresenter presenter)
    {
        this.presenter = presenter;
    }

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 input = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) input = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.S)) input = Vector3.down;
        else if (Input.GetKeyDown(KeyCode.A)) input = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D)) input = Vector3.right;

        if (input != Vector3.zero && presenter != null)
        {
            presenter.OnInput(input);
        }
    }

    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void UpdateSprite(MoveDir dir)
    {
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
