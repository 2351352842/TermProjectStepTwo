using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class BuildInfo
{
    public int BuildVersion;
    public Dictionary<string, ulong> FileNames = new Dictionary<string, ulong>();
    public ulong FileTotalSize;
}
/// <summary>
/// 对应了EditorWindow界面上打包的类
/// </summary>
public class AssetPackage
{

    public PackageInfo Package;
    public string PackageName { get { return Package.PackageName; } }

    /// <summary>
    /// 当前包中已加载出的资源
    /// </summary>
    public Dictionary<string, Object> LoadedAssets = new Dictionary<string, Object>();

    public T LoadAsset<T>(string assetName) where T : Object
    {
        T assetObject = default;

        foreach (AssetInfo info in Package.assetInfos)
        {
            if (info.AssetName == assetName)
            {
                if (LoadedAssets.Keys.Contains(assetName))
                {
                    return LoadedAssets[assetName] as T;
                }

                foreach (string dependAssetBundle in AssetManagerRuntime.Instance.MainBundleManifest.GetAllDependencies(info.AssetBundleName))
                {
                    string dependPath = Path.Combine(AssetManagerRuntime.Instance.AssetBundleLoadPath, dependAssetBundle);

                    AssetBundle.LoadFromFile(dependPath);
                }

                string assetBundlePath = Path.Combine(AssetManagerRuntime.Instance.AssetBundleLoadPath, info.AssetBundleName);

                AssetBundle bundle = AssetBundle.LoadFromFile(assetBundlePath);

                assetObject = bundle.LoadAsset<T>(assetName);

            }
        }
        if (assetObject == null)
        {
            Debug.LogError($"没有找到{assetName}");
        }
        return assetObject;
    }
}

public class AssetManagerRuntime
{
    /// <summary>
    /// 当前类的单例变量
    /// 
    /// </summary>
    public static AssetManagerRuntime Instance;

    /// <summary>
    /// AB包加载模式
    /// </summary>
    AssetBundlePattern CurrentPattern;

    /// <summary>
    /// AB包本地加载路径
    /// </summary>
    public string AssetBundleLoadPath;

    /// <summary>
    /// 本地资源路径
    /// </summary>
    public string LocalAssetPath;

    /// <summary>
    /// Asset下载地址
    /// </summary>
    public string DownloadPath;

    /// <summary>
    /// 本地资源版本
    /// </summary>
    public int LocalAssetVersion;

    /// <summary>
    /// 远端资源版本
    /// </summary>
    public int RemoteAssetVersion;

    /// <summary>
    /// 本地所有包的列表文件
    /// </summary>
    List<string> LocalAllPackages;


    /// <summary>
    /// 已加载的包列表,避免重复加载
    /// </summary>
    Dictionary<string, AssetPackage> LoadedPackages = new Dictionary<string, AssetPackage>();

    /// <summary>
    /// 主包的Manifest
    /// </summary>
    public AssetBundleManifest MainBundleManifest;

    public const string LocalAssetFolderName = "LocalAssets";

    /// <summary>
    /// HTTP资源服务器地址
    /// </summary>
    public string HTTPAddress = "http://10.24.1.119:8080/";

    public static void AssetManagerInit(AssetBundlePattern assetBundlePattern)
    {
        if (Instance == null)
        {
            Instance = new AssetManagerRuntime();
        }
        Instance.CurrentPattern = assetBundlePattern;

        Instance.CheckAssetBundlePath();
        Instance.CheckLocalAssetVersion();
        Instance.CheckAssetBundleLoadPath();
    }


    void CheckAssetBundlePath()
    {
        switch (CurrentPattern)
        {
            case AssetBundlePattern.EditorSimulation:
                //AssetBundleLoadPath = Path.Combine(Application.persistentDataPath,LocalAssetFolderName);
                break;
            case AssetBundlePattern.Local:
                LocalAssetPath = Path.Combine(Application.streamingAssetsPath, LocalAssetFolderName);
                break;
            case AssetBundlePattern.Remote:
                DownloadPath = Path.Combine(Application.persistentDataPath, "DownloadAsset");
                LocalAssetPath = Path.Combine(Application.streamingAssetsPath, LocalAssetFolderName);
                if (!Directory.Exists(DownloadPath))
                {
                    Directory.CreateDirectory(DownloadPath);
                }
                break;
        }
    }
    void CheckLocalAssetVersion()
    {
        string versionFilePath = Path.Combine(DownloadPath, "LocalVersion.version");

        if (!File.Exists(versionFilePath))
        {
            LocalAssetVersion = 100;
            File.WriteAllText(versionFilePath, LocalAssetVersion.ToString());
            return;
        }

        LocalAssetVersion = int.Parse(File.ReadAllText(versionFilePath));
        Debug.Log(LocalAssetVersion);
    }
    void CheckAssetBundleLoadPath()
    {
        switch (CurrentPattern)
        {
            case AssetBundlePattern.EditorSimulation:
                //AssetBundleLoadPath = Path.Combine(Application.persistentDataPath, "AssetBundles");
                break;
            case AssetBundlePattern.Local:
                AssetBundleLoadPath = Path.Combine(LocalAssetPath, LocalAssetVersion.ToString());
                break;
            case AssetBundlePattern.Remote:
                AssetBundleLoadPath = Path.Combine(LocalAssetPath, LocalAssetVersion.ToString());
                break;
        }
        AssetBundleLoadPath = Path.Combine(LocalAssetPath, LocalAssetVersion.ToString());
        if (!Directory.Exists(AssetBundleLoadPath))
        {
            Directory.CreateDirectory(AssetBundleLoadPath);
        }
    }
    

    public void UpdateLocalAssetVersion()
    {
        LocalAssetVersion = RemoteAssetVersion;
        string versionFilePath = Path.Combine(LocalAssetPath, "LocalVersion.version");
        File.WriteAllText(versionFilePath, LocalAssetVersion.ToString());
        CheckAssetBundleLoadPath();
        Debug.Log($"本地版本更新结束,当前版本为{LocalAssetVersion}");
    }

    /// <summary>
    /// 加载Package方法
    /// </summary>
    /// <param name="PackageName"> 将要加载的包名 </param>
    /// <returns></returns>
    public AssetPackage LoadPackage(string PackageName)
    {
        if (LocalAllPackages == null)
        {
            string packageListPath = Path.Combine(Application.streamingAssetsPath,"LocalAssets",LocalAssetVersion.ToString(), "AllPackages");
            string packageListString = File.ReadAllText(packageListPath);

            LocalAllPackages = JsonConvert.DeserializeObject<List<string>>(packageListString);
        }
        if (LoadedPackages.Keys.Contains(PackageName))
        {
            Debug.LogWarning($"{PackageName}包已加载");
            return LoadedPackages[PackageName];
        }
        if (MainBundleManifest == null)
        {
            string mainBundlePath = Path.Combine(AssetBundleLoadPath, "LocalAssets");

            AssetBundle mainBundle = AssetBundle.LoadFromFile(mainBundlePath);
            MainBundleManifest = mainBundle.LoadAsset<AssetBundleManifest>(nameof(AssetBundleManifest));
        }
        AssetPackage package = new AssetPackage();
        string packagePath = Path.Combine(AssetBundleLoadPath, PackageName);
        string packageString = File.ReadAllText(packagePath);
        package.Package = JsonConvert.DeserializeObject<PackageInfo>(packageString);
        LoadedPackages.Add(PackageName, package);
        foreach (string dependPackageName in package.Package.PackageDependencies)
        {
            LoadPackage(dependPackageName);
        }
        return package;
    }
}
