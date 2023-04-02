using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    [SerializeField] private Activity match3Activity;
    
    public int price;
    public Text text;

    bool isActive = false;
    Button button;
    void Start()
    {
        button = transform.GetChild(0).gameObject.GetComponent<Button>();
        switch (gameObject.transform.gameObject.name)
        {
            case "BuildMain":
                break;
            case "BuildMatchThree":
                if (Variables.Data.BuildInfo.BuildMatchThree) Activate();
                else
                {
                    text.text = price.ToString();
                }
                break;
            case "Build1":
                if (Variables.Data.BuildInfo.Build1) Activate();
                else
                {
                    text.text = price.ToString();
                }
                break;
            case "Build2":
                if (Variables.Data.BuildInfo.Build2) Activate();
                else
                {
                    text.text = price.ToString();
                }
                break;
            case "Build3":
                if (Variables.Data.BuildInfo.Build3) Activate();
                else
                {
                    text.text = price.ToString();
                }
                break;
            case "Build4":
                if (Variables.Data.BuildInfo.Build4) Activate();
                else
                {
                    text.text = price.ToString();
                }
                break;
            default: ShowMessage("Неизвестный объект");
                break;
        };
        
        
        
    }

    public void Buy()
    {
        if (!isActive)
        {
            if (Variables.Data.Cash >= price)
            {
                switch (gameObject.transform.gameObject.name)
                {
                    case "BuildMain": //TODO нажато на главное сдание
                        break;
                    case "BuildMatchThree":
                        Variables.Data.BuildInfo.BuildMatchThree = true;
                        break;
                    case "Build1":

                        Variables.Data.BuildInfo.Build1 = true;
                        break;
                    case "Build2":
                        Variables.Data.BuildInfo.Build2 = true;
                        break;
                    case "Build3":
                        Variables.Data.BuildInfo.Build3 = true;
                        break;
                    case "Build4":
                        Variables.Data.BuildInfo.Build4 = true;
                        break;
                    default:
                        ShowMessage("Неизвестное здание");
                        break;
                }
                Variables.AddCash(-price);
                Activate();
            }
            else 
            {
                if(Application.platform == RuntimePlatform.Android) ShowMessage("Недостаточно денег");
            }
            
        }              
    }

    public void Play()
    {
        switch (gameObject.transform.gameObject.name)
        {
            case "BuildMain": //TODO нажато на главное здание
                break;
            case "BuildMatchThree": 
                match3Activity.Activate();
                break;
            default: ShowMessage("Здание не работает");
                break;
        }
    }

    /// <summary>
    /// Вывод Toast на телефоне
    /// источник - https://stackoverflow.com/questions/52590525/how-to-show-a-toast-message-in-unity-similar-to-one-in-android
    /// </summary>
    /// <param name="message"></param>
    void ShowMessage(string message)
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
    public void Activate()
    {
        isActive = true;
        button.image.color = new Color(255, 255, 255, 255);
        button.transform.DetachChildren();
        button.onClick.AddListener(Play);
    }
}
