//Made by: Mathias Sorin
//Last updated: 11/05/2021

using UnityEngine;

//Singleton game manager
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Easily accessible classes
    public AbilityCatalog abilityCatalog;
    public FloatingTextDisplay floatingTextDisplay;

    //Local player components
    public GameObject localPlayer;
    public Transform playerTransform;
    public Camera playerCamera;

    //Spawn point for players
    public Transform spawnPoint;

    //Player input variables
    public Vector2 mousePosition;
    public bool mouseLeftClick;
    public bool mouseRightClick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        abilityCatalog = GetComponent<AbilityCatalog>();
        floatingTextDisplay = GetComponent<FloatingTextDisplay>();
    }

    public void UpdateMouseData(Vector2 newMousePosition, bool newMouseLeftClick, bool newMouseRightClick)
    {
        mousePosition = newMousePosition;
        mouseLeftClick = newMouseLeftClick;
        mouseRightClick = newMouseRightClick;
    }
}
