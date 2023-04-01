using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class GetObject
{
    public static Button[,] InitButtons(int Size)
    {
        Button[,] buttons = new Button[Size, Size];
        for (int numberBox = 0; numberBox < Size * Size; numberBox++)
            buttons[numberBox % Size, numberBox / Size] =
                GameObject.Find($"Item ({numberBox})").GetComponent<Button>();
        return buttons;
    }

    public static int GetNumber(string name)
    {
        Regex regex = new("\\((\\d+)\\)");
        Match match = regex.Match(name);
        if (!match.Success)
            throw new Exception("Ячейка не найдена");
        Group group = match.Groups[1];
        string str = group.Value;
        return Convert.ToInt32(str);
    }

    public static Image GetImage(Button button)
    {
        return button.GetComponent<Image>().transform.Find("Icon").GetComponent<Image>();
    }
}

