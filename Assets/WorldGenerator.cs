using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Versioning;
using UnityEngine;

public enum ObjectTypes
{
    building,
    tree
}

[ExecuteInEditMode]
public class WorldGenerator : MonoBehaviour
{
    [Range(0, 100)]
    public int length;
    [Range(0, 10)]
    public int gridX;
    [Range(0, 10)]
    public int gridZ;
    public bool hasGenerated = false;
    public GameObject [] objectList;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasGenerated || objectList[0] == null)
            return;
        else
        {
            var size = transform.childCount;
            for (int i = 0; i < size; i++)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    if (Random.Range(0, 100) < 3 || col % 5 == 0 || row % 3 == 0)
                    {
                        continue;
                    }

                    var structureTypeDecider = Random.Range(0, 100);
                    ObjectTypes structureType;

                    if (structureTypeDecider < 75)
                        structureType = ObjectTypes.building;
                    else
                        structureType = ObjectTypes.tree;

                        Vector3 pos = new Vector3(gridX * row, 0.5f, gridZ * col);
                        Quaternion rot = Quaternion.Euler(0, 0, 0);

                    //current.transform.localScale = new Vector3(1, Random.Range(1.0f, 5.0f), 1);
                    //height = mesh.bounds.extents.y * current.transform.localScale.y;
                    //current.transform.position = current.transform.position + new Vector3(0, height, 0);
                    if (structureType == ObjectTypes.building)
                    {
                        GameObject current = Instantiate(objectList[0], pos, rot, transform);
                        Mesh mesh = current.GetComponent<MeshFilter>().sharedMesh;

                        int blockHeight = Random.Range(0, 5);
                        print(blockHeight);
                        for (int i = 0; i < blockHeight; i++)
                        {
                            var onTop = Instantiate(objectList[0], pos + new Vector3(0, 1 * i, 0), rot, current.transform);
                        }
                    }
                    else if (structureType == ObjectTypes.tree)
                    {
                        GameObject current = Instantiate(objectList[Random.Range(1,5)], pos, rot, transform);
                        Mesh mesh = current.GetComponent<MeshFilter>().sharedMesh;
                    }
                }
            }
            hasGenerated = true;
        }
    }
}


