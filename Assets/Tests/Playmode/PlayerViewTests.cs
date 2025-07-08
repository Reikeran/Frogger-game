using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerViewTests
{
    private GameObject go;
    private PlayerView playerView;
    private SpriteRenderer spriteRenderer;
    private Sprite up, down, left, right;

    [SetUp]
    public void Setup()
    {
        go = new GameObject("PlayerView");

        spriteRenderer = go.AddComponent<SpriteRenderer>();
        playerView = go.AddComponent<PlayerView>();
        playerView.Awake();


        up = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 1, 1), Vector2.zero);
        down = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
        left = Sprite.Create(Texture2D.redTexture, new Rect(0, 0, 1, 1), Vector2.zero);
        right = Sprite.Create(Texture2D.grayTexture, new Rect(0, 0, 1, 1), Vector2.zero);

        playerView.upSprite = up;
        playerView.downSprite = down;
        playerView.leftSprite = left;
        playerView.rightSprite = right;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(go);
    }

    [Test]
    public void UpdateSprite_UpDirection_SetsUpSprite()
    {
      
        playerView.UpdateSprite(MoveDir.Up);


        Assert.AreEqual(up, spriteRenderer.sprite);
    }

    [Test]
    public void UpdateSprite_DownDirection_SetsDownSprite()
    {
        playerView.UpdateSprite(MoveDir.Down);
        Assert.AreEqual(down, spriteRenderer.sprite);
    }

    [Test]
    public void UpdateSprite_LeftDirection_SetsLeftSprite()
    {
        playerView.UpdateSprite(MoveDir.Left);
        Assert.AreEqual(left, spriteRenderer.sprite);
    }

    [Test]
    public void UpdateSprite_RightDirection_SetsRightSprite()
    {
        playerView.UpdateSprite(MoveDir.Right);
        Assert.AreEqual(right, spriteRenderer.sprite);
    }
}
