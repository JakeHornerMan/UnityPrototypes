using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject highlight;

    public void SetColor(bool isOffset){
        spriteRenderer.color = isOffset ? baseColor : offsetColor;
    }

    void OnMouseEnter(){
        highlight.SetActive(true);
    }

    void OnMouseExit(){
        highlight.SetActive(false);
    }
}
