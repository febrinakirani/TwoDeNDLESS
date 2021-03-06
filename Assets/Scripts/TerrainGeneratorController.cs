using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneratorController : MonoBehaviour
{
   [Header("Templates")]
   public List<TerrainTemplateController> terrainTemplates;
   public float terrainTemplateWidth;

   [Header("Generator Area")]
   public Camera gameCamera;
   public float areaStartOffset;
   public float areaEndOffset;

   private List<GameObject> spawnedTerrain;

   private float lastGeneratedPositionX;

   private const float debugLineHeight = 10.0f;

    private void Start()
    {
        spawnedTerrain = new List<GameObject>();
        while (lastGeneratedPositionX < GetHorizontalPositionEnd())
        {
            GenerateTerrain(lastGeneratedPositionX); 
            //generate
            lastGeneratedPositionX += terrainTemplateWidth;
           
        }
    }

    // Update is called once per frame
    private void Update()
    {
         while (lastGeneratedPositionX < GetHorizontalPositionEnd())
        {
            GenerateTerrain(lastGeneratedPositionX); 
            lastGeneratedPositionX += terrainTemplateWidth;
            
        }
    }
    private float GetHorizontalPositionStart()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2 (0f, 0f)).x + areaStartOffset;
    }

    private float GetHorizontalPositionEnd()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2 (1f, 0f)).x + areaEndOffset;
    }
    private void GenerateTerrain()
    {
       GameObject newTerrain = Instantiate(terrainTemplates[Random.Range(0, terrainTemplates.Count)].gameobject, transform);
       newTerrain.transform.position = new Vector2(posX, 0f);
       spawnedTerrain.Add(newTerrain);
    
    }
    private void OnDrawGizimos()
    {
        Vector3 areaStarPosition = transform.position;
        Vector3 areaEndPosition = transform.position;

        areaStarPosition.x = GetHorizontalPositionStart();
        areaEndPosition.x = GetHorizontalPositionEnd();

         Debug.DrawLine(areaStarPosition + Vector3.up * debugLineHeight / 2, areaStarPosition + Vector3.down * debugLineHeight/2, Color.red);
         Debug.DrawLine(areaEndPosition + Vector3.up * debugLineHeight / 2, areaEndPosition + Vector3.down * debugLineHeight/2, Color.red);
        }
}
