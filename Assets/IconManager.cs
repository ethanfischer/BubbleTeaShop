using UnityEngine;

public class IconManager : MonoBehaviour
{
    public Sprite RegularBobaSprite;
    public Sprite StrawberryBobaSprite;
    public Sprite BlueberryBobaSprite;
    public Sprite MangoBobaSprite;
    public Sprite IceSprite;
    public Sprite IceSprite2;
    public Sprite IceSprite3;
    public Sprite MilkSprite;
    public Sprite SugarSprite;
    public Sprite SugarSprite2;
    public Sprite SugarSprite3;
    public Sprite TeaSprite;
    public Sprite CheeseFoamSprite;
    public Sprite GrassJellySprite;
    public Sprite Blank;
    
    //singleton unity pattern
    private static IconManager _instance;
    public static IconManager Instance
    { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<IconManager>();
        }

        return _instance;
    } }



}
