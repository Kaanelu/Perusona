using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;

/// <summary>
/// Addressable対応オブジェクト
/// </summary>
[System.Serializable]
public class AddressableObject<T> where T : Object
{
    /// <summary>
    /// ロードが完了したかどうか
    /// </summary>
    [SerializeField]
    private bool is_setup_ = false;
    public bool IsSetUp
    {
        get { return is_setup_; }
    }

    /// <summary>
    /// ロードは一回だけのため、フラグ
    /// </summary>
    [SerializeField]
    private bool is_check_ = false;

    /// <summary>
    /// インスタンスしたかどうか
    /// </summary>
    private bool is_instance_ = false;

    /// <summary>
    /// ロードするオブジェクト
    /// </summary>
    [SerializeField]
    private T object_;

    public T GetObject
    {
        get { return object_; }
    }

    /// <summary>
    /// アドレスのパス
    /// </summary>
    [SerializeField]
    private string addressable_path_ = "";
    public string SetPath
    {
        set { addressable_path_ = value; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="value_path"></param>
    public AddressableObject(string value_path)
    {
        addressable_path_ = value_path;
    }

    /// <summary>
    /// ロード
    /// </summary>
    public void LoadStart()
    {
        if (is_check_ == true) return;
        if (is_setup_ == true) return;
        is_check_ = true;
        Addressables.LoadAssetAsync<T>(addressable_path_).Completed += op => 
        {
            object_ = op.Result;
            is_setup_ = true;
        };
    }

    /// <summary>
    /// ロード
    /// </summary>
    public void LoadStart(Object value_obj)
    {
        if (is_check_ == true) return;
        if (is_setup_ == true) return;
        is_check_ = true;
        Addressables.LoadAssetAsync<T>(addressable_path_).Completed += op =>
        {
            is_setup_ = true;
            object_ = op.Result;
            value_obj = object_;
            Release();
        };
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <returns></returns>
    public GameObject Instance()
    {
        if (is_setup_ == false) return null;
        if (object_ == null) return null;
        if (object_.GetType() != typeof(GameObject)) return null;

        if(is_instance_ == false)
        {
            GameObject obj = GameObject.Instantiate(object_ as GameObject, Vector3.zero,Quaternion.identity);
            is_instance_ = true;
            return obj;
        }

        return null;
    }

    /// <summary>
    /// 開放
    /// </summary>
    public void Release()
    {
        if (is_setup_ == false) return;
        if (object_ == null) return;
        if (object_.GetType() != typeof(GameObject)) Addressables.Release<T>(object_);
        else
        {
            Addressables.Release<T>(object_);
            Addressables.ReleaseInstance(object_ as GameObject);
        }
        object_ = null;
    }
}
