//Based on the following author's code
/* 
 * This class was written by Nishant S [nishforever@vsnl.com]
 * You may freely use this class in your freeware, shareware or even
 * commercial applications. But you must retain this header information.
 * Feel free to report any bugs or suggestions you have :-)  
*/ 

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SomeTechie.RoundRobinScheduler
{
	public class EditableListBox : System.Windows.Forms.ListBox
	{
        public event EventHandler UserChangedCount;
        public event ItemsChangedEventHandler ItemsRemoved;
        public event ItemsChangedEventHandler ItemsAdded;
        public event ItemsChangedEventHandler ItemsEdited;
        public event EventHandler Scroll;
        public event EventHandler ItemsCollectionChanged;
        protected bool enableWndProcItemsChangedEvent = true;

        public override int SelectedIndex
        {
            get
            {
                return base.SelectedIndex;
            }
            set
            {
                base.SelectedIndex = value;
                if(value>=0)tbox.beginEdit(value);
            }
        }

        // WM_VSCROLL message constants
        private const int WM_VSCROLL = 0x0115;
        private const int SB_THUMBTRACK = 5;
        private const int SB_ENDSCROLL = 8;
        //Items changed message constants
        private const int LB_ADDSTRING = 0x180;
        private const int LB_INSERTSTRING = 0x181;
        private const int LB_DELETESTRING = 0x182;
        private const int LB_RESETCONTENT = 0x184;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Trap the WM_VSCROLL message to generate the Scroll event
            if (m.Msg == WM_VSCROLL)
            {
                int nfy = m.WParam.ToInt32() & 0xFFFF;
                if (nfy == SB_THUMBTRACK || nfy == SB_ENDSCROLL)
                {
                    if (Scroll != null) Scroll(this, new EventArgs());
                }
            }
            // Trap the items changed messages to generate the ItemsChanged event
            if ((m.Msg == LB_ADDSTRING ||
               m.Msg == LB_INSERTSTRING ||
               m.Msg == LB_DELETESTRING ||
               m.Msg == LB_RESETCONTENT) &&
               enableWndProcItemsChangedEvent){
            
                if (ItemsCollectionChanged != null) ItemsCollectionChanged(this, new EventArgs());
            }
        }

        public EditableListBox()
        {
            this.DrawMode = DrawMode.Normal;
            this.ScrollAlwaysVisible = true;
            tbox = new NTextBox(this);
            tbox.Hide();
            Controls.Add(tbox);
            tbox.EditEnded += new EventHandler(tbox_EditEnded);
            tbox.ItemChanged += new ItemsChangedEventHandler(tbox_ItemChanged);
        }

        void tbox_ItemChanged(object sender, ItemsChangedEventArgs e)
        {
            ItemsEdited(this, e);
        }

        void tbox_EditEnded(object sender, EventArgs e)
        {
            if (tbox.index == SelectedIndex)
            {
                base.SelectedIndex = -1;
            }
        }

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if(Site!=null)
				return;
          
            if(e.Index > -1)
			{
				string s = Items[e.Index].ToString();							
			
					e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window),e.Bounds);

                    // Center the text vertically
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(s,Font,new SolidBrush(SystemColors.WindowText),
                        e.Bounds, stringFormat);				
					
                    e.Graphics.DrawRectangle(new Pen(SystemColors.Highlight),e.Bounds);	
			}
        }

        // Handle the MeasureItem event for an owner-drawn ListBox. 
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)Math.Round(this.Font.GetHeight(e.Graphics)) + 6;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int index = IndexFromPoint(e.X, e.Y);

            if (index != ListBox.NoMatches &&
                index != 65535)
            {
                tbox.beginEdit(index);
            }

            base.OnMouseClick(e);
        }

        NTextBox tbox;

		class NTextBox : TextBox
		{
            public event EventHandler EditEnded;
            public event ItemsChangedEventHandler ItemChanged;

            protected bool _isActive = false;
            public bool isActive
            {
                get
                {
                    return _isActive;
                }
            }
            
            public EditableListBox listBox;
			protected int _index = -1;
            public int index
            {
                get
                {
                    return _index;
                }
            }

			bool brementer = false;

			public NTextBox(EditableListBox parent)
			{
                BackColor = SystemColors.ActiveCaption;
                ForeColor = SystemColors.ActiveCaptionText;
                listBox=parent;
                listBox.Scroll += new EventHandler(listBox_Scroll);
            }

            void listBox_Scroll(object sender, EventArgs e)
            {
                redraw(Text);
                listBox.Invalidate();
                this.Invalidate();
            }

			protected override void OnKeyUp(KeyEventArgs e)
			{
				if(brementer)
				{
					Text = "";
					brementer = false;
				}
				base.OnKeyUp(e);
			}

			protected override void OnKeyPress(KeyPressEventArgs e)
			{
				base.OnKeyPress(e);
				
				if(e.KeyChar == 13 || e.KeyChar == 27)
				{
                    endEdit();
                    if (index + 1 < listBox.Items.Count) beginEdit(index + 1);
				}
			}

            public void endEdit()
            {
                if (isActive && index >= 0 && index < listBox.Items.Count)
                {
                    _isActive = false;

                    if ((string)listBox.Items[index] != Text)
                    {
                        listBox.Items[index] = Text;
                        if (ItemChanged != null) ItemChanged(this, new ItemsChangedEventArgs(listBox.Items[index], index));
                    }
                    Hide();
                    EditEnded(this, new EventArgs());
                }
            }
            public void beginEdit(int index)
            {
                if (isActive)
                {
                    endEdit();
                }
                _isActive = true;
                _index = index;
                string s = listBox.Items[index].ToString();
                redraw(s);
               
                listBox.makeIndexVisible(index);
            }
            protected void redraw(string text)
            {
                if (index >= 0)
                {
                    Rectangle rect = listBox.GetItemRectangle(index);

                    Location = new Point(rect.X, rect.Y);
                    Size = new Size(rect.Width, rect.Height);
                    Text = text;
                    SelectAll();
                    Show();
                    Focus();
                }
            }
        }

		private void InitializeComponent()
        {
            this.SuspendLayout();
            this.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.Size = new System.Drawing.Size(120, 95);
            this.ResumeLayout(false);
        }

        public void AddItem(object item, bool isComputerGenerated = false)
        {
            enableWndProcItemsChangedEvent = false;

            Items.Add(item);
            int index = Items.IndexOf(item);
            SelectedIndex = index;

            if (ItemsAdded != null) ItemsAdded(this, new ItemsChangedEventArgs(item, index, isComputerGenerated));
            if (UserChangedCount != null) UserChangedCount(this, new EventArgs());

            enableWndProcItemsChangedEvent = true;
        }

        public void InsertItem(int index, object item, bool isComputerGenerated = false)
        {
            enableWndProcItemsChangedEvent = false;

            Items.Insert(index, item);
            SelectedIndex = index;
            if (ItemsAdded != null) ItemsAdded(this, new ItemsChangedEventArgs(item, index, isComputerGenerated));
            if (UserChangedCount != null) UserChangedCount(this, new EventArgs());

            enableWndProcItemsChangedEvent = true;
        }

        public void AddItems(System.Collections.Generic.List<object> items, bool isComputerGenerated = false)
        {
            enableWndProcItemsChangedEvent = false;

            List<int> indices = new List<int>();
            foreach (object item in items)
            {
                Items.Add(item);
                int index = Items.IndexOf(item);
                indices.Add(index);
                SelectedIndex = index;
            }
            if (ItemsAdded != null) ItemsAdded(this, new ItemsChangedEventArgs(items, indices, isComputerGenerated));
            if (UserChangedCount != null) UserChangedCount(this, new EventArgs());

            enableWndProcItemsChangedEvent = true;
        }

        public void RemoveItem(object item, bool isComputerGenerated = false)
        {
            enableWndProcItemsChangedEvent = false;

            if (this.SelectedItem == item && tbox.isActive)
            {
                tbox.endEdit();
            }
            int index = Items.IndexOf(item);
            Items.Remove(item);
            SelectedIndex = index < Items.Count ? index : index - 1;
            if (ItemsRemoved != null) ItemsRemoved(this, new ItemsChangedEventArgs(item, index, isComputerGenerated));
            if (UserChangedCount != null) UserChangedCount(this, new EventArgs());

            enableWndProcItemsChangedEvent = true;
        }

        public void RemoveItemAt(int index, bool isComputerGenerated = false)
        {
            enableWndProcItemsChangedEvent = false;

            if (this.SelectedIndex == index && tbox.isActive)
            {
                tbox.endEdit();
            }
            object item = Items[index];
            Items.RemoveAt(index);
            SelectedIndex = index < Items.Count ? index : index - 1;
            if (ItemsRemoved != null) ItemsRemoved(this, new ItemsChangedEventArgs(item, index, isComputerGenerated));
            if (UserChangedCount != null) UserChangedCount(this, new EventArgs());

            enableWndProcItemsChangedEvent = true;
        }

        public void RemoveItems(System.Collections.Generic.List<object> items, bool isComputerGenerated = false)
        {
            enableWndProcItemsChangedEvent = false;

            List<int> indices = new List<int>();
            foreach (object item in items)
            {
                if (this.SelectedItem == item && tbox.isActive)
                {
                    tbox.endEdit();
                }
                int index = Items.IndexOf(item);
                indices.Add(index);
                Items.Remove(item);
                SelectedIndex = index < Items.Count ? index : index - 1;
                
            }
            if (ItemsRemoved != null) ItemsRemoved(this, new ItemsChangedEventArgs(items, indices, isComputerGenerated));
            if (UserChangedCount != null) UserChangedCount(this, new EventArgs());

            enableWndProcItemsChangedEvent = true;
        }

        public void makeIndexVisible(int index)
        {
            //The max number of items that the listbox can display at a time
            int NumberOfItems = ClientSize.Height / ItemHeight;

            if (TopIndex > index || TopIndex + NumberOfItems <= index)
            {
                //The item at the top when you can just see the bottom item
                TopIndex = Items.Count - NumberOfItems + 1;
            }
        }

        public void endEdit()
        {
            tbox.endEdit();
        }

        public void beginEdit(int index)
        {
            tbox.beginEdit(index);
        }
	}

    public delegate void ItemsChangedEventHandler(object sender, ItemsChangedEventArgs e);
    public class ItemsChangedEventArgs : EventArgs
    {
        protected List<object> _items;
        public List<object> Items
        {
            get
            {
                return _items;
            }
        }
        protected List<int> _indices;
        public List<int> Indices
        {
            get
            {
                return _indices;
            }
        }
        protected bool _isComputerGenerated;
        public bool IsComputerGenerated
        {
            get
            {
                return _isComputerGenerated;
            }
        }

        public ItemsChangedEventArgs(object item, int index, bool isComputerGenerated = false)
            : this(new List<object>(new object[] { item }), new List<int>(new int[] { index }), isComputerGenerated)
        {
        }

        public ItemsChangedEventArgs(List<object> items, List<int> indices, bool isComputerGenerated = false)
        {
            _items = items;
            _indices = indices;
            _isComputerGenerated = isComputerGenerated;
        }
    }
}