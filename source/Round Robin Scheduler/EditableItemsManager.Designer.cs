namespace SomeTechie.RoundRobinScheduler
{
    partial class EditableItemsManager
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lblNumItems = new System.Windows.Forms.Label();
            this.updwnItemsCount = new System.Windows.Forms.NumericUpDown();
            this.lblITems = new System.Windows.Forms.Label();
            this.btnMoveItemUp = new System.Windows.Forms.Button();
            this.btnMoveItemDown = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lstItems = new SomeTechie.RoundRobinScheduler.EditableListBox();
            ((System.ComponentModel.ISupportInitialize)(this.updwnItemsCount)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddItem
            // 
            this.btnAddItem.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btnAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddItem.Image = global::SomeTechie.RoundRobinScheduler.Properties.Resources.add;
            this.btnAddItem.Location = new System.Drawing.Point(0, 296);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(40, 36);
            this.btnAddItem.TabIndex = 2;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lblNumItems
            // 
            this.lblNumItems.AutoSize = true;
            this.lblNumItems.Location = new System.Drawing.Point(2, 2);
            this.lblNumItems.Margin = new System.Windows.Forms.Padding(2);
            this.lblNumItems.Name = "lblNumItems";
            this.lblNumItems.Size = new System.Drawing.Size(133, 20);
            this.lblNumItems.TabIndex = 10;
            this.lblNumItems.Text = "Number of items";
            // 
            // updwnItemsCount
            // 
            this.updwnItemsCount.Location = new System.Drawing.Point(139, 0);
            this.updwnItemsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.updwnItemsCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.updwnItemsCount.Name = "updwnItemsCount";
            this.updwnItemsCount.Size = new System.Drawing.Size(68, 26);
            this.updwnItemsCount.TabIndex = 0;
            this.updwnItemsCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.updwnItemsCount.ValueChanged += new System.EventHandler(this.updwnItemCount_ValueChanged);
            // 
            // lblITems
            // 
            this.lblITems.AutoSize = true;
            this.lblITems.Location = new System.Drawing.Point(2, 31);
            this.lblITems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblITems.Name = "lblITems";
            this.lblITems.Size = new System.Drawing.Size(55, 20);
            this.lblITems.TabIndex = 12;
            this.lblITems.Text = "Items:";
            // 
            // btnMoveItemUp
            // 
            this.btnMoveItemUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMoveItemUp.Image = global::SomeTechie.RoundRobinScheduler.Properties.Resources.arrowup;
            this.btnMoveItemUp.Location = new System.Drawing.Point(321, 132);
            this.btnMoveItemUp.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveItemUp.Name = "btnMoveItemUp";
            this.btnMoveItemUp.Size = new System.Drawing.Size(40, 36);
            this.btnMoveItemUp.TabIndex = 4;
            this.btnMoveItemUp.UseVisualStyleBackColor = true;
            this.btnMoveItemUp.Click += new System.EventHandler(this.btnMoveItemUp_Click);
            // 
            // btnMoveItemDown
            // 
            this.btnMoveItemDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMoveItemDown.Image = global::SomeTechie.RoundRobinScheduler.Properties.Resources.arrowdown;
            this.btnMoveItemDown.Location = new System.Drawing.Point(321, 176);
            this.btnMoveItemDown.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveItemDown.Name = "btnMoveItemDown";
            this.btnMoveItemDown.Size = new System.Drawing.Size(40, 36);
            this.btnMoveItemDown.TabIndex = 5;
            this.btnMoveItemDown.UseVisualStyleBackColor = true;
            this.btnMoveItemDown.Click += new System.EventHandler(this.btnMoveItemDown_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveItem.Image = global::SomeTechie.RoundRobinScheduler.Properties.Resources.remove;
            this.btnRemoveItem.Location = new System.Drawing.Point(48, 296);
            this.btnRemoveItem.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(40, 36);
            this.btnRemoveItem.TabIndex = 3;
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblNumItems);
            this.flowLayoutPanel1.Controls.Add(this.updwnItemsCount);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(361, 28);
            this.flowLayoutPanel1.TabIndex = 13;
            // 
            // lstItems
            // 
            this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lstItems.FormattingEnabled = true;
            this.lstItems.ItemHeight = 20;
            this.lstItems.Location = new System.Drawing.Point(0, 55);
            this.lstItems.Margin = new System.Windows.Forms.Padding(4);
            this.lstItems.Name = "lstItems";
            this.lstItems.ScrollAlwaysVisible = true;
            this.lstItems.Size = new System.Drawing.Size(312, 233);
            this.lstItems.TabIndex = 1;
            this.lstItems.UserChangedCount += new System.EventHandler(this.lstItems_UserChangedCount);
            this.lstItems.ItemsRemoved += new SomeTechie.RoundRobinScheduler.ItemsChangedEventHandler(this.lstItems_ItemRemoved);
            this.lstItems.ItemsAdded += new SomeTechie.RoundRobinScheduler.ItemsChangedEventHandler(this.lstItems_ItemAdded);
            this.lstItems.ItemsEdited += new SomeTechie.RoundRobinScheduler.ItemsChangedEventHandler(this.lstItems_ItemChanged);
            this.lstItems.ItemsCollectionChanged += new System.EventHandler(this.lstItems_ItemsChanged);
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
            // 
            // EditableItemsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.btnMoveItemDown);
            this.Controls.Add(this.btnMoveItemUp);
            this.Controls.Add(this.lblITems);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.lstItems);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditableItemsManager";
            this.Size = new System.Drawing.Size(361, 333);
            this.Load += new System.EventHandler(this.EditableItemsManager_Load);
            this.Enter += new System.EventHandler(this.EditableItemsManager_Enter);
            this.Leave += new System.EventHandler(this.EditableItemsManager_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.updwnItemsCount)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Label lblNumItems;
        private EditableListBox lstItems;
        private System.Windows.Forms.NumericUpDown updwnItemsCount;
        private System.Windows.Forms.Label lblITems;
        private System.Windows.Forms.Button btnMoveItemUp;
        private System.Windows.Forms.Button btnMoveItemDown;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

    }
}
