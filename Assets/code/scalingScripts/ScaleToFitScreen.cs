using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{
    private SpriteRenderer sr;
    float worldScreenHeight;
    float worldScreenWidth;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // world height is always camera's orthographicSize * 2
        worldScreenHeight = Camera.main.orthographicSize * 2;
        Debug.Log("camera size is:" +  Camera.main.orthographicSize);

        // world width is calculated by diving world height with screen heigh
        // then multiplying it with screen width
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // to scale the game object we divide the world screen width with the
        // size x of the sprite, and we divide the world screen height with the
        // size y of the sprite

        transform.localScale = new Vector3((worldScreenWidth / sr.sprite.bounds.size.x), (worldScreenHeight / sr.sprite.bounds.size.y), 1);
        Debug.Log("sprite size is: " + sr.sprite.bounds.size);
    }

    public float getScaleMultiplierX()
    {
        return worldScreenWidth / sr.sprite.bounds.size.x;
    }
    public float getScaleMultiplierY()
    {
        return worldScreenHeight / sr.sprite.bounds.size.y;
    }

} // class
