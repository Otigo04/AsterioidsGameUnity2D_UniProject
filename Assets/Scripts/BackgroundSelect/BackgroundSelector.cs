using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundSelector : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image[] previewImages;
    void Start()
    {
        for (int i = 0; i < previewImages.Length; i++) 
        {
            int index = i;
            previewImages[i].sprite = backgrounds[i];
            previewImages[i].GetComponent<Button>().onClick.AddListener(() => SelectBackground(index));
        }

        GameObject bg = GameObject.Find("Background");
        bg.GetComponent<SpriteRenderer>().sprite = _Gamemanager.Instance.selectedBackground;
    }

    void SelectBackground(int index) 
    {
        _Gamemanager.Instance.selectedBackground = backgrounds[index];
        SceneManager.LoadScene("GameScene"); // Die gamescene wird hier geladen
    }

    
    void Update()
    {
        
    }
}
