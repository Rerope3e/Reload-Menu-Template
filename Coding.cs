using BepInEx;
using UnityEngine;
using UnityEngine.UI;

[BepInPlugin("com.yourname.reloadmenu", "Reload Menu", "1.0.0")]
public class ReloadMenu : BaseUnityPlugin
{
    private GameObject canvas;
    private GameObject panel;
    private bool isMenuVisible = false;

    private bool isFlying = false;
    private bool hasPlatforms = false;
    private bool isSpeedBoost = false;
    private bool isGhostMonkey = false;

    private void OnEnable()
    {
        Logger.LogInfo("Reload Menu Loaded");
        CreateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isMenuVisible = !isMenuVisible;
            canvas.SetActive(isMenuVisible);
        }

        if (isFlying)
        {
            // Implement flying logic
            var player = GameObject.Find("Player");
            player.transform.position += Vector3.up * Time.deltaTime * 5; // Example fly logic
        }

        if (isSpeedBoost)
        {
            // Implement speed boost logic
            var player = GameObject.Find("Player");
            player.GetComponent<CharacterController>().moveSpeed = 20f; // Example speed boost logic
        }

        // Ensure features only work in modded lobbies
        if (!IsInModdedLobby())
        {
            DisableAllFeatures();
        }
    }

    private void CreateUI()
    {
        // Create a new Canvas
        canvas = new GameObject("ReloadMenuCanvas");
        var canvasComponent = canvas.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        DontDestroyOnLoad(canvas);

        // Create a Panel
        panel = new GameObject("Panel");
        panel.transform.SetParent(canvas.transform);
        var panelRectTransform = panel.AddComponent<RectTransform>();
        panelRectTransform.sizeDelta = new Vector2(300, 400);
        var panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.5f);  // Semi-transparent background

        // Create buttons for each feature
        CreateButton("Fly", new Vector2(0, 100), () => ToggleFeature(ref isFlying));
        CreateButton("Platforms", new Vector2(0, 50), () => ToggleFeature(ref hasPlatforms));
        CreateButton("Speed Boost", new Vector2(0, 0), () => ToggleFeature(ref isSpeedBoost));
        CreateButton("Ghost Monkey", new Vector2(0, -50), () => ToggleFeature(ref isGhostMonkey));

        canvas.SetActive(false);
    }

    private void CreateButton(string buttonText, Vector2 position, UnityEngine.Events.UnityAction onClickAction)
    {
        var button = new GameObject(buttonText + "Button");
        button.transform.SetParent(panel.transform);
        var buttonRectTransform = button.AddComponent<RectTransform>();
        buttonRectTransform.sizeDelta = new Vector2(200, 50);
        buttonRectTransform.anchoredPosition = position;

        var buttonImage = button.AddComponent<Image>();
        buttonImage.color = Color.white;

        var buttonComponent = button.AddComponent<Button>();
        buttonComponent.onClick.AddListener(onClickAction);

        var text = new GameObject("Text");
        text.transform.SetParent(button.transform);
        var textComponent = text.AddComponent<Text>();
        textComponent.text = buttonText;
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.alignment = TextAnchor.MiddleCenter;
        var textRectTransform = text.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(200, 50);
        textRectTransform.anchoredPosition = new Vector2(0, 0);
    }
... (23 lines left)
