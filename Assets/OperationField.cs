using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationField : MonoBehaviour
{
    public static OperationField instance{ get; set; }
    
    private RectTransform rectTransform;

    public int col=4;
    public int row=4;

    public float buttonWidth=80;
    public float buttonHeight=80;

    public Button operationButton;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        rectTransform=GetComponent<RectTransform>();
        float width=rectTransform.rect.width;
        float height=rectTransform.rect.height;
        float widthGap=(width-row*buttonWidth)/(row+1);
        float heightGap=(height-row*buttonHeight)/(col+1);
        Vector2 tmpPos=new Vector2(widthGap,-heightGap);
        for(int y=0;y<col;y++)
        {
            for(int x=0;x<row;x++)
            {
                Button button=Instantiate(operationButton,transform);
                RectTransform buttonRectTransform=button.GetComponent<RectTransform>();
                buttonRectTransform.anchoredPosition=tmpPos;
                button.name="Button"+" "+(y+1)+"-"+(x+1);
                tmpPos.x+=widthGap+buttonWidth;
            }
            tmpPos.y-=heightGap+buttonHeight;
            tmpPos.x=widthGap;
        }
    }
}
