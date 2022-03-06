using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothes : MonoBehaviour
{

    // ������ �� ���� � ���������
    [SerializeField] private Transform armorSloats;
    // ������ � 3� �������� ������
    [SerializeField] private List<ClothesController> clothesCon;



    public string pHead = "";
    public string pBody = "";
    public string pBottom = "";
    public string pLegs = "";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    /// <summary>
    /// ���������� ������ �� ���������
    /// </summary>
    /// <param name="type">��� �����: ������, ����, ��� ��� ����</param>
    /// <param name="takeOn">�������� ������� ���������� �������, ����� ��� �����</param>
    /// <param name="name">���� ����� �� ���������� �������� ������</param>
    public void UpdateArmor(ArmorType type, bool takeOn, string name)
    {

        // �������� �� �� ��� ������ ������� ��� �������
        if (takeOn)
        {
            
            // ����������� ��� ���� ����� � ������� �����
            TakeOffSameTypes(type);

            // ����� ������������ ����� �� ����� � ��������� �� �����������
            foreach (var item in clothesCon)
            {
                if (item._nameOfModel.Equals(name))
                {
                    item._modelClothes.SetActive(true);
                }
            }
        }
        else
        {
            switch (type)
            {
                case ArmorType.Head:

                    // ����������� ��� ���� ����� � ����� ����
                    TakeOffSameTypes(type);

                    // ����� ����� �� ��������� � ��������� �� �����������
                    foreach (var item in clothesCon)
                    {
                        if (item._nameOfModel.Equals("head"))
                        {
                            item._modelClothes.SetActive(true);
                        }
                    }

                    break;
                case ArmorType.Body:
                    // ����������� ��� ���� ����� � ����� ����
                    TakeOffSameTypes(type);

                    // ����� ����� �� ��������� � ��������� �� �����������
                    foreach (var item in clothesCon)
                    {
                        if (item._nameOfModel.Equals("body"))
                        {
                            item._modelClothes.SetActive(true);
                        }
                    }

                    break;
                case ArmorType.Bottom:

                    // ����������� ��� ���� ����� � ����� ����
                    TakeOffSameTypes(type);

                    // ����� ����� �� ��������� � ��������� �� �����������
                    foreach (var item in clothesCon)
                    {
                        if (item._nameOfModel.Equals("bottom"))
                        {
                            item._modelClothes.SetActive(true);
                        }
                    }
                    break;
                case ArmorType.Legs:

                    // ����������� ��� ���� ����� � ����� ����
                    TakeOffSameTypes(type);

                    // ����� ����� �� ��������� � ��������� �� �����������
                    foreach (var item in clothesCon)
                    {
                        if (item._nameOfModel.Equals("legs"))
                        {
                            item._modelClothes.SetActive(true);
                        }
                    }

                    break;
                default:
                    break;
            }
        }
        
    }

    private void TakeOffSameTypes(ArmorType type)
    {

        foreach (var item in clothesCon)
        {

            if (item._armorType == type)
            {
                item._modelClothes.SetActive(false);
            }

            
        }
    }



    // ����� ��� �������� ������� � ������� �������� ������ �� 3� ������ ������
    [System.Serializable]
    private class ClothesController
    {
        public GameObject _modelClothes;
        public string _nameOfModel;
        public ArmorType _armorType;

    }
}

