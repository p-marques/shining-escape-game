using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGraphicsRotator : MonoBehaviour
{
    private ShiningCharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponentInParent<ShiningCharacterController>();

        if (!_characterController)
            Debug.LogError("CharacterGraphicsRotator didn't find a chacacter in parent.");
    }

    private void Update()
    {
        if (!_characterController) return;

        float xVelocity = _characterController.Rigidbody.velocity.x;

        if (xVelocity < -0.5f)
        {
            if (transform.right.x > 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (xVelocity > 0.5f)
        {
            if (transform.right.x < 0)
                transform.rotation = Quaternion.identity;
        }
    }
}
