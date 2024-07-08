using UnityEngine;

public class RandomizePixels : MonoBehaviour
{
    public Texture2D sourceTexture; 
    void Start()
    {
        if (sourceTexture == null)
        {
            Debug.LogError("Source Texture is not assigned!");
            return;
        }

        Debug.Log("Source Texture assigned successfully!");

        // Create a new Texture2D with the same dimensions as the source texture
        Texture2D randomizedTexture = new Texture2D(sourceTexture.width, sourceTexture.height);

        // Get the pixels from the source texture
        Color[] pixels = sourceTexture.GetPixels();
        Debug.Log("Pixels retrieved from source texture.");

        // Shuffle the pixels array
        for (int i = pixels.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Color temp = pixels[i];
            pixels[i] = pixels[j];
            pixels[j] = temp;
        }
        Debug.Log("Pixels shuffled.");

        // Set the shuffled pixels to the new texture
        randomizedTexture.SetPixels(pixels);
        randomizedTexture.Apply();
        Debug.Log("Randomized texture created and applied.");

        // Assign the new texture to the SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found!");
            return;
        }

        spriteRenderer.sprite = Sprite.Create(randomizedTexture, new Rect(0, 0, randomizedTexture.width, randomizedTexture.height), new Vector2(0.5f, 0.5f));
        Debug.Log("Sprite created and assigned to SpriteRenderer.");
    }
}
