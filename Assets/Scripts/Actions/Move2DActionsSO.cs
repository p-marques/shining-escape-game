using UnityEngine;

[CreateAssetMenu(fileName = "Move2DActionsSO", menuName = "Game/Actions/Move 2D Actions")]
public class Move2DActionsSO : ScriptableObject
{
    public void Move(ShiningCharacterController character, float horizontalAxis, 
        float speed, float drag)
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

        character.Rigidbody.velocity = currentVelocity;
    }
}
