using UnityEditor;
using UnityEngine;

public static class FbxChildDuplicator
{
    // FBX を選択しているときだけメニューを有効化
    [MenuItem("Assets/Duplicate All Child Objects In FBX", validate = true)]
    private static bool DuplicateAllSubobjects_Validate()
    {
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return
            !string.IsNullOrEmpty(path) &&
            Selection.activeObject is GameObject &&
            path.EndsWith(".fbx", System.StringComparison.OrdinalIgnoreCase);
    }

    [MenuItem("Assets/Duplicate All Child Objects In FBX")]
    private static void DuplicateAllSubobjects()
    {
        // 選択中の FBX メインアセットを取得
        var assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var mainAsset = AssetDatabase.LoadMainAssetAtPath(assetPath);
        if (mainAsset == null)
        {
            Debug.LogError("Failed to load main asset at path: " + assetPath);
            return;
        }

        // CopiedFolder がなければ作成
        const string copiedFolderPath = "Assets/CopiedFolder";
        if (!AssetDatabase.IsValidFolder(copiedFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", "CopiedFolder");
        }

        // FBX 内の全サブアセットを取得
        var subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);
        int count = 0;

        foreach (var sub in subAssets)
        {
            if (sub is GameObject go)
            {
                // シーンに一時的にインスタンス化
                var instance = Object.Instantiate(go);
                instance.name = go.name;

                // プレハブとして保存
                string prefabPath = AssetDatabase.GenerateUniqueAssetPath(
                    $"{copiedFolderPath}/{instance.name}.prefab"
                );
                PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);

                // 作成したインスタンスを破棄
                Object.DestroyImmediate(instance);
                count++;
            }
        }

        if (count > 0)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"Duplicated {count} child GameObjects to {copiedFolderPath}");
        }
        else
        {
            Debug.LogWarning("No child GameObjects found in the selected FBX.");
        }
    }
}
