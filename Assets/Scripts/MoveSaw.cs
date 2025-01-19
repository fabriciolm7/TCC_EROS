using System;
using UnityEngine;

public class MoveSaw : MonoBehaviour
{
    public float speed = 5f;
    public float moveTime = 2f;

    private enum Direction
    {
        Right,
        Left
    }

    private Direction currentDirection = Direction.Right;
    private float timer;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);
    }

    void Update()
    {
        MoveSawInDirection(Vector2.right, currentDirection == Direction.Right);
        MoveSawInDirection(Vector2.left, currentDirection == Direction.Left);

        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            ToggleDirection();
            timer = 0f;
        }
    }

    void MoveSawInDirection(Vector2 direction, bool shouldMove)
    {
        if (shouldMove)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void ToggleDirection()
    {
        currentDirection = (Direction)(((int)currentDirection + 1) % Enum.GetValues(typeof(Direction)).Length);
    }
}
