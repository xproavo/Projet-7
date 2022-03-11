using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infiniteVertical;
    [SerializeField] private bool infiniteHorizontal;

    private Transform CameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    private void Start()
    {
        CameraTransform = Camera.main.transform;
        lastCameraPosition = CameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;

    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = CameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.x);
        lastCameraPosition = CameraTransform.position;

        if (infiniteHorizontal) 
        { 
            if (Mathf.Abs(CameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPositionX = (CameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(CameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }
        if (infiniteVertical)
        {
            if (Mathf.Abs(CameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetPositionY = (CameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, CameraTransform.position.y + offsetPositionY);
            }
        }
            
    }
}
