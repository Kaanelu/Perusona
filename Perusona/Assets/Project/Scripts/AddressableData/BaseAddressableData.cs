using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// アドレサブルデータ
/// </summary>
public class BaseAddressableData
{

    public BaseAddressableData() { AddressableDataCore.Instance.AddAddressableData(this); }

    /// <summary>
    /// 複数読み込みか
    /// </summary>
    protected bool is_array_ = false;

    /// <summary>
    /// セットアップ確認
    /// </summary>
    protected bool is_setup_ = false;

    /// <summary>
    /// 自動でリリースするかどうか
    /// </summary>
    protected bool is_auto_release_ = false;
    public bool GetFlagAutoRelease()
    {
        return is_auto_release_;
    }
    /// <summary>
    /// 自動リリース
    /// </summary>
    public void OnAutoRelease()
    {
        is_auto_release_ = true;
    }

    /// <summary>
    /// 使用フラグ
    /// </summary>
    protected bool is_use_ = false;
    public bool GetIsUse() { return is_use_; }
    public void OnIsUse() { is_use_ = true; }

    /// <summary>
    /// ロード確認
    /// </summary>
    protected bool is_load_ = false;

    /// <summary>
    /// セットアップとロードの確認
    /// </summary>
    /// <returns></returns>
    public bool GetFlagSetUpLoading()
    {
        return is_setup_ & is_load_;
    }

    /// <summary>
    /// アドレサブルオブジェクト
    /// </summary>
    protected Object addressable_object_ = null;
    public Object GetBaseAddressableData()
    {
        return addressable_object_;
    }

    protected Object[] array_addressable_object_ = null;
    public Object[] GetBaseArrayAddressableData()
    {
        return array_addressable_object_;
    }
    public Object GetBaseArrayAddressableData(int index)
    {
        if (array_addressable_object_ == null) return null;
        if (array_addressable_object_.Length == 0) return null;
        if (array_addressable_object_.Length <= index || index < 0) return null;

        return array_addressable_object_[index];
    }
    public uint GetArrayAddressableDataCount()
    {
        if (array_addressable_object_ == null) return (0);

        return (uint)array_addressable_object_.Length;
    }


    /// <summary>
    /// ロード開始
    /// </summary>
    /// <param name="load_path"></param>
    public virtual void LoadStart(string load_path) { }
    public virtual void LoadArrayStart(string load_path) { }


    public virtual void Release() { }
}

public class AddressableData<T> : BaseAddressableData where T : Object
{
    public AddressableData(){}

    

    public T GetAddressableData()
    {
        if (GetFlagSetUpLoading() == false) return null;

        return addressable_object_ as T;
    }

    public T GetArrayAddressableData(int index)
    {
        if (array_addressable_object_ == null) return null;
        if (array_addressable_object_.Length == 0) return null;
        if (array_addressable_object_.Length <= index || index < 0) return null;

        return array_addressable_object_[index] as T;
    }

    protected AsyncOperationHandle<T> addressable_handle_ = new AsyncOperationHandle<T>();
    public AsyncOperationHandle<T> GetAddressableHandle()
    {
        return addressable_handle_;
    }

    protected AsyncOperationHandle<IList<T>> array_addressable_handle_ = new AsyncOperationHandle<IList<T>>();
    public AsyncOperationHandle<IList<T>> GetArrayAddressableHandle()
    {
        return array_addressable_handle_;
    }

    public override void LoadStart(string load_path)
    {
        if (is_load_ == true) return;
        if (is_setup_ == true) return;
        if (load_path == "") return;




        is_load_ = true;
        addressable_handle_ = Addressables.LoadAssetAsync<T>(load_path);
        addressable_handle_.Completed += op =>
        {
            addressable_object_ = op.Result;
            array_addressable_object_ = new Object[1];
            array_addressable_object_[0] = addressable_object_;
            is_setup_ = true;
        };

       

    }

    public override void LoadArrayStart(string load_path)
    {
        if (is_load_ == true) return;
        if (is_setup_ == true) return;
        if (load_path == "") return;

        is_load_ = true;
        array_addressable_handle_ = Addressables.LoadAssetAsync<IList<T>>(load_path);
        array_addressable_handle_.Completed += op =>
        {
            array_addressable_object_ = new Object[op.Result.Count];

            for(int count = 0; count < op.Result.Count; count++)
            {
                array_addressable_object_[count] = op.Result[count];
            }
            addressable_object_ = array_addressable_object_[0];
            is_setup_ = true;
            is_array_ = true;
        };
    }

    public override void Release()
    {
        if (GetFlagSetUpLoading() == false) return;

        

        if (addressable_handle_.Equals(null) == false && addressable_object_ != null && is_array_ == false)
        {
            
            Addressables.Release(addressable_handle_);

            addressable_object_ = null;
            array_addressable_object_ = null;


        }
        if (array_addressable_handle_.Equals(null) == false && array_addressable_object_ != null)
        {
            if (is_array_ == false) return;

            Addressables.Release(array_addressable_handle_);
            if (array_addressable_object_.GetType() == typeof(GameObject))
            {
                foreach (var obj in array_addressable_object_)
                {
                    Addressables.ReleaseInstance(obj as GameObject);
                }
            }
            addressable_object_ = null;
            array_addressable_object_ = null;
        }

    }

}

