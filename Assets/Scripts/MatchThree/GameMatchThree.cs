using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameMatchThree : MonoBehaviour
{
    public float TweenDuration;
    public float PopDuration;
    public float FallDuration;
    public int Size;
    public Text ScoreLabel;
    public Color32 colorItem;// = new(150, 68, 40, 255);
    public Color32 colorSelectedItem;// = new(75, 34, 20, 255);


    Button[,] buttons;
    static Icon[] images;
    MatchThree matchThree;

    bool canClick = true;
    bool needFall = false;

    Button itemSelected = null;

    public float ScoreFactor;
    public void Exit()
    {
        Variables.AddCash(int.Parse(ScoreLabel.text) * ScoreFactor);
        SceneManager.LoadScene("TownMap");
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize() => images = Resources.LoadAll<Icon>("MatchThree/Icons/");

    void Start()
    {
        matchThree = new MatchThree(Size, images.Length);

        buttons = GetObject.InitButtons(Size);
        
        FillBoxes();
    }



    private void FillBoxes()
    {
        foreach (Button item in buttons)
        {
            int n = GetObject.GetNumber(item.name);
            int x = n % Size;
            int y = n / Size;
            GetObject.GetImage(item). sprite = images[matchThree.GetIcon(x, y)].sprite;
        }
    }

    public async void Click()
    {
        if (!canClick) return;

        int n = GetObject.GetNumber(EventSystem.current.currentSelectedGameObject.name);
        int x = n % Size;
        int y = n / Size;
        if (matchThree.CanMove(x, y))
        {
            canClick = false;

            itemSelected.GetComponent<Image>().color = colorItem;
            
            await IconsAnimation.Swap(buttons[x, y], itemSelected, TweenDuration);

            await DeleteItems();
            if (needFall)
            {
                do
                {
                    await FallIcons();
                    await DeleteItems();
                }
                while (needFall);
            }
            else
            {
                int numberSelectedItem = GetObject.GetNumber(itemSelected.name);
                matchThree.SwapItems(new Vector2Int(x, y), new Vector2Int(numberSelectedItem % Size, numberSelectedItem / Size));
                await IconsAnimation.Swap(itemSelected, buttons[x, y], TweenDuration);

            }

            itemSelected = null;
            canClick = true;
        }
        else
        {

            if (itemSelected != null) itemSelected.GetComponent<Image>().color = colorItem;
            itemSelected = buttons[x, y];
            itemSelected.GetComponent<Image>().color = colorSelectedItem;
        }
        
    }

    private async Task FallIcons()
    {
        List<Vector2> itemsFall;
        List<Vector3> itemsFallNew;        
        do
        {
            IconsAnimation animation = new();
            itemsFall = matchThree.FallItems();
            itemsFallNew = matchThree.FallNewItems();

            foreach (var itemXY in itemsFall)
            {
                animation.FallItem(buttons[(int)itemXY.x, (int)itemXY.y], buttons[(int)itemXY.x, (int)itemXY.y + 1], FallDuration);
            }
            foreach (var itemXYI in itemsFallNew)
            {
                animation.FallItem(buttons[(int)itemXYI.x, (int)itemXYI.y], images[(int)itemXYI.z].sprite, FallDuration);
            }
            await animation.Play();



        } while (!(itemsFall.Count == 0 && itemsFallNew.Count == 0));
        

        
    }

    private async Task DeleteItems()
    {
        List<Vector2> itemsToDelete = matchThree.GetItemsToDelete();

        if (itemsToDelete == null)
        {
            needFall = false;
            return;
        }
        needFall = true;

        int score;
        try
        {
            score = int.Parse(ScoreLabel.text);
        }
        catch
        {
            score = 0;
        }
        IconsAnimation animation = new();
        
        foreach (Vector2 item in itemsToDelete)
        {
            score += 1;
            animation.Pop(buttons[(int)item.x, (int)item.y], PopDuration);
        }
        await animation.Play();
        ScoreLabel.text = score.ToString();
    }
}
