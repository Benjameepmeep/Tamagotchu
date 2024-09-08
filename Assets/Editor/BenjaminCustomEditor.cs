using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BenjaminCustomEditor : EditorWindow
{


    [MenuItem("Window/UI Toolkit/BenjaminEditor")]
    public static void ShowExample()
    {
        BenjaminCustomEditor wnd = GetWindow<BenjaminCustomEditor>();
        wnd.titleContent = new GUIContent("BenjaminEditor");
    }

    [SerializeField]
    private VisualTreeAsset _uXMLTree = default;

    private int _clickCount = 0;

    private const string buttonPrefix = "button";

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("These controls were created using C# code.");
        root.Add(label);

        Button button = new Button();
        button.name = "button3";
        button.text = "This is button3.";
        root.Add(button);

        Toggle toggle = new Toggle();
        toggle.name = "toggle3";
        toggle.label = "Number?";
        root.Add(toggle);

        // Instantiate UXML created automatically which is set at the default VisualTreeAsset.
        root.Add(_uXMLTree.Instantiate());

        // Import UXML created manually.
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BenjaminCustomEditor_UXML.uxml");
        VisualElement labelFromUXML_uxml = visualTree.Instantiate();
        root.Add(labelFromUXML_uxml);

        //Call the event handler
        SetupButtonHandler();
    }

    //Functions as the event handlers for your button click and number counts
    private void SetupButtonHandler()
    {
        VisualElement root = rootVisualElement;

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterHandler);
    }

    private void RegisterHandler(Button button)
    {
        button.RegisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt)
    {
        VisualElement root = rootVisualElement;

        ++_clickCount;

        //Because of the names we gave the buttons and toggles, we can use the
        //button name to find the toggle name.
        Button button = evt.currentTarget as Button;
        string buttonNumber = button.name.Substring(buttonPrefix.Length);
        string toggleName = "toggle" + buttonNumber;
        Toggle toggle = root.Q<Toggle>(toggleName);

        Debug.Log("Button was clicked!" + (toggle.value ? " Count: " + _clickCount : ""));
    }
}
