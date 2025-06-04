using UnityEngine;


public class Playermodel
{
    public Vector3 Position { get; private set; }
    private Vector3 minBounds;
    private Vector3 maxBounds;
    public MoveDir LastDirection { get; private set; } = MoveDir.Up;

    public Playermodel(Vector3 startPos, Vector3 minBounds, Vector3 maxBounds)
    {
        this.Position = startPos;
        this.minBounds = minBounds;
        this.maxBounds = maxBounds;
    }
    public void Move(Vector3 direction)
    {
        Position += direction;
        Position = new Vector3(
            Mathf.Clamp(Position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(Position.y, minBounds.y, maxBounds.y),
            Position.z);
    }
    public void UpdateDirection(Vector3 input)
    {
        if (input.x > 0) LastDirection = MoveDir.Right;
        else if (input.x < 0) LastDirection = MoveDir.Left;
        else if (input.y > 0) LastDirection = MoveDir.Up;
        else if (input.y < 0) LastDirection = MoveDir.Down;
    }
    public bool CheckWin()
    {
        if (Position.y == maxBounds.y)
        {
            return true;
        }
        else { return false; }
    }
}

