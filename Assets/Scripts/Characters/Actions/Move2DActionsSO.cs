using UnityEngine;

[CreateAssetMenu(fileName = "Move2DActionsSO", menuName = "Game/Actions/Move 2D Actions")]
public class Move2DActionsSO : ScriptableObject
{
    public void Move(ICharacter character, float horizontalAxis, 
        float speed, float drag, float dampener = 1f)
    {
        Vector2 currentVelocity = character.Rigidbody.velocity;

        if (Mathf.Abs(horizontalAxis) > 0.01f)
        {
            currentVelocity.x = horizontalAxis * speed;
        }
        else
        {
            currentVelocity.x *= (1.0f - drag);
        }

        currentVelocity.x *= dampener;

        character.Rigidbody.velocity = currentVelocity;
    }

    public void Jump(ICharacter character, float speed)
    {
        Vector2 currentVelocity = character.Rigidbody.velocity;

        currentVelocity.y = speed;

        character.Rigidbody.velocity = currentVelocity;
    }
}
