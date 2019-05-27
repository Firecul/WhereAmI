using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using MenuAPI;
using System.Drawing;

namespace TestMenu
{
    public class ExampleMenu : BaseScript
    {

		public bool ShowPlayerBlips { get; private set; } = true;
        public bool HideRadar { get; private set; } = false;

		public ExampleMenu()
        {
            Tick += OnTick;
        }

		private async Task OnTick()
		{
			await Task.FromResult(0);

			if (IsControlJustReleased(1, 166))
			{
				if (MenuController.IsAnyMenuOpen())
				{
					MenuController.CloseAllMenus();
				}
				else
				{
					MenuController.MenuToggleKey = (Control)(-1);
					ExampleMenu1();
				}
			}

		}


		private async Task MiscSettings()
		{

			if (HideRadar)
			{
				DisplayRadar(true);
			}
			// Show radar (or hide it if the user disabled it in pausemenu > settings > display > show radar.
			else if (!IsRadarHidden()) // this should allow other resources to still disable it
			{
				DisplayRadar(IsRadarPreferenceSwitchedOn());
			}
            else
            {
                await Delay(0);
            }
        }










        public void ExampleMenu1()
        {
            // Setting the menu alignment to be right aligned. This can be changed at any time and it'll update instantly.
            // To test this, checkout one of the checkbox items in this example menu. Clicking it will toggle the menu alignment.
            MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Right;

            // Creating the first menu.
            Menu menu = new Menu("Map Menu", "by Firecul") { Visible = true };
            MenuController.AddMenu(menu);


			MenuCheckboxItem hideRadar = new MenuCheckboxItem("Hide Radar", "Hide the radar/minimap.", HideRadar);

			MenuCheckboxItem playerBlips = new MenuCheckboxItem("Show Player Blips", "Shows blips on the map for all players.", ShowPlayerBlips);

            // Creating 3 checkboxs, 2 different styles and one has a locked icon and it's 'not enabled' (not enabled meaning you can't toggle it).
            MenuCheckboxItem box = new MenuCheckboxItem("Checkbox - Style 1 (click me!)", "This checkbox can toggle the menu position! Try it out.", !menu.LeftAligned)
            {
                Style = MenuCheckboxItem.CheckboxStyle.Tick
            };

            MenuCheckboxItem box2 = new MenuCheckboxItem("Checkbox - Style 2", "This checkbox does nothing right now.", true)
            {
                Style = MenuCheckboxItem.CheckboxStyle.Cross
            };

            MenuCheckboxItem box3 = new MenuCheckboxItem("Checkbox (unchecked + locked)", "Make this menu right aligned. If you set this to false, then the menu will move to the left.", false)
            {
                Style = MenuCheckboxItem.CheckboxStyle.Cross,
                Enabled = false,
                LeftIcon = MenuItem.Icon.LOCK
            };

            // Adding the checkboxes to the menu.
            menu.AddMenuItem(hideRadar);
			menu.AddMenuItem(playerBlips);
            menu.AddMenuItem(box);
            menu.AddMenuItem(box2);
            menu.AddMenuItem(box3);


            /* Instructional buttons setup for the second (submenu) menu.
            submenu.InstructionalButtons.Add(Control.CharacterWheel, "Right?!");
            submenu.InstructionalButtons.Add(Control.CreatorLS, "Buttons");
            submenu.InstructionalButtons.Add(Control.CursorScrollDown, "Cool");
            submenu.InstructionalButtons.Add(Control.CreatorDelete, "Out!");
            submenu.InstructionalButtons.Add(Control.Cover, "This");
            submenu.InstructionalButtons.Add(Control.Context, "Check"); */

           


            /*
             ########################################################
                                 Event handlers
             ########################################################
            */


            menu.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                // Code in here gets executed whenever a checkbox is toggled.
                Debug.WriteLine($"OnCheckboxChange: [{_menu}, {_item}, {_index}, {_checked}]");

                // If the align-menu checkbox is toggled, toggle the menu alignment.
                if (_item == box)
                {
                    if (_checked)
                    {
                        MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Right;
                    }
                    else
                    {
                        MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Left;
                    }
                }
                else if (_item == hideRadar)
                {
                    HideRadar = _checked;
                    if (!_checked)
                    {
                        DisplayRadar(true);
                    }
                }
                else if (_item == playerBlips)
                {
                    ShowPlayerBlips = _checked;
                }
            };

            menu.OnItemSelect += (_menu, _item, _index) =>
            {
                // Code in here would get executed whenever an item is pressed.
                Debug.WriteLine($"OnItemSelect: [{_menu}, {_item}, {_index}]");
            };

            menu.OnIndexChange += (_menu, _oldItem, _newItem, _oldIndex, _newIndex) =>
            {
                // Code in here would get executed whenever the up or down key is pressed and the index of the menu is changed.
                Debug.WriteLine($"OnIndexChange: [{_menu}, {_oldItem}, {_newItem}, {_oldIndex}, {_newIndex}]");
            };

            menu.OnListIndexChange += (_menu, _listItem, _oldIndex, _newIndex, _itemIndex) =>
            {
                // Code in here would get executed whenever the selected value of a list item changes (when left/right key is pressed).
                Debug.WriteLine($"OnListIndexChange: [{_menu}, {_listItem}, {_oldIndex}, {_newIndex}, {_itemIndex}]");
            };

            menu.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                // Code in here would get executed whenever a list item is pressed.
                Debug.WriteLine($"OnListItemSelect: [{_menu}, {_listItem}, {_listIndex}, {_itemIndex}]");
            };

            menu.OnSliderPositionChange += (_menu, _sliderItem, _oldPosition, _newPosition, _itemIndex) =>
            {
                // Code in here would get executed whenever the position of a slider is changed (when left/right key is pressed).
                Debug.WriteLine($"OnSliderPositionChange: [{_menu}, {_sliderItem}, {_oldPosition}, {_newPosition}, {_itemIndex}]");
            };

            menu.OnSliderItemSelect += (_menu, _sliderItem, _sliderPosition, _itemIndex) =>
            {
                // Code in here would get executed whenever a slider item is pressed.
                Debug.WriteLine($"OnSliderItemSelect: [{_menu}, {_sliderItem}, {_sliderPosition}, {_itemIndex}]");
            };

            menu.OnMenuClose += (_menu) =>
            {
                // Code in here gets triggered whenever the menu is closed.
                Debug.WriteLine($"OnMenuClose: [{_menu}]");
            };

            menu.OnMenuOpen += (_menu) =>
            {
                // Code in here gets triggered whenever the menu is opened.
                Debug.WriteLine($"OnMenuOpen: [{_menu}]");
            };


        }
    }
}
