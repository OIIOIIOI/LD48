using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager BuildingManagerInstance;
    public enum BuildingType
    {
        Lab,
        HarpoonCenter,
        ExpeditionCenter,
        HousesLeft,
        HouseRight
    }

    // Building gameObject
    public GameObject laboratory;
    public GameObject harpoonCenter;
    public GameObject expeditionCenter;
    public GameObject housesLeft;
    public GameObject houseRight;
    
    // PlaceHolder gameObject
    public GameObject left;
    public GameObject right;
    public GameObject bottom;
    public GameObject bottomLeft;
    public GameObject bottomRight;
    
    private void Awake()
    {
        if (BuildingManagerInstance == null)
            BuildingManagerInstance = this;
        else if (BuildingManagerInstance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {

    }
    
    /* Fall action functions */
    // use harpon center
    
    // Prepare expedition
    
    // Analyze relic
    
    // damage building
    public void DamageBuilding(BuildingType type)
    {
        
    }
    
    /* Stase actions functions */
    // Build
    
    // repair
    
    // launch expedition
    
    /* Building getters */
}
