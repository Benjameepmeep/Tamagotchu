using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BenjaminSpriteSelector : EditorWindow
{
    [SerializeField] private int selectedIndex = -1;
   
    [MenuItem("Window/UI Toolkit/BenjaminSpriteSelector")]
    public static void ShowExample()
    {
        BenjaminSpriteSelector wnd = GetWindow<BenjaminSpriteSelector>();
        wnd.titleContent = new GUIContent("BenjaminSpriteSelector");
        wnd.minSize = new Vector2(450, 200);
        wnd.maxSize = new Vector2(1920, 720);
    }

    private VisualElement _rightPane;

    public void CreateGUI()
    {
        // Get a list of all sprites in the project
        var spriteObjectGuids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets" });
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
        _rightPane = new ScrollView(ScrollViewMode.VerticalAndHorizontal);
        splitView.Add(_rightPane);

        // Initialize the list view with all sprites' names
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allSpriteObjects[index].name; };
        leftPane.itemsSource = allSpriteObjects;

        // React to the user's selection
        leftPane.selectionChanged += OnSpriteSelectionChange;

        // Restore the selection index from before the hot reload.
        leftPane.selectedIndex = selectedIndex;

        // Store the selection index when the selection changes.
        leftPane.selectionChanged += (_) => selectedIndex = leftPane.selectedIndex;

        // .selectionChanged is an event that expects a delegate / callback function with a signature. 
        // Specifially a signature with a parameter signifying a group of items selected, which we don't use specifically. 
        // We only use .selectedIndex from the item/object inside this group.
        // Therefore we can omit the parameter inside the () with (_), to show we do not use this parameter.

        // Alternative way to write the above line:
            // leftPane.selectionChanged += (items) => { selectedIndex = leftPane.selectedIndex; };

        // If we wanted to use the "(items)" parameter in this list, we could write a function like the below to log each item selected.

            // leftPane.selectionChanged += (items) => {
            // foreach (var item in items)
            // {
            //    Debug.Log("Selected item: " + item);
            // }
            // selectedIndex = leftPane.selectedIndex;
            // };
    }

    private void LeftPane_selectionChanged(IEnumerable<object> obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnSpriteSelectionChange(IEnumerable<object> selectedItems)
    {
        // Clear all previous content from the pane.
        _rightPane.Clear();

        // Get the selected sprite and display it.
        var enumerator = selectedItems.GetEnumerator();
        if (enumerator.MoveNext())
        {
            var selectedSprite = enumerator.Current as Sprite;
            if (selectedSprite != null)
            {
                // Add a new Image control and display the sprite.
                var spriteImage = new Image();
                spriteImage.scaleMode = ScaleMode.ScaleToFit;
                spriteImage.sprite = selectedSprite;

                // Add the Image control to the right-hand pane.
                _rightPane.Add(spriteImage);
            }

        }
    }
}
