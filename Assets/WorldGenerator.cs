using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Versioning;
using UnityEngine;

[ExecuteInEditMode]
public class WorldGenerator : MonoBehaviour
{
    [Range(0, 100)]
    public int length;
    [Range(0, 100)]
    public int gridX;
    [Range(0, 100)]
    public int gridZ;
    public float treeSpreadX = 1;
    public float treeSpreadY = 0;
    public float treeSpreadZ = 1;

    public Transform GeneratorParent;

    public bool hasGenerated = false;

    public GameObject[] trees;
    public enum ObjectTypes
    {
        building,
        tree
    }
    // Start is called before the first frame update

    GameObject myCube;
    void Start()
    {
         myCube = Resources.Load("Cube") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (hasGenerated || myCube==null)
            return;
        else
        {
            var size = GeneratorParent.childCount;
            for (int i = 0; i <size; i++)
            {
                DestroyImmediate(GeneratorParent.GetChild(0).gameObject);
            }

            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    Vector3 pos = GeneratorParent.transform.position+ new Vector3(gridX * row, 0, gridZ * col);
                    Quaternion rot = Quaternion.Euler(0, 0, 0);

                    int structureTypeDecider = Random.Range(0, 100);
                    ObjectTypes structureType;

                    if (structureTypeDecider < 80)
                        structureType = ObjectTypes.building;
                    else
                        structureType = ObjectTypes.tree;

                    if (Random.Range(0, 100) < 3 || col % 5 == 0 || row % 3 == 0)
                    {
                        continue;
                    }

                    else if (structureType == ObjectTypes.building)
                    {
                        GameObject current = Instantiate(myCube, pos, rot, GeneratorParent);
                        Mesh mesh = current.GetComponent<MeshFilter>().sharedMesh;
                        int structNum = Random.Range(0, 5);

                        float height = mesh.bounds.extents.y * current.transform.localScale.y;
                        current.transform.position = current.transform.position + new Vector3(0, height, 0);
                        pos = current.transform.position;
                        for (int i = 0; i < structNum; i++)
                        {
                            var onTop = Instantiate(myCube, pos + new Vector3(0, current.transform.localScale.y * i, 0), rot, current.transform);
                            onTop.transform.localScale = new Vector3(1, 1, 1);
                        }
                    }

                    else
                    {
                        int structNum = Random.Range(1, 5);
                        for (int i = 0; i < structNum; i++)
                        {
                            Vector3 treeSpread3 = new Vector3(Random.Range(-treeSpreadX, treeSpreadX), Random.Range(-treeSpreadY, treeSpreadY), Random.Range(-treeSpreadZ, treeSpreadZ));
                            pos += treeSpread3;
                            rot = Quaternion.Euler(0, Random.Range(0,360), 0);
                            GameObject current = Instantiate(trees[Random.Range(0, trees.Length)], pos, rot, GeneratorParent);
                        }

                    }
                }
            }
            hasGenerated = true;
        }
    }
}
