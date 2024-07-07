using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public Vector2 startPosition;
    public SpriteRenderer barSpriteRenderer;

    public void UpdateBarTransform(Vector2 endPosition)
    {
        transform.position = (startPosition + endPosition)/ 2;
        Vector2 direction = endPosition - startPosition;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        float length = direction.magnitude;
        barSpriteRenderer.size = new Vector2(length, barSpriteRenderer.size.y);
    }
}
