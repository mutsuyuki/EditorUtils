using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshMaker))]
public class MeshMakerExecutor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GUILayout.BeginVertical("Box");
        {
            GUILayout.Label("指定した頂点数のメッシュを作る");
            if (GUILayout.Button("作成する")) {
                ((MeshMaker) target).CreateLargePointsMesh();
            }


            GUILayout.Space(16);
            GUILayout.Label("複数のUVを持ったメッシュを作る");
            if (GUILayout.Button("作成する")) {
                ((MeshMaker) target).CreateMultiUvMesh();
            }
        }
        GUILayout.EndVertical();
    }
}


[CreateAssetMenu(menuName = "ScriptableObject/MeshMaker", fileName = "MeshMaker")]
public class MeshMaker : ScriptableObject {
    [UnityEngine.Range(3, 1000000)]
    [SerializeField] private int verticesSize;
    [SerializeField] private DefaultAsset outDir;

    public void CreateLargePointsMesh() {
        var mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        var vertices = new List<Vector3>();
        for (float i = 0; i < verticesSize; i++) {
            vertices.Add(new Vector3(i / verticesSize, 0, 0.2f));
            vertices.Add(new Vector3(i / verticesSize, 0, -0.2f));
        }

        var indices = new List<int>();
        for (int i = 0; i < verticesSize - 1; i++) {
            indices.Add(i * 2);
            indices.Add(i * 2 + 2);
            indices.Add(i * 2 + 1);
            indices.Add(i * 2 + 1);
            indices.Add(i * 2 + 2);
            indices.Add(i * 2 + 3);
        }

        mesh.SetVertices(vertices);
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);

        var path = outDir ? AssetDatabase.GetAssetPath(outDir) : Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));
        AssetDatabase.CreateAsset(mesh, path + "/large_points_mesh.asset");
        AssetDatabase.SaveAssets();
    }


    public void CreateMultiUvMesh() {
        var mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        var vertices = new List<Vector3> {
            new Vector3(-1, -1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, -1, 0)
        };

        var indices = new List<int> {
            0, 1, 2,
            0, 2, 3
        };

        var uvs0 = new List<Vector3> {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 1, 1)
        };

        var uvs1 = new List<Vector3> {
            new Vector3(1, 1, 1),
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1),
            new Vector3(0, 0, 0)
        };
        
        
        var uvs4 = new List<Vector3> {
            new Vector3(1, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0)
        };

        mesh.SetVertices(vertices);
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        mesh.SetUVs(0, uvs0);
        mesh.SetUVs(1, uvs1);
        mesh.SetUVs(4, uvs4);
        
        Debug.Log(mesh.uv.Length);
        Debug.Log(mesh.uv2.Length);
        Debug.Log(mesh.uv3.Length);
        Debug.Log(mesh.uv4.Length);
        Debug.Log(mesh.uv5.Length);

        var path = outDir ? AssetDatabase.GetAssetPath(outDir) : Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));
        AssetDatabase.CreateAsset(mesh, path + "/multi_uv_mesh.asset");
        AssetDatabase.SaveAssets();
    }
}