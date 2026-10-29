using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager instance{ get; set; }
    public List<GameObject> unitsSelected=new List<GameObject>();
    public List<GameObject> allUnitsList=new List<GameObject>();

    public LayerMask clickableLayer;
    public LayerMask groundLayer;
    public LayerMask attackableLayer;

    public GameObject groundMarker;
    private Camera camera;

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

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit, Mathf.Infinity,clickableLayer))
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectSingleUnit(hit.collider.gameObject);
                }
            }
            else
            {
                if(!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectUnits();
                }
            }
        }

        if(Input.GetMouseButtonDown(1)&&unitsSelected.Count>0)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit, Mathf.Infinity,attackableLayer))
            {
                if(hit.transform.CompareTag("Enemy"))
                {
                    bool flag=false;
                    foreach(GameObject unit in unitsSelected)
                    {
                        if(unit.GetComponent<AttackController>())
                        {
                            unit.GetComponent<AttackController>().attackTarget = hit.transform;
                            flag=true;
                        }
                    }
                    if(flag)
                    { 
                        ChangeAttackCursor();
                    }
                }
            }
            else if(Physics.Raycast(ray,out hit, Mathf.Infinity,groundLayer))
            {
                foreach(GameObject unit in unitsSelected)
                {
                    if(unit.GetComponent<AttackController>())
                    {
                        unit.GetComponent<AttackController>().attackTarget = null;
                    }
                }

                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }

        }
    }

    private void ChangeAttackCursor()
    {
        
    }

    public void DeselectUnits()
    {
        foreach(GameObject unit in unitsSelected)
        {
            EnableUnitMovement(unit,false);
            TriggerSelctionIndicator(unit,false);
        }
        unitsSelected.Clear();
        groundMarker.SetActive(false);
    }

    private void MultiSelect(GameObject unit)
    {
        if(unitsSelected.Contains(unit))
        {
            unitsSelected.Remove(unit);
            EnableUnitMovement(unit,false);
            TriggerSelctionIndicator(unit,false);
        }
        else
        {
            unitsSelected.Add(unit);
            EnableUnitMovement(unit,true);
            TriggerSelctionIndicator(unit,true);
        }
    }

    private void SelectSingleUnit(GameObject unit)
    {
        DeselectUnits();
        unitsSelected.Add(unit);
        EnableUnitMovement(unit,true);
        TriggerSelctionIndicator(unit,true);
    }

    public void DragSelect(GameObject unit)
    {
        unitsSelected.Add(unit);
        EnableUnitMovement(unit,true);
        TriggerSelctionIndicator(unit,true);
    }

    private void EnableUnitMovement(GameObject unit,bool trigger)
    {
        unit.GetComponent<UnitMovement>().enabled = trigger;
    }

    private void TriggerSelctionIndicator(GameObject unit,bool isVisible)
    {
        unit.transform.Find("Indicator").gameObject.SetActive(isVisible);
    }
}
