using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Inventory/Build Inventory AssetBundle")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "AssetBundles";
        if(!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, 
                                        BuildAssetBundleOptions.None, 
                                        BuildTarget.StandaloneWindows);
    }
}