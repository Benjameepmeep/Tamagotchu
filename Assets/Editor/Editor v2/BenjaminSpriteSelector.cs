using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BenjaminSpriteSelector : EditorWindow
{
    [MenuItem("Window/UI Toolkit/BenjaminSpriteSelector")]
    public static void ShowExample()
    {
        BenjaminSpriteSelector wnd = GetWindow<BenjaminSpriteSelector>();
        wnd.titleContent = new GUIContent("BenjaminSpriteSelector");
    }

    public void CreateGUI()
    {
        // Get a list of all sprites in the project
        string [] spriteObjectGuids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/Sprites" });
        var allSpriteObjects = new List<Sprite>();
        foreach (var guid in spriteObjectGuids)
        {
            allSpriteObjects.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        // Create a two-pane view with the left pane being fixed.
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        // Add the view to the visual tree by adding it as a child to the root element.
        rootVisualElement.Add(splitView);

        // A TwoPaneSplitView needs exactly two child elements.
        var leftPane = new ListView();
        splitView.Add(leftPane); 
        var rightPane = new VisualElement();
        splitView.Add(rightPane);

        // Initialize the list view with all sprites' names
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allSpriteObjects[index].name; };
        leftPane.itemsSource = allSpriteObjects;
    }
}
