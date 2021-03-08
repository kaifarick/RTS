using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSelect : MonoBehaviour
{
    public bool isSelected;
    private float selX;
    private float selY;
    private float selWidth;
    private float selHeight;

    float selXold;
    float selYold;

    public Texture2D texture;

    Vector3 startPoint;
    Vector3 endPoint;
    Ray ray;
    RaycastHit hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSelected = true;
            selXold = Input.mousePosition.x;
            selYold = Input.mousePosition.y;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                startPoint = hit.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.Instance.GroupSelected.Clear();

            isSelected = false;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit,100000f))
            {
                endPoint = hit.point;
            }

            FindSelect();
        }

        if (isSelected)
        {
            selX = Input.mousePosition.x;
            selY = Screen.height - Input.mousePosition.y;
            selWidth = selXold - Input.mousePosition.x;
            selHeight = Input.mousePosition.y - selYold;
        }        
    }

    private void OnGUI()
    {
        if (isSelected)
        {
            GUI.DrawTexture(new Rect(selX, selY, selWidth, selHeight), texture);
        }
    }

    void FindSelect()
    {
        foreach(GameObject unit in GameManager.Instance.AllFriendUnits)
        {
            float x = unit.transform.position.x;
            float z = unit.transform.position.z;
            if(x>startPoint.x && x<endPoint.x || x<startPoint.x && x> endPoint.x)
            {
                if(z>startPoint.z && z<endPoint.z || z < startPoint.z && z > endPoint.z)
                {
                    //GameManager.Instance.GroupSelected.Add(unit);
                    //switch (unit.tag)
                    //{
                    //    case "HammerFriendly":
                    //        unit.GetComponent<HammerFriend>().Marker.SetActive(true);
                    //        break;
                    //    case "CrossbowFriendly":
                    //        unit.GetComponent<CrossbowFriendly>().Marker.SetActive(true);
                    //        break;
                    //    case "HammerEnemy":
                    //        unit.GetComponent<HammerEnemy>().Marker.SetActive(true);
                    //        break;
                    //    case "CrossbowEnemy":
                    //        unit.GetComponent<CrossbowEnemy>().Marker.SetActive(true);
                    //        break;
                   // }
                }
            }
        }
    }    
}
