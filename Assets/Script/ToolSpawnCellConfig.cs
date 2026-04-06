using UnityEditor;
using UnityEngine;

public class ToolSpawnCellConfig : EditorWindow
{
    [MenuItem("Tools/Pikachu/Tự động tạo 24 Cell Config")]

    public static void Create24CellConfig()
    {
        string folderPath = "Assets/ScriptableObject";

        for(int i = 1; i <= 24; i++)
        {
            CellConfig newData = ScriptableObject.CreateInstance<CellConfig>();
            
            
            newData.IdCell = i;

            newData.ColorCell = Random.ColorHSV();

        
            string assetPath = $"{folderPath}/Cell_{i}.asset";
            AssetDatabase.CreateAsset(newData, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
}
