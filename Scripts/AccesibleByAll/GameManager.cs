//Made by: Mathias Sorin
//Last updated: 11/05/2021

using UnityEngine;

//Singleton game manager
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Easily accessible classes
    [HideInInspector] public NetworkConnectionManager networkConnectionManager;
    [HideInInspector] public SceneLoader sceneLoader;
    [HideInInspector] public AbilityCatalog abilityCatalog;
    [HideInInspector] public FloatingTextDisplay floatingTextDisplay;
    public ObjectSpawning objectSpawning;

    //Local player components
    [HideInInspector] public GameObject localPlayer;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public Camera playerCamera;

    //Player input variables
    [HideInInspector] public Vector2 mousePosition;
    [HideInInspector] public bool mouseLeftClick;
    [HideInInspector] public bool mouseRightClick;

    //Spawn point for players
    [HideInInspector] public Transform spawnPoint;

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

        networkConnectionManager = GetComponent<NetworkConnectionManager>();
        sceneLoader = GetComponent<SceneLoader>();
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
