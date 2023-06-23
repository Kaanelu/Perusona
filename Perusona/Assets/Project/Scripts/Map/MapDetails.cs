using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// map�ڍ׃N���X
/// </summary>
public class MapDetails
{


    /// <summary>
    /// �o�g���t�B�[���h
    /// </summary>
    private Vector3 battleFieldPosition_ = Vector3.zero;
    public Vector3 GetBattleFieldPosition()
    {
        return battleFieldPosition_;
    }

    /// <summary>
    /// �o�g���t�B�[���h�O��
    /// </summary>
    private static readonly string battleObjName_ = "BattleField";

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="mapOnj"></param>
    public bool Initialize(GameObject mapOnj)
    {
        bool res;

        if(mapOnj == null)
        {
            return false;
        }


        //�q�v�f�S�擾
        var chiled = mapOnj.GetComponentsInChildren<Transform>();

        //�o�g���t�B�[���h�I�u�W�F�N�g
        GameObject battleField = null;
        foreach(var ch in chiled)
        {
            if(ch.gameObject.name == battleObjName_)
            {
                battleField = ch.gameObject;
                break;
            }
        }

        if(battleField == null)
        {
            return false;
        }

        //�o�g���t�B�[���h�I�u�W�F������W�����擾
        battleFieldPosition_ = battleField.transform.position;

        res = true;
        return res;
    }
}
