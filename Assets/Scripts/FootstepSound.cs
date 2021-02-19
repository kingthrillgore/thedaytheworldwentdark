using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{

    public float height;
    public TerrainData mTerrainData;
    public int alphamapWidth;
    public int alphamapHeight;
    public float[,,] mSplatmapData;
    public int mNumTextures;

    AnimationEvents animEvents;

   


    // Start is called before the first frame update
    void Start()
    {
        mTerrainData = Terrain.activeTerrain.terrainData;
        alphamapWidth = mTerrainData.alphamapWidth;
        alphamapHeight = mTerrainData.alphamapHeight;
    
        mSplatmapData = mTerrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight);
        mNumTextures = mSplatmapData.Length / (alphamapWidth * alphamapHeight);

        animEvents = GameObject.Find("Arms Animation").GetComponent<AnimationEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray FeetRay = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(FeetRay, out hit, height)) {
            Debug.Log("Colliding, duh");

            int terrainIdx = GetActiveTerrainTextureIdx();
            //Debug.Log(terrainIdx);

            //Here is where you would add your sound calls based on the Switch/Case
            switch (terrainIdx)
            {
                case 0:
                    Debug.Log("Surface 0 (Sand/Beach)");
                    animEvents.FootstepEvent.setParameterByName("Surface", 1f);

                    break;
                case 1:
                    Debug.Log("Surface 1 (Grass)");
                    animEvents.FootstepEvent.setParameterByName("Surface", 0f);

                    break;
                case 2:
                    Debug.Log("Surface 2 (Not sure what)");

                    break;
                default:
                    Debug.Log("None");
                    Debug.Log("Terrain Identifier: " + terrainIdx);
                    break;
            }
        }
    }

    private Vector3 ConvertToSplatMapCoordinate(Vector3 playerPos)
    {
        Vector3 vecRet = new Vector3();
        Terrain ter = Terrain.activeTerrain;
        Vector3 terPosition = ter.transform.position;
        vecRet.x = ((playerPos.x - terPosition.x) / ter.terrainData.size.x) * ter.terrainData.alphamapWidth;
        vecRet.z = ((playerPos.z - terPosition.z) / ter.terrainData.size.z) * ter.terrainData.alphamapHeight;
        return vecRet;
    }

    int GetActiveTerrainTextureIdx()
    {
        //Vector3 playerPos = PlayerController.Instance.position;
        Vector3 playerPos = this.transform.position;
        Vector3 TerrainCord = ConvertToSplatMapCoordinate(playerPos);
        int ret = 0;
        float comp = 0f;
        for (int i = 0; i < mNumTextures; i++)
        {
            if (comp < mSplatmapData[(int)TerrainCord.z, (int)TerrainCord.x, i])
                ret = i;
        }
        return ret;
    }
}
