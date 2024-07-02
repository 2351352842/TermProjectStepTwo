using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AssetBundleLoad : MonoBehaviour
{
    #region 变量定义
    public AssetBundlePattern LoadPattern;

    public Button LoadAssetBundleButton;
    public Button LoadAssetButton;
    public Button UnloadAssetButton;
    public Button UnloadAssetBundleButton;
    public AssetBundle CubeAssetBundle;
    public AssetBundle SphereAssetBundle;
    public string HTTPAddress = "http://10.24.1.119:8080/";
    string FileURL;
    string SavePath;
    string RemoteVersionPath;
    string DownloadVersionPath;
    UnityWebRequest request;
    #endregion

    void Start()
    {
        //AssetManagerRuntime.AssetManagerInit(LoadPattern);
        //if (LoadPattern == AssetBundlePattern.Remote)
        //{
        //    AssetManagerRuntime.Instance.HTTPAddress = HTTPAddress;
        //    StartCoroutine(GetRemoteVersion());
        //}
        
    }
    public void xiazai()
    {
        AssetManagerRuntime.AssetManagerInit(LoadPattern);
        if (LoadPattern == AssetBundlePattern.Remote)
        {
            AssetManagerRuntime.Instance.HTTPAddress = HTTPAddress;
            StartCoroutine(GetRemoteVersion());
        }
    }
    public void jiazai()
    {
        Debug.Log("资源加载成功");
    }
    void LoadAsset()
    {
        //AssetPackage assetPackage = AssetManagerRuntime.Instance.LoadPackage("A");
        //Debug.Log(assetPackage.PackageName);
        //GameObject sampleObject = assetPackage.LoadAsset<GameObject>("Assets/Prefabs/Sphere.prefab");
        //Instantiate(sampleObject);
    }
    DownloadInfo CurrentDownloadInfo;
    private void OnCompleted(string fileName, string msg)
    {
        Debug.Log($"{fileName}{msg}");
        if (!CurrentDownloadInfo.DownloadFileNames.Contains(fileName))
        {
            CurrentDownloadInfo.DownloadFileNames.Add(fileName);
        }
        switch (fileName)
        {
            case "AllPackages":
                CreateDownloadList();
                break;
            case "AssetBundleHashs":
                CreateDownloadList();
                break;
        }
        string downloadInfoString = JsonConvert.SerializeObject(CurrentDownloadInfo);
        string downloadInfoPath = Path.Combine(AssetManagerRuntime.Instance.DownloadPath, "Loca1DownloadInfo");
        File.WriteAllText(downloadInfoPath, downloadInfoString);
    }
    private void OnProgress(float prg, long currLength, long totalLength)
    {
        Debug.LogFormat("下载进度 {0:0.00}%, {1}M/ {2}M", (prg * 100), currLength * 1.0f / 1024 / 1024, totalLength * 1.0f / 1024 / 1024);
        //如果在100%时刚好取消下载了,那么文件将无法继续下载
        //导致无法执行下载完成方法
    }
    private void OnError(ErrorCode code, string msg)
    {

    }
    void CreateDownloadList()
    {
        //本地表读取路径
        string assetBundleHashsLoadPath = Path.Combine(AssetManagerRuntime.Instance.AssetBundleLoadPath, "AssetBundleHashs");
        string assetBundleHashsString = "";
        string[] localAssetBundleHashs = null;
        if (File.Exists(assetBundleHashsLoadPath))
        {
            assetBundleHashsString = File.ReadAllText(assetBundleHashsLoadPath);
            localAssetBundleHashs = JsonConvert.DeserializeObject<string[]>(assetBundleHashsString);
        }
        //远端表读取路径
        assetBundleHashsLoadPath = Path.Combine(AssetManagerRuntime.Instance.DownloadPath,AssetManagerRuntime.Instance.RemoteAssetVersion.ToSafeString(), "AssetBundleHashs");
        string[] remoteAssetBundleHashs = null;
        if (File.Exists(assetBundleHashsLoadPath))
        {
            assetBundleHashsString = File.ReadAllText(assetBundleHashsLoadPath);
            remoteAssetBundleHashs = JsonConvert.DeserializeObject<string[]>(assetBundleHashsString);
        }
        if (remoteAssetBundleHashs == null)
        {
            Debug.LogError("远端表读取失败,请查看文件是否存在");
            return;
        }
        List<string> assetBundleNames = null;
        if (localAssetBundleHashs == null)
        {
            Debug.LogWarning("本地表读取失败,直接下载远端资源");
            assetBundleNames = remoteAssetBundleHashs.ToList();
        }
        else
        {
            AssetBundleVersionDiffrence versionDiffrence = ContrastAssetBundleHashTable(localAssetBundleHashs, remoteAssetBundleHashs);
            assetBundleNames = versionDiffrence.AdditionAssetBundles;
        }
        if (assetBundleNames != null && assetBundleNames.Count > 0)
        {
            assetBundleNames.Add("LocalAssets");
            StartCoroutine(DownloadAssetBundle(assetBundleNames, () => {
                CopyDownloadAssetsToLocalFile();
                AssetManagerRuntime.Instance.UpdateLocalAssetVersion();
                LoadAsset();
            }));
        }
        else;
    }
    public static AssetBundleVersionDiffrence ContrastAssetBundleHashTable(string[] oldHashTable, string[] newHashTable)
    {
        AssetBundleVersionDiffrence diffrence = new AssetBundleVersionDiffrence();
        diffrence.AdditionAssetBundles = new List<string>();
        diffrence.ReducedAssetBundles = new List<string>();
        //如果老的Hash列表中,有新Hash列表不包含的包,说明是需要移除的包
        foreach (string assetHash in oldHashTable)
        {
            if (!newHashTable.Contains(assetHash))
            {
                diffrence.ReducedAssetBundles.Add(assetHash);
            }
        }

        //如果新的Hash表中,有老的Hash表不包含的包,说明是新增的包
        foreach (string assetHash in newHashTable)
        {
            if (!oldHashTable.Contains(assetHash))
            {
                diffrence.AdditionAssetBundles.Add(assetHash);
            }
        }

        return diffrence;
    }
    void CopyDownloadAssetsToLocalFile()
    {
        string downloadVersionFilePath = Path.Combine(AssetManagerRuntime.Instance.DownloadPath, AssetManagerRuntime.Instance.RemoteAssetVersion.ToSafeString());
        DirectoryInfo directoryInfo = new DirectoryInfo(downloadVersionFilePath);
        string localAssetVersionPath = Path.Combine(AssetManagerRuntime.Instance.LocalAssetPath, AssetManagerRuntime.Instance.RemoteAssetVersion.ToSafeString());
        directoryInfo.MoveTo(localAssetVersionPath);
    }
    IEnumerator GetRemoteVersion()
    {
        string remoteVersionFilePath = Path.Combine(HTTPAddress, "BuildOutput", "BuildVersion.version");
        UnityWebRequest request = UnityWebRequest.Get(remoteVersionFilePath);
        request.SendWebRequest();
        while (!request.isDone)
        {
            //返回null代表等待一帧
            yield return null;
        }
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError(request.error);
            yield break;
        }
        int version = int.Parse(request.downloadHandler.text);
        AssetManagerRuntime.Instance.RemoteAssetVersion = version;
        Debug.Log($"远端资源版本为{version}");
        if (AssetManagerRuntime.Instance.LocalAssetVersion == version)
        {
            Debug.Log("版本一致无需更新");
            LoadAsset();
            yield break;
        }
            string downloadPath = Path.Combine(AssetManagerRuntime.Instance.DownloadPath, AssetManagerRuntime.Instance.RemoteAssetVersion.ToString());
        if (!Directory.Exists(downloadPath))
        {
            Directory.CreateDirectory(downloadPath);
        }
        if (AssetManagerRuntime.Instance.LocalAssetVersion != AssetManagerRuntime.Instance.RemoteAssetVersion)
        {
            StartCoroutine(GetRemotePackages());
        }
        yield return null;
    }
    IEnumerator GetRemotePackages()
    {
        string remotePackagePath = Path.Combine(HTTPAddress, "BuildOutput", AssetManagerRuntime.Instance.RemoteAssetVersion.ToString(), "AllPackages");
        UnityWebRequest request = UnityWebRequest.Get(remotePackagePath);
        request.SendWebRequest();
        while (!request.isDone)
        {
            //返回nul1代表等待一帧
            yield return null;
        }
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError(request.error);
            yield break;
        }
        string allPackagesString = request.downloadHandler.text;
        string packagesSavePath = Path.Combine(AssetManagerRuntime.Instance.DownloadPath, AssetManagerRuntime.Instance.RemoteAssetVersion.ToString(), "AllPackages");
        File.WriteAllText(packagesSavePath, allPackagesString);
        Debug.Log($"Packages下载完成{packagesSavePath}");
        List<string> packageNames = JsonConvert.DeserializeObject<List<string>>(allPackagesString);
        foreach (string packageName in packageNames)
        {
            remotePackagePath = Path.Combine(HTTPAddress, "BuildOutput", AssetManagerRuntime.Instance.RemoteAssetVersion.ToString(), packageName);
            request = UnityWebRequest.Get(remotePackagePath);
            request.SendWebRequest();
            while (!request.isDone)
            {
                //返回null代表等待一帧
                yield return null;
            }
            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
                yield break;
            }
            string packageString = request.downloadHandler.text;
            packagesSavePath = Path.Combine(AssetManagerRuntime.Instance.DownloadPath, AssetManagerRuntime.Instance.RemoteAssetVersion.ToString(), packageName);
            File.WriteAllText(packagesSavePath, packageString);
            Debug.Log($"package下载完成{packageName}");
        }
        StartCoroutine(GetRemoteAssetBundleHash());
        yield return null;
    }
    IEnumerator GetRemoteAssetBundleHash()
    {
        string remoteHashPath = Path.Combine(HTTPAddress, "BuildOutput", AssetManagerRuntime.Instance.RemoteAssetVersion.ToString(), "AssetBundleHashs");
        UnityWebRequest request = UnityWebRequest.Get(remoteHashPath);
        request.SendWebRequest();
        while (!request.isDone)
        {
            //返回null代表等待一帧
            yield return null;
        }
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError(request.error);
            yield break;
        }
        string hashString = request.downloadHandler.text;
        string hashSavePath = Path.Combine(AssetManagerRuntime.Instance.DownloadPath, AssetManagerRuntime.Instance.RemoteAssetVersion.ToString(), "AssetBundleHashs");
        File.WriteAllText(hashSavePath, hashString);
        Debug.Log($"AssetBundleHash列表下载完成{hashString}");
        CreateDownloadList();
        yield return null;
    }
    IEnumerator DownloadAssetBundle(List<string> fileNames, Action callBack = null)
    {
        foreach (string fileName in fileNames)
        {
            string assetBundleName = fileName;
            if (fileName.Contains("_"))
            {
                //下划线后一位才是AssetBundleName
                int startIndex = fileName.IndexOf("_") + 1;
                assetBundleName = fileName.Substring(startIndex);
            }
            string assetBundleDownloadPath = Path.Combine(HTTPAddress, "BuildOutput", AssetManagerRuntime.Instance.RemoteAssetVersion.ToString(), assetBundleName);
            Debug.Log(assetBundleDownloadPath);
            UnityWebRequest request = UnityWebRequest.Get(assetBundleDownloadPath);
            request.SendWebRequest();
            while (!request.isDone)
            {
                //返回null代表等待一帧
                yield return null;
            }
            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
                yield break;
            }
            String assetBundleSavePath = Path.Combine(AssetManagerRuntime.Instance.DownloadPath, AssetManagerRuntime.Instance.RemoteAssetVersion.ToString(), assetBundleName);
            File.WriteAllBytes(assetBundleSavePath, request.downloadHandler.data);
            Debug.Log($"AssetBundle下载完毕{assetBundleName}");
        }
        callBack?.Invoke();
        yield return null;
    }
}
