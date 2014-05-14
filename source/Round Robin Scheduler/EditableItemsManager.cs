using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SomeTechie.RoundRobinScheduler
{
    public partial class EditableItemsManager : UserControl
    {
        public event EventHandler ItemsChanged;
        public event ItemsChangedEventHandler ItemsRemoved;
        public event ItemsChangedEventHandler ItemsAdded;
        public event ItemsChangedEventHandler ItemChanged;
        public event ItemMovedEventHandler ItemMoved;

        protected string _itemStringFormat = "{0}";
        public string ItemStringFormat
        {
            get
            {
                return _itemStringFormat;
            }
            set
            {
                _itemStringFormat = value;
            }
        }

        protected bool _LockRearrange = false;
        public bool LockRearrange
        {
            get
            {
                return _LockRearrange;
            }
            set
            {
                if (_LockRearrange != value)
                {
                    _LockRearrange = value;
                    updwnItemsCount.Enabled = !_LockRearrange;
                    btnAddItem.Enabled = !_LockRearrange;
                    btnRemoveItem.Enabled = !_LockRearrange;
                    btnMoveItemUp.Enabled = !_LockRearrange;
                    btnMoveItemDown.Enabled = !_LockRearrange;
                }
            }
        }

        protected string _numItemsText = "Number of items";
        public string NumItemsText
        {
            get
            {
                return _numItemsText;
            }
            set
            {
                _numItemsText = value;
            }
        }

        protected string _itemsText = "Items:";
        public string ItemsText
        {
            get
            {
                return _itemsText;
            }
            set
            {
                _itemsText = value;
            }
        }

        public ListBox.ObjectCollection Items
        {
            get
            {
                return lstItems.Items;
            }
        }

        public List<string> ItemsList
        {
            get
            {
                List<string> list = new List<string>();
                System.Collections.IEnumerator ItemsEnumerator = Items.GetEnumerator();
                while (ItemsEnumerator.MoveNext())
                {
                    list.Add(ItemsEnumerator.Current.ToString());
                }
                return list;
            }
            set
            {
                List<object> toRemove = new List<object>();
                for (int i = Items.Count-1; i >= value.Count; i--)
                {
                    toRemove.Add(Items[i]);
                }
                if (toRemove.Count > 0) lstItems.RemoveItems(toRemove, true);

                List<object> toAdd = new List<object>();
                for (int i = 0; i < value.Count;i++ )
                {
                    if (Items.Count > i)
                    {
                        bool needRefreshEditBox = lstItems.SelectedIndex == i;
                        lstItems.endEdit();
                        Items[i] = value[i];
                        lstItems.beginEdit(i);
                    }
                    else toAdd.Add(value[i]);
                }
                if (toAdd.Count > 0) lstItems.AddItems(toAdd, true);
            }
        }
        
        public EditableItemsManager()
        {
            InitializeComponent();
        }

        private void EditableItemsManager_Load(object sender, EventArgs e)
        {
            updateItemsList();
            refreshButtonEnablement();
            lstItems.beginEdit(0);
            lblNumItems.Text = NumItemsText;
            lblITems.Text = ItemsText;
        }

        private void updwnItemCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_LockRearrange)
            {
                updateItemsList();
                refreshButtonEnablement();
            }
            else updwnItemsCount.Value = lstItems.Items.Count;
        }

        private void updateItemsList()
        {
            int targetItemsCount = (int)updwnItemsCount.Value;

            List<object> toRemove = new List<object>();
            for (int i = lstItems.Items.Count - 1; i >= targetItemsCount; i--)
            {
                toRemove.Add(lstItems.Items[i]);
            }
            if (toRemove.Count > 0)lstItems.RemoveItems(toRemove);

            List<object> toAdd = new List<object>();
            for (int i = lstItems.Items.Count; i < targetItemsCount; i++)
            {
                toAdd.Add(String.Format(this.ItemStringFormat, i + 1));
            }
            if (toAdd.Count > 0)lstItems.AddItems(toAdd);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!_LockRearrange)lstItems.AddItem(String.Format(this.ItemStringFormat, lstItems.Items.Count + 1));
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (!_LockRearrange && lstItems.SelectedIndex >= 0 && lstItems.SelectedIndex < lstItems.Items.Count && lstItems.Items.Count>updwnItemsCount.Minimum) lstItems.RemoveItemAt(lstItems.SelectedIndex);
        }

        private void lstItems_UserChangedCount(object sender, EventArgs e)
        {
            if (!_LockRearrange)
            {
                updateItemCount();
                if (ItemsChanged != null) ItemsChanged(this, new EventArgs());
            }
        }
        private void updateItemCount()
        {
            if (lstItems.Items.Count < updwnItemsCount.Minimum)
            {
                updwnItemsCount.Value = updwnItemsCount.Minimum;
                updateItemsList();
            }
            else
            {
                updwnItemsCount.Value = lstItems.Items.Count;
            }
            refreshButtonEnablement();
        }

        private void RelocateItem(int currentIndex, int newIndex)
        {
            if (currentIndex > -1 && currentIndex < lstItems.Items.Count &&
                newIndex > -1 && newIndex < lstItems.Items.Count)
            {
                lstItems.endEdit();
                object item = lstItems.Items[currentIndex];
                lstItems.Items.RemoveAt(currentIndex);
                lstItems.Items.Insert(newIndex, item);
                lstItems.SelectedIndex = newIndex;
                refreshButtonEnablement();
                if (ItemMoved != null) ItemMoved(this, new ItemMovedEventArgs(lstItems.Items[newIndex], currentIndex, newIndex));
                if (ItemsChanged != null) ItemsChanged(this, new EventArgs());
            }
        }

        private void btnMoveItemUp_Click(object sender, EventArgs e)
        {
            if (!_LockRearrange)RelocateItem(lstItems.SelectedIndex, lstItems.SelectedIndex - 1);
        }

        private void btnMoveItemDown_Click(object sender, EventArgs e)
        {
            if (!_LockRearrange)RelocateItem(lstItems.SelectedIndex, lstItems.SelectedIndex + 1);
        }

        private void refreshButtonEnablement()
        {
            if (!_LockRearrange)
            {
                this.btnMoveItemUp.Enabled = lstItems.SelectedIndex > 0;
                this.btnMoveItemDown.Enabled = lstItems.SelectedIndex >= 0 && lstItems.SelectedIndex < lstItems.Items.Count - 1;
                this.btnRemoveItem.Enabled = lstItems.SelectedIndex >= 0 && lstItems.SelectedIndex < lstItems.Items.Count && lstItems.Items.Count > updwnItemsCount.Minimum;
            }
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshButtonEnablement();
        }

        private void EditableItemsManager_Leave(object sender, EventArgs e)
        {
            lstItems.endEdit();
        }

        private void lstItems_ItemAdded(object sender, ItemsChangedEventArgs e)
        {
            if(ItemsAdded!=null)ItemsAdded(this, e);
        }

        private void lstItems_ItemChanged(object sender, ItemsChangedEventArgs e)
        {
            if (ItemChanged != null) ItemChanged(this, e);
        }

        private void lstItems_ItemRemoved(object sender, ItemsChangedEventArgs e)
        {
            if (ItemsRemoved != null) ItemsRemoved(this, e);
        }

        private void lstItems_ItemsChanged(object sender, EventArgs e)
        {
            updateItemCount();
        }

        private void EditableItemsManager_Enter(object sender, EventArgs e)
        {
            lstItems.Focus();
        }
    }

    public delegate void ItemMovedEventHandler(object sender, ItemMovedEventArgs e);
    public class ItemMovedEventArgs : EventArgs
    {
        protected object _item;
        public object Item
        {
            get
            {
                return _item;
            }
        }
        protected int _oldIndex;
        public int OldIndex
        {
            get
            {
                return _oldIndex;
            }
        }
        protected int _newIndex;
        public int NewIndex
        {
            get
            {
                return _newIndex;
            }
        }
        public ItemMovedEventArgs(object item, int oldIndex, int newIndex)
        {
            _item = item;
            _oldIndex = oldIndex;
            _newIndex = newIndex;
        }
    }
}
