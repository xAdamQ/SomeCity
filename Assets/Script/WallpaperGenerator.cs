using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WallpaperGenerator : MonoBehaviour
{
    public static WallpaperGenerator I;

    public Sprite TransetionSprite, ShiftSprite;

    public WallpaperProfile CurrentProfile;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        GO();
    }

    float MixTime;
    public void GO()
    {
        MixTime = CurrentProfile.MixTime;

        StopAllCoroutines();

        switch (CurrentProfile.WallpaperType)
        {

            case WallpaperType.Transetion:
                StartCoroutine(ColorTransetion());
                break;

            case WallpaperType.Shift:
                StartCoroutine(ShiftColors());
                break;

        }
    }

    public IEnumerator ColorTransetion()
    {
        GetComponent<Image>().sprite = TransetionSprite;
        var texture2D = GetComponent<Image>().sprite.texture;

        var up = new Color();
        var down = new Color();

        var rate = 1f / Resolution;
        var palette = CurrentProfile.Palette;

        for (int h = 0; h < Resolution; h++)
        {
            texture2D.SetPixel(0, h, palette[0]);
        }
        texture2D.Apply();

        // start with first color
        var paletteCounter = 0;
        while (true)
        {
            var UpColorInd = paletteCounter % (palette.Length);
            var DownColorInd = (paletteCounter + 1) % (palette.Length);
            paletteCounter++;

            up = palette[UpColorInd];
            down = palette[DownColorInd];

            for (int i = 0; i < Resolution; i++)
            {
                for (int e = Resolution - 1; e > 0; e--)
                {
                    texture2D.SetPixel(0, e, texture2D.GetPixel(0, e - 1));
                }// shift colors up

                var current = Color.Lerp(up, down, i * rate);

                texture2D.SetPixel(0, 0, current);

                texture2D.Apply();
                yield return new WaitForSeconds(MixTime / Resolution);
            }

        }
    }

    public int Resolution = 128; // in case of shift mode, it's the unit of change
    public IEnumerator ShiftColors()
    {
        var AnimeHT = new Hashtable();
        AnimeHT.Add("from", Color.white);
        AnimeHT.Add("to", Color.black);
        AnimeHT.Add("time", MixTime);
        AnimeHT.Add("onupdatetarget", gameObject);
        AnimeHT.Add("onupdate", "ChangeColor");
        AnimeHT.Add("easetype", iTween.EaseType.linear);

        var texture2D = ShiftSprite.texture;
        GetComponent<Image>().sprite = ShiftSprite; //deafelt on image is transetion

        var palette = CurrentProfile.Palette;
        var rate = 1f / Resolution;
        var paletteCounter = 0;
        var changeReselution = Resolution * 3;

        while (true)
        {
            var UpColorInd = paletteCounter % (palette.Length);
            var DownColorInd = (paletteCounter + 1) % (palette.Length);
            paletteCounter++;

            var up = palette[UpColorInd];
            var down = palette[DownColorInd];

            AnimeHT["from"] = up;
            AnimeHT["to"] = down;

            iTween.ValueTo(gameObject, AnimeHT);

            yield return new WaitForSeconds(MixTime);
        }
    }

    public void ChangeColor(Color value)
    {
        GetComponent<Image>().color = value;
    }


}
