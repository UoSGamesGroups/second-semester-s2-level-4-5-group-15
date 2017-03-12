using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private bool fade_in_;
    [SerializeField] private float fade_time_;

    private const byte kOpacityMax = 255;
    private const byte kOpacityMin = 0;
    private const byte kOpacityStep = 5;

    private bool change_scene_;
    private string scene_name_;

    private IEnumerator coroutine;

    private Color32 draw_colour_;

    private enum FadeDirection { kIn, kOut };
    private Texture texture_;

    private static Texture2D create_texture(int width, int height, Color32 colour)
    {
        var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

        Debug.Log("Height: " + texture.height);

        Debug.Log("Width: " + texture.width);

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texture.SetPixel(x, y, colour);
            }
        }

        texture.Apply();

        return texture;
    }

    private IEnumerator fade(FadeDirection fade_direction)
    {
        float wait_time = fade_time_ / (kOpacityMax / kOpacityStep);
        switch (fade_direction)
        {
            case FadeDirection.kIn:
                for (byte i = kOpacityMax; i > kOpacityMin; i -= kOpacityStep)
                {
                    draw_colour_.a = i;
                    yield return new WaitForSeconds(wait_time);
                }
                break;
            case FadeDirection.kOut:
                for (byte i = kOpacityMin; i < kOpacityMax; i += kOpacityStep)
                {
                    draw_colour_.a = i;
                    yield return new WaitForSeconds(wait_time);
                }
                break;
        }
        if (change_scene_)
        {
            SceneManager.LoadScene(scene_name_, LoadSceneMode.Single);
        }
    }

    private void OnGUI()
    {
        GUI.color = draw_colour_;
        GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), texture_);
    }

    private void Start()
    {
        texture_ = create_texture(1, 1, Color.black);

        if (fade_in_)
        {
            coroutine = fade(FadeDirection.kIn);
            StartCoroutine(coroutine);
        }
    }

    public void transition_to(string scene_name)
    {
        StopCoroutine(coroutine);
        scene_name_ = scene_name;
        change_scene_ = true;
        coroutine = fade(FadeDirection.kOut);
        StartCoroutine(coroutine);
    }

}
