using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    public Sprite[] hatSprites;
    private string[] hatNames = new string[] { "TopHat", "Fez", "Fedora", "Pineapple", "Bow", "Horn", "Eyemask", "Bunny Ears", "Poo", "Ice Cream", "Pirate", "Jester", "Riddler", "Crown", "Mario" };
    private float[] sizeMultpliers = new float[] {0.8f, 0.6f, 0.9f, 0.7f, 0.8f, 0.9f, 0.6f, 0.8f, 0.7f, 0.9f, 0.8f, 0.8f, 0.9f, 0.8f, 0.8f};
    private float[] xTranslations = new float[] { -0.1f, -0.1f, -0.1f, -0.1f, -0.3f, 0.2f, -0.1f, -0.08f, -0.11f, -0.1f, -0.1f, -0.13f, -0.1f, -0.07f, -0.07f};
    private float[] yTranslations = new float[] { 0.6f, 0.5f, 0.5f, 0.6f, 0.35f, 0.6f, 0.15f, 0.4f, 0.56f, 0.5f, 0.55f, 0.55f, 0.45f, 0.45f, 0.45f};

    string currentHatName;
    int index;

    void Start()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        currentHatName = PlayerPrefs.GetString("PrefCurrentHat", "none");

        if(currentHatName == "none")
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            index = System.Array.IndexOf(hatNames, currentHatName);
            GetComponent<SpriteRenderer>().sprite = hatSprites[index];
        }

        Vector3 scale = new Vector3(sizeMultpliers[index], sizeMultpliers[index], sizeMultpliers[index]);

        gameObject.transform.localScale += scale;
        gameObject.transform.Translate(xTranslations[index], yTranslations[index], 0);
    }
}
