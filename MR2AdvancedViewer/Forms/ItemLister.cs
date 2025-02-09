﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MR2AdvancedViewer.Forms
{
    public partial class ItemLister : Form
    {
        public Button[] ItemButtons = new Button[20];
        public int[] itemIDs = new int[20];
        public int selectedButton;
        public ViewerWindow AVW;
        public string[] rawdata_item = null;

        public ItemLister()
        {
            InitializeComponent();
        }

        public void ParseItemList(int arrayslot)
        {
            if (rawdata_item.Length >= arrayslot)
            {
                string[] scratchdata = rawdata_item[arrayslot].Split('|');
                PurchasePrice.Text = scratchdata[5];
                SalePrice.Text = scratchdata[4];
                ItemTypeDesc.Text = scratchdata[1];
                ItemDescription.Text = scratchdata[2];
                ItemEffect.Text = scratchdata[3];
            }
        }

        private void ItemLister_Load(object sender, EventArgs e)
        {
            Button button;
            for (int i = 0; i < 20; i++)
            {
                button = new Button
                {
                    Tag = "BTN_ID_" + i
                };
                button.Width = 120;
                ButtonPanel.Controls.Add(button);
                ItemButtons[i] = button;
            }
        }

        public void ItemListUpdate()
        {
            if (AVW != null && rawdata_item == null)
                rawdata_item = AVW.MR2ReadDataFile(@"data\en_van\mr2_item_list.txt");
            for (int i = 0; i < 20; i++)
            {
                ItemButtons[i].Text = (i + 1) + ": " + AVW.MonDesireNames(itemIDs[i]);
                ItemButtons[i].Click += new System.EventHandler(ClickButton);
            }
        }

        public void ClickButton(Object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            selectedButton = int.Parse(btn.Text.Substring(0, btn.Text.IndexOf(@":"))) - 1;

            if (itemIDs[selectedButton] >= 178)
            {
                SalePrice.Text = "-----";
                PurchasePrice.Text = "-----";
                ItemTypeDesc.Text = "No Item";
                ItemDescription.Text = "No item selected.";
                ItemEffect.Text = "This is a blank slot.";
                //Some other stuff to blank it
            }
            else
            {
                ParseItemList(itemIDs[selectedButton]);
            }
        }
    }
}
