using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class IconsAnimation
{
    private DG.Tweening.Sequence sequence;
    public IconsAnimation()
    {
        sequence = DOTween.Sequence();
        sequence.SetEase(Ease.Linear);
    }
    public async System.Threading.Tasks.Task Play()
    {
        await sequence.Play()
                      .AsyncWaitForCompletion();
    }

    /// <summary>
    /// Анимация падения элемента на первую линию
    /// </summary>
    /// <param name="button">элемент, которому задается новая иконка</param>
    /// <param name="sprite">новая иконка</param>
    /// <param name="fallDuration">длительность анимации</param>
    public void FallItem(Button button, Sprite sprite, float fallDuration)
    {
        Image image = GetObject.GetImage(button);
        image.transform.localPosition = new Vector3(0, 60, 0);
        image.transform.localScale = Vector3.one;
        image.sprite = sprite;
        sequence.Join(image.transform.DOLocalMove(Vector3.zero, fallDuration));
    }
    public void FallItem(Button button, Button button1, float fallDuration)
    {
        Image image = GetObject.GetImage(button);
        Image image0 = GetObject.GetImage(button1);

        image.transform.SetParent(button1.transform);
        sequence.Join(image.transform.DOLocalMove(Vector3.zero, fallDuration));

        image0.transform.SetParent(button.transform);
        image0.transform.localPosition = Vector3.zero;

    }
    public void Pop(Button button, float popDuration)
    {
        Image image = GetObject.GetImage(button);
        sequence.Join(image.transform.DOScale(Vector3.zero, popDuration)
                                     .OnComplete(() => { image.sprite = null; }));        
    }
    public static async System.Threading.Tasks.Task Swap(Button button1, Button button2, float tweenDuration)
    {

        Image image1 = GetObject.GetImage(button1);
        Image image2 = GetObject.GetImage(button2);

        Canvas canvas = GameObject.Find("Board").GetComponent<Canvas>();

        image1.transform.SetParent(canvas.transform);
        image2.transform.SetParent(canvas.transform);

        Transform image1Transform = image1.transform;
        Transform image2Transform = image2.transform;

        Sequence sequence = DOTween.Sequence();
        sequence.SetEase(Ease.Linear);

        sequence.Join(image1Transform.DOMove(image2Transform.position, tweenDuration)
                                     .OnComplete(() => { 
                                         image1Transform.SetParent(button2.transform); 
                                     }))
                .Join(image2Transform.DOMove(image1Transform.position, tweenDuration)
                                     .OnComplete(() => {
                                         image2Transform.SetParent(button1.transform);
                                     }));

        await sequence.Play()
                      .AsyncWaitForCompletion();
    }
}
