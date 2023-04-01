using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public delegate void ShowBox(int x, int y, int ball);

public class MatchThree
{
    private int Size = 10;
    private int Items = 5;
    int[,] map;
    Random random = new Random();
    Vector2Int itemSelected; 
    bool isIconSelected;    

    public MatchThree(int size, int itemsCount)
    {
        Size = size;
        Items = itemsCount;
        map = new int[Size, Size];
        FillBoxes();
    }

    /// <summary>
    /// ��������� ������ ������� ������ ����������, ���� ������ ���� �������
    /// </summary>
    /// <returns>��������� 3 ��������: x, y -> ���������� ��������, z -> ����� ������ </returns>
    public List<Vector3> FallNewItems()
    {
        List<Vector3> newItems = new List<Vector3>();
        for (int x = 0; x < Size; x++)
        {
            if (map[x,0] == -1)
            {
                int icon = random.Next(Items);
                map[x, 0] = icon;
                newItems.Add(new Vector3(x, 0, icon));
            }
        }
        return newItems;
    }

    /// <summary>
    /// � ������ �������� item ����, ���� ������ ������� ������
    /// </summary>
    /// <returns>���������� ������ ��������� ���������, ������� ����� ��������</returns>
    public List<Vector2> FallItems()
    {        
        List<Vector2> itemsToFall = new(); 
        for (int x = 0; x < Size; x++)
        {
            for (int y = Size - 2; y >= 0; y--)
            {
                if (map[x, y + 1] == -1 && map[x, y] != -1)
                {
                    map[x, y + 1] = map[x, y];
                    map[x, y] = -1;
                    itemsToFall.Add(new Vector2(x,y));
                }
            }
        }
            
        return itemsToFall;
    }

    public List<Vector2> GetItemsToDelete()
    {
        int[,] items = new int[Size, Size];
        for (int x = 0; x < Size; x++)
            for (int y = 0; y < Size; y++)
                items[x, y] = 0;
        
        int? item1;
        int? item2;
        bool more3;
        void ValidItem(int x, int y, bool isRow)
        {
            if (item1 == null) item1 = map[x, y]; //������ �������            
            else
            {
                if (item1 == map[x, y])
                {
                    //������ ������� ������������������ ����� ��������
                    if (item2 == null) item2 = map[x, y]; //������� ������� ������ � ������������������                    
                    else
                    {
                        //������������������ >= 3
                        if (!more3)
                        {
                            if (isRow)
                            {
                                items[x - 2, y] += 1;
                                items[x - 1, y] += 1;
                            }
                            else
                            {
                                items[x, y - 2] += 1;
                                items[x, y - 1] += 1;
                            }
                            //������������������ = 3                            
                            more3 = true;

                        }
                        items[x, y] += 1;
                    }
                }
                else //������ ������� ������������������ �� ����� ��������, �������� ����� ������������������
                {
                    item1 = map[x, y];
                    item2 = null;
                    more3 = false;
                }
            }
        }

        for (int y = 0; y < Size; y++)
        {
            more3 = false;
            item1 = null;
            item2 = null;
            for (int x = 0; x < Size; x++)
            {
                ValidItem(x, y, true);
            }
        }
        
        for (int x = 0; x < Size; x++)
        {
            more3 = false;
            item1 = null;
            item2 = null;

            for (int y = 0; y < Size; y++)
            {
                ValidItem(x, y, false);
            }
        }
        List<Vector2> itemsToDelet = new();
        for (int y = 0; y < Size; y++)
            for (int x = 0; x < Size; x++)
                if (items[x, y] > 0)
                {
                    map[x, y] = -1;
                    itemsToDelet.Add(new Vector2(x, y));
                }
        
        if (itemsToDelet.Count == 0) return null;
        return itemsToDelet;
    }

    public int GetIcon(int x, int y)
    {
        return map[x, y];
    }

    public bool CanMove(int x, int y)
    {
        
        if (!isIconSelected)
        {
            TakeItem(x, y);
            return false;
        }

        int dx = x - itemSelected.x;
        int dy = y - itemSelected.y;

        if ((dx == 0 && (dy == 1 || dy == -1)) || (dy == 0 && (dx == 1 || dx == -1)))
        {
            MoveItems(x, y);
            return true;
        }
        else
        {
            TakeItem(x, y);
            return false;          
        }
        
    }

    private void MoveItems(int x, int y)
    {
        (map[itemSelected.x, itemSelected.y], map[x, y]) = (map[x, y], map[itemSelected.x, itemSelected.y]);
        isIconSelected = false;
    }
    public void SwapItems(Vector2Int item1, Vector2Int item2)
    {
        (map[item1.x, item1.y], map[item2.x, item2.y]) = (map[item2.x, item2.y], map[item1.x, item1.y]);
    }

    private void TakeItem(int x, int y)
    {
        itemSelected = new Vector2Int(x, y);
        isIconSelected = true;
    }

    private void FillBoxes()
    {
        for (int x = 0; x < Size; x++)
            for (int y = 0; y < Size; y++)
                SetMap(x, y);
    }

    private void SetMap(int x, int y)
    {
        int item;
        do
        {
            item = random.Next(Items);            
        } 
        while (x > 1 && //���� x>1, �� ��������� ��� ������ �����, ����� �� ���� ��� ������
                map[x - 1, y] == map[x - 2, y] && 
                map[x - 2, y] == item ||
                y > 1 && //���� y>1, �� ��������� ��� ������ ������, ����� �� ���� ��� ������
                map[x, y - 1] == map[x, y - 2] &&
                map[x, y - 2] == item);        

        map[x, y] = item;
    }
}