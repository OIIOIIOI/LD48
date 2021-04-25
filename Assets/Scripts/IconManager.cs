using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public enum IconsType
    {
        Population,
        Materials,
        Knowledge,
        Relic
    }
    public Sprite[] icons;
    // Start is called before the first frame update

    public void SetIconType(IconsType iconType)
    {
        /*switch (iconType)
        {
            //case 
        }*/
    }
}
