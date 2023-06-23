using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
/// <summary>
/// �A�h���T�u���f�[�^�̓��ꕨ
/// </summary>
public class AddressableDataContainer
{
    /// <summary>
    /// BaseAddressableData�̃��X�g
    /// </summary>
    [SerializeField]
    private List<BaseAddressableData> list_addressable_data_ = new List<BaseAddressableData>();

    /// <summary>
    /// ���X�g�ő�J�E���g
    /// </summary>
    /// <returns></returns>
    public uint GetListAddressableDataCount()
    {
        if (list_addressable_data_ == null) return (0);
        return (uint)list_addressable_data_.Count;
    }


    /// <summary>
    /// �ǉ�
    /// </summary>
    /// <param name="add_data"></param>
    public void AddAddressableData(BaseAddressableData add_data)
    {
        if (add_data == null) return;

        list_addressable_data_.Add(add_data);
    }

    /// <summary>
    /// �C���f�b�N�X��BaseAddressableData���擾
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BaseAddressableData Find(uint index)
    {
        if (list_addressable_data_ == null) return null;
        if (list_addressable_data_.Count == 0 || index < 0) return null;
        if (list_addressable_data_.Count <= index) return null;

        return list_addressable_data_[(int)index];
    }

    /// <summary>
    /// BaseAddressableData��BaseAddressableData���擾
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BaseAddressableData Find(BaseAddressableData data)
    {
        if (list_addressable_data_ == null) return null;
        if (list_addressable_data_.Count == 0) return null;

        return list_addressable_data_.Find(find_data => find_data == data);
    }

    /// <summary>
    /// ���������[�X(null�������ꍇ)
    /// </summary>
    public void AutoRelease()
    {
        if (list_addressable_data_ == null) return;
        if (list_addressable_data_.Count == 0) return;

        var list_release = list_addressable_data_.FindAll(find_data => find_data.GetFlagAutoRelease() && find_data.GetFlagSetUpLoading() && true);

        bool is_release = false;
        for (int count = 0; count < list_release.Count; count++)
        {
            var data = list_release[count];
            if (data == null) continue;

            data.Release();

            is_release = true;
        }

        if (is_release == false) return;

        list_addressable_data_.RemoveAll(find_data => find_data.GetBaseAddressableData() == null);
    }
}
