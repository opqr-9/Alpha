using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionBox : MonoBehaviour
{
    Camera camera;
    public RectTransform boxRect;
    Rect selectionBox;

    Vector2 startPos;
    Vector2 endPos;

    private void Start()
    {
        camera = Camera.main;
        startPos=endPos=Vector2.zero;
        boxRect.sizeDelta = Vector2.zero;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            if(startPos.x-endPos.x!=0 && startPos.y-endPos.y!=0)
            {
                UpdateSelectionBox();
                SelectUnits();
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            boxRect.sizeDelta = Vector2.zero;
        }
    }

    private void UpdateSelectionBox()
    {
        boxRect.position = (startPos + endPos) / 2;
        boxRect.sizeDelta=new Vector2(Mathf.Abs(startPos.x-endPos.x),Mathf.Abs(startPos.y-endPos.y));
    }

    private void SelectUnits()
    {
        selectionBox = new Rect(Mathf.Min(startPos.x,endPos.x),Mathf.Min(startPos.y,endPos.y),Mathf.Abs(startPos.x-endPos.x),Mathf.Abs(startPos.y-endPos.y));
        UnitSelectionManager.instance.DeselectUnits();
        foreach(GameObject unit in UnitSelectionManager.instance.allUnitsList)
        {
            if(selectionBox.Contains(camera.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelectionManager.instance.DragSelect(unit);
            }
        }
    }   
}
