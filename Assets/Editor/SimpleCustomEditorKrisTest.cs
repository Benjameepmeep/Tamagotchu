using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleCustomEditorKrisTest : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private int m_ClickCount = 0;
    private const string m_ButtonPrefix = "button";

    [MenuItem("Window/UI Toolkit/SimpleCustomEditorKrisTest")]
    public static void ShowExample()
    {
        SimpleCustomEditorKrisTest wnd = GetWindow<SimpleCustomEditorKrisTest>();
        wnd.titleContent = new GUIContent("SimpleCustomEditorKrisTest");
    }

    public void CreateGUI()
    {
        var visualElement = new VisualElement();
visualElement.style.width = 100;
visualElement.style.height = 100;
visualElement.style.backgroundColor = new Color(1, 0, 0, 1); // Red color
visualElement.style.borderTopColor = Color.black;
visualElement.style.borderTopWidth = 2;
visualElement.style.borderLeftColor = Color.black;
visualElement.style.borderLeftWidth = 2;
visualElement.style.borderRightColor = Color.black;
visualElement.style.borderRightWidth = 2;
visualElement.style.borderBottomColor = Color.black;
visualElement.style.borderBottomWidth = 2;
rootVisualElement.Add(visualElement);


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

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        //Call the event handler
        SetupButtonHandler();
    }

    //Functions as the event handlers for your button click and number counts
    private void SetupButtonHandler(){
        VisualElement root = rootVisualElement;

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterHandler);
    }

    private void RegisterHandler(Button button){
        button.RegisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt){
        VisualElement root = rootVisualElement;

        ++m_ClickCount;

        //Because of the names we gave the buttons and toggles, we can use the button name to find the toggle name.
        Button button = evt.currentTarget as Button;
        string buttonNumber = button.name.Substring(m_ButtonPrefix.Length);
        string toggleName = "toggle" + buttonNumber;
        Toggle toggle = root.Q<Toggle>(toggleName);

        Debug.Log("Button was clicked!" + (toggle.value ? " Count: " + m_ClickCount : ""));
    }
}
