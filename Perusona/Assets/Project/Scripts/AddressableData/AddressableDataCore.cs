using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�h���T�u���f�[�^�R�A
/// </summary>
public class AddressableDataCore : MonoBehaviour
{
    /// <summary>
    /// AddressableDataCore�̃O���[�o�X�ϐ�
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
    /// �N���G�C�g�֐�
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
    /// �ǉ�
    /// </summary>
    /// <param name="add_data"></param>
    public void AddAddressableData(BaseAddressableData add_data)
    {
        if (data_container_ == null) return;
        data_container_.AddAddressableData(add_data);
    }

    /// <summary>
    /// �C���f�b�N�X��BaseAddressableData���擾
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BaseAddressableData Find(uint index)
    {
        if (data_container_ == null) return null;

        return data_container_.Find(index);
    }

    /// <summary>
    /// BaseAddressableData��BaseAddressableData���擾
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BaseAddressableData Find(BaseAddressableData data)
    {
        if (data_container_ == null) return null;

        return data_container_.Find(data);
    }

    /// <summary>
    /// ���������[�X(null�������ꍇ)
    /// </summary>
    public void AutoRelease()
    {
        if (data_container_ == null) return;
        data_container_.AutoRelease();
    }
}
