using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class OnMouseDisplay : SingletonBehaviour<OnMouseDisplay>
{
    public List<Color> Colors; 
    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Colors[1];
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Colors[0];
    }


}
