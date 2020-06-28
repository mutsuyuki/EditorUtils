using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, "mytriangle")]
public class TrianglesImporter : ScriptedImporter {
    public override void OnImportAsset(AssetImportContext context) {
        var name = System.IO.Path.GetFileNameWithoutExtension(assetPath);
        ;

        // 仮想のファイルタイプ「mytriangle」には、三角形の中心点(x,y,z）だけが書き込まれている想定
        // 読み込んでVector3にパースする
        StreamReader sr = new StreamReader(assetPath);
        string mytriangleText = sr.ReadToEnd();
        sr.Close();
        string[] temp = mytriangleText.Split(',');
        Vector3 basePos = new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));

        // ３角形のメッシュを作る
        var mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        var vertices = new List<Vector3> {
            new Vector3(0, 0.5f, 0) + basePos,
            new Vector3(0.5f, -0.5f, 0) + basePos,
            new Vector3(-0.5f, -0.5f, 0) + basePos
        };

        var indices = new List<int> {
            0, 1, 2
        };

        mesh.SetVertices(vertices);
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        mesh.name = name + "_mesh";
        context.AddObjectToAsset(mesh.name, mesh);

        // マテリアルを作る
        var material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        material.name = name + "_material";
        context.AddObjectToAsset(material.name, material);

        // 元のテキストファイルもアセットに入れておく
        var text = new TextAsset(mytriangleText);
        text.name = name + "_mytriangle_file";
        context.AddObjectToAsset(text.name, text);

        // アセットを作成
        var triangleObject = new GameObject();
        triangleObject.name = name;
        triangleObject.AddComponent<MeshFilter>().sharedMesh = mesh;
        triangleObject.AddComponent<MeshRenderer>().material = material;
        context.AddObjectToAsset(triangleObject.name, triangleObject);
        context.SetMainObject(triangleObject);
    }
}