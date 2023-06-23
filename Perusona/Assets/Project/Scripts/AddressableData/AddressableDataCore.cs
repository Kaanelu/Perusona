using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アドレサブルデータコア
/// </summary>
public class AddressableDataCore : MonoBehaviour
{
    /// <summary>
    /// AddressableDataCoreのグローバス変数
    /// </summary>
    /// 
    
    private static AddressableDataCore global_addressable_data_core_ = null;
    public static AddressableDataCore Instance
    {
        get
        {
            if(global_addressable_data_core_ == null)
            {
                GameObject gameObject = new GameObject("AddressableDataCore");
                global_addressable_data_core_ =  gameObject.AddComponent<AddressableDataCore>();

            }

            return global_addressable_data_core_;
        }
    }

    /// <summary>
    /// クリエイト関数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static AddressableObject<T> CreateAddressable<T>(string path) where T : Object, new()
    {
        return new AddressableObject<T>(path);
    }

    private AddressableDataContainer data_container_ = new AddressableDataContainer();
    public AddressableDataContainer GetAddressableDataContainer()
    {
        return data_container_;
    }

    // Start is called before the first frame update
    void Start()
    {
        global_addressable_data_core_ = this;
        if(data_container_ == null)data_container_ = new AddressableDataContainer();
    }

    // Update is called once per frame
    void Update()
    {
        if (global_addressable_data_core_ == null) return;

        AutoRelease();
    }

    /// <summary>
    /// 追加
    /// </summary>
    /// <param name="add_data"></param>
    public void AddAddressableData(BaseAddressableData add_data)
    {
        if (data_container_ == null) return;
        data_container_.AddAddressableData(add_data);
    }

    /// <summary>
    /// インデックスでBaseAddressableDataを取得
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BaseAddressableData Find(uint index)
    {
        if (data_container_ == null) return null;

        return data_container_.Find(index);
    }

    /// <summary>
    /// BaseAddressableDataでBaseAddressableDataを取得
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BaseAddressableData Find(BaseAddressableData data)
    {
        if (data_container_ == null) return null;

        return data_container_.Find(data);
    }

    /// <summary>
    /// 自動リリース(nullだった場合)
    /// </summary>
    public void AutoRelease()
    {
        if (data_container_ == null) return;
        data_container_.AutoRelease();
    }
}
