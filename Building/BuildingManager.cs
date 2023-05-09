using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{

    public InventoryMenuManager menuManager;

    public Brick brickPrefab;
    
    public Transform placedBrickParent;

    protected Brick currentlyHeldBrick;
    
    private bool positionIsOK = false;

    private ToolMode toolMode;

    private void Start(){
        currentlyHeldBrick = Instantiate(brickPrefab,placedBrickParent);
        currentlyHeldBrick.brickCollider.enabled = false;
        toolMode = ToolMode.Build;
    }

    private void OnEnable() => menuManager.StartListeningForClick(GetNewBrickTypeData);

    private void OnDisable() => menuManager.StopListeningForClick(GetNewBrickTypeData);

    private void Update()
    {
        //Check if mouse is over UI
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }

        //Build functionality
        if(toolMode == ToolMode.Build){
            if(currentlyHeldBrick != null){

                var mouseWorldPosition = GetMouseWorldPosition();
                var nearestPointOnGrid = GridLogic.FindNearestPointOnGrid(mouseWorldPosition);
                Vector3 placePosition = nearestPointOnGrid;
                for(int i = 0; i < 10; i++){
                    var centerOfBrickAtPlacementPosition = placePosition + currentlyHeldBrick.transform.rotation * currentlyHeldBrick.brickCollider.center;
                    var colliders = Physics.OverlapBox(centerOfBrickAtPlacementPosition,
                                                        currentlyHeldBrick.brickCollider.size/2f,
                                                        currentlyHeldBrick.transform.rotation,
                                                        LayerMask.GetMask("Lego"));

                    positionIsOK = colliders.Length == 0;

                    if(positionIsOK) break;
                    else placePosition.y += GridLogic.Grid.y;
                }

                if(positionIsOK) currentlyHeldBrick.transform.position = placePosition;
                else currentlyHeldBrick.transform.position = nearestPointOnGrid;

            }

            if(Input.GetMouseButtonDown(0)){
                var rotation = currentlyHeldBrick.transform.rotation;
                currentlyHeldBrick.brickCollider.enabled = true;
                currentlyHeldBrick = null;
                currentlyHeldBrick = Instantiate(brickPrefab,placedBrickParent);
                currentlyHeldBrick.transform.rotation = rotation;
                currentlyHeldBrick.brickCollider.enabled = false;
            }

            if(Input.GetKeyDown(KeyCode.R)){
                currentlyHeldBrick.transform.Rotate(Vector3.up,90);
            }

        }

        //Destroy functionality
        if(toolMode == ToolMode.Destroy){
            if(Input.GetMouseButtonDown(0)){
                var mouseScreenPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Lego"))){
                    var objectToDelete = hit.collider.gameObject;
                    Destroy(objectToDelete);
                }
            }
        }

        //Swap Tool Mode
        if(Input.GetKeyDown(KeyCode.Q)){
            //Swap states
            if(toolMode == ToolMode.Build) 
            {
                toolMode = ToolMode.Destroy;
                
                //Remove currently placeable brick
                Destroy(currentlyHeldBrick.gameObject);
                currentlyHeldBrick = null;
                //Activate destroy brick code
            }
            else if(toolMode == ToolMode.Destroy) 
            {
                toolMode = ToolMode.Build;

                //Spawn a placeable brick
                currentlyHeldBrick = Instantiate(brickPrefab,placedBrickParent);
                currentlyHeldBrick.brickCollider.enabled = false;
                //Activate place code
            }
        }

    }

    //Get mouse world position using raycast
    private static Vector3 GetMouseWorldPosition()
    {
        var mouseScreenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;

        Vector3 mouseWorldPosition = new Vector3(int.MaxValue,int.MaxValue,z: int.MaxValue);

        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Lego")))
        {
            mouseWorldPosition = hit.point;
            return mouseWorldPosition;
        }

        return mouseWorldPosition;
    }

    //Method called OnInventoryMenuSelection
    private void GetNewBrickTypeData(InventoryItem itemData){
        brickPrefab = itemData.BrickPrefab;
        Destroy(currentlyHeldBrick.gameObject);
        currentlyHeldBrick = null;

        currentlyHeldBrick = Instantiate(brickPrefab,placedBrickParent);
        currentlyHeldBrick.brickCollider.enabled = false;
    }
}

public enum ToolMode{
    Build,
    Destroy
}