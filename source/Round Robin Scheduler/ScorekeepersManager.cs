using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler
{
    public partial class ScorekeepersManager : UserControl
    {
        protected List<List<Control>> scoreKeepersControls = new List<List<Control>>();
        protected List<ComboBox> scoreKeepersAccessCodeControls = new List<ComboBox>();
        protected List<CheckBox> scoreKeepersCheckBoxControls = new List<CheckBox>();
        protected List<TextBox> scoreKeepersNameControls = new List<TextBox>();
        protected List<int> selectedIndices = new List<int>();
        protected Controller Controller = Controller.GetController();
        protected object emptyAccessCodeItem = "";
        protected List<ScoreKeeper> oldScoreKeepers = new List<ScoreKeeper>();

        protected List<ScoreKeeper> ScoreKeepers
        {
            get
            {
                return Controller.ScoreKeepers;
            }
        }

        public ScorekeepersManager()
        {
            InitializeComponent();
            refreshAccessCodeList();
            Controller.ScoreKeeperAccessCodesChanged += new EventHandler(Controller_ScoreKeeperAccessCodesChanged);
            Controller.ScoreKeepersChanged += new EventHandler(Controller_ScoreKeepersChanged);
        }

        void Controller_ScoreKeepersChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ScoreKeepers.Count; i++)
            {
                if (oldScoreKeepers.Count > i)
                {
                    if (oldScoreKeepers[i] != ScoreKeepers[i])
                    {
                        //Update the existing entry
                        refreshRows(i);
                    }
                }
                else
                {
                    //Create a new entry
                    addScoreKeepers(ScoreKeepers[i]);
                    checkBoxHeader.Checked = false;
                }
            }
            
            List<int> toRemove = new List<int>();
            for (int i = ScoreKeepers.Count; i < oldScoreKeepers.Count; i++)
            {
                toRemove.Add(i);
            }
            if (toRemove.Count > 0) removeScoreKeepers(toRemove.ToArray());

            refreshButtonEnablement();
        }

        void Controller_ScoreKeeperAccessCodesChanged(object sender, EventArgs e)
        {
            refreshAccessCodeList();
        }

        protected void refreshAccessCodeList(int scoreKeeperIndex = -1)
        {
            List<object> accessCodeList = new List<object>();
            accessCodeList.Add(emptyAccessCodeItem);
            accessCodeList.AddRange(Controller.ScoreKeeperAccessCodes.ToArray());
            accessCodeList.Sort();
            int startIndex = 0;
            int endIndex = ScoreKeepers.Count - 1;
            if (scoreKeeperIndex >= 0)
            {
                startIndex = Math.Max(startIndex,scoreKeeperIndex);
                endIndex = Math.Min(endIndex, scoreKeeperIndex);
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                ComboBox AccessCodeSelector = scoreKeepersAccessCodeControls[i];
                object currentAccessCode = Controller.ScoreKeepers[i].AssociatedAccessCode;
                AccessCodeSelector.Items.Clear();
                AccessCodeSelector.Items.AddRange(accessCodeList.ToArray());
                if (currentAccessCode != null && AccessCodeSelector.Items.Contains(currentAccessCode)) AccessCodeSelector.SelectedItem = currentAccessCode;
                else AccessCodeSelector.SelectedItem = emptyAccessCodeItem;
            }
        }

        protected void refreshSizing()
        {
            layoutPanel.Width = Width;
            layoutPanel.Height = Height - btnAddScoreKeeper.Height;
        }

        private void btnAddScoreKeeper_Click(object sender, EventArgs e)
        {
            ScoreKeeper scoreKeeper = new ScoreKeeper(String.Format("Score Keeper {0}", ScoreKeepers.Count + 1));

            ScoreKeepers.Add(scoreKeeper);
            Controller.TriggerScoreKeepersChanged();
        }

        protected void refreshRows(params int[] indices)
        {
            foreach (int index in indices)
            {
                ScoreKeeper scoreKeeper = ScoreKeepers[index];

                scoreKeepersNameControls[index].Text = scoreKeeper.Name;
                scoreKeepersCheckBoxControls[index].Checked = false;
                ComboBox scoreKeeperAccessCodeControl = scoreKeepersAccessCodeControls[index];
                if (scoreKeeper.AssociatedAccessCode != null && scoreKeeperAccessCodeControl.Items.Contains(scoreKeeper.AssociatedAccessCode))
                {
                    scoreKeeperAccessCodeControl.SelectedItem = scoreKeeper.AssociatedAccessCode;
                }
                else
                {
                    scoreKeeperAccessCodeControl.SelectedItem = emptyAccessCodeItem;
                }
            }
            checkBoxHeader.Checked = false;
        }

        protected void addScoreKeepers(params ScoreKeeper[] scoreKeepers)
        {
            foreach (ScoreKeeper scoreKeeper in scoreKeepers)
            {
                List<Control> scoreKeeperControls = new List<Control>();
                int scoreKeeperIndex = ScoreKeepers.IndexOf(scoreKeeper);
                int rowNumber = scoreKeeperIndex + 1;
                layoutPanel.RowCount = rowNumber + 2;
                scoreKeepersControls.Add(scoreKeeperControls);


                //Add check box
                CheckBox checkBox = new CheckBox();
                checkBox.Text = "";
                checkBox.Dock = DockStyle.Fill;
                checkBox.CheckAlign = ContentAlignment.MiddleCenter;

                checkBox.CheckedChanged += delegate(object esender, EventArgs ee)
                {
                    if (checkBox.Checked)
                    {
                        selectedIndices.Add(scoreKeeperIndex);
                    }
                    else
                    {
                        selectedIndices.Remove(scoreKeeperIndex);
                        checkBoxHeader.Checked = false;
                    }
                    refreshButtonEnablement();
                };

                layoutPanel.Controls.Add(checkBox);
                layoutPanel.SetColumn(checkBox, 0);
                scoreKeeperControls.Add(checkBox);
                scoreKeepersCheckBoxControls.Add(checkBox);

                //Add name textbox
                TextBox scoreKeeperName = new TextBox();
                scoreKeeperName.Text = scoreKeeper.Name;
                scoreKeeperName.Dock = DockStyle.Top;

                scoreKeeperName.TextChanged += delegate(object esender, EventArgs ee)
                {
                    ScoreKeeper sk = ScoreKeepers[scoreKeeperIndex];
                    sk.Name = scoreKeeperName.Text;
                    Controller.TriggerScoreKeeperNameChanged(sk);
                };

                //Select all on focus
                bool scoreKeeperNameAlreadyFocused = false;
                scoreKeeperName.Leave += delegate(object esender, EventArgs ee)
                {
                    scoreKeeperNameAlreadyFocused = false;
                };

                scoreKeeperName.Enter += delegate(object esender, EventArgs ee)
                {
                    // Select all text only if the mouse isn't down.
                    // This makes tabbing to the textbox give focus.
                    if (MouseButtons == MouseButtons.None)
                    {
                        scoreKeeperName.SelectAll();
                        scoreKeeperNameAlreadyFocused = true;
                    }
                };

                scoreKeeperName.MouseUp += delegate(object esender, MouseEventArgs ee)
                {
                    // .net selects the text on mouse up.
                    // Select all text only do it if the textbox isn't already focused,
                    // and if the user hasn't selected all text.
                    if (!scoreKeeperNameAlreadyFocused && scoreKeeperName.SelectionLength == 0)
                    {
                        scoreKeeperNameAlreadyFocused = true;
                        scoreKeeperName.SelectAll();
                    }
                };

                layoutPanel.Controls.Add(scoreKeeperName);
                layoutPanel.SetColumn(scoreKeeperName, 1);
                scoreKeeperControls.Add(scoreKeeperName);
                scoreKeepersNameControls.Add(scoreKeeperName);

                //Add acces code drop down
                ComboBox scoreKeeperAccessCode = new ComboBox();
                scoreKeeperAccessCode.DropDownStyle = ComboBoxStyle.DropDownList;

                scoreKeeperAccessCode.SelectedIndexChanged += delegate(object esender, EventArgs ee)
                {
                    object item = scoreKeeperAccessCode.SelectedItem;
                    ScoreKeeper sk = ScoreKeepers[scoreKeeperIndex];
                    if (item != emptyAccessCodeItem)
                    {
                        sk.AssociatedAccessCode = item.ToString();
                        foreach (ComboBox scoreKeeperAccessCodeControl in scoreKeepersAccessCodeControls)
                        {
                            //Prevent multiple score keepers from having the same access code
                            if (scoreKeeperAccessCodeControl == scoreKeeperAccessCode) continue;
                            if (scoreKeeperAccessCodeControl.SelectedItem == item) scoreKeeperAccessCodeControl.SelectedItem = emptyAccessCodeItem;
                        }
                    }
                    else sk.AssociatedAccessCode = null;
                };

                layoutPanel.Controls.Add(scoreKeeperAccessCode);
                layoutPanel.SetColumn(scoreKeeperAccessCode, 2);
                scoreKeeperControls.Add(scoreKeeperAccessCode);
                scoreKeepersAccessCodeControls.Add(scoreKeeperAccessCode);
                oldScoreKeepers.Add(scoreKeeper);
                refreshAccessCodeList(scoreKeeperIndex);

                assignScoreKeepersControlsRows(scoreKeeperIndex);
            }
            checkBoxHeader.Checked = false;
        }

        protected void refreshButtonEnablement()
        {
            btnRemoveScoreKeeper.Enabled = selectedIndices.Count > 0;
        }

        protected void assignScoreKeepersControlsRows(int scoreKeeperIndex = -1)
        {
            int startIndex = 0;
            int endIndex = scoreKeepersControls.Count - 1;
            if (scoreKeeperIndex >= 0)
            {
                startIndex = Math.Max(startIndex, scoreKeeperIndex);
                endIndex = Math.Min(endIndex, scoreKeeperIndex);
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                foreach (Control control in scoreKeepersControls[i])
                {
                    layoutPanel.SetRow(control, i + 1);
                }
            }
        }

        private void ScorekeepersManager_Load(object sender, EventArgs e)
        {
            refreshSizing();
            refreshButtonEnablement();
        }

        private void ScorekeepersManager_Resize(object sender, EventArgs e)
        {
            refreshSizing();
        }

        private void btnRemoveScoreKeeper_Click(object sender, EventArgs e)
        {
            if (selectedIndices.Count > 0)
            {
                selectedIndices.Sort();
                selectedIndices.Reverse();

                foreach (int index in selectedIndices)
                {
                    ScoreKeepers.RemoveAt(index);
                }
                Controller.TriggerScoreKeepersChanged();
                selectedIndices.Clear();
            }
        }

        private void removeScoreKeepers(params int[] indices)
        {
            List<int> indexList = new List<int>(indices);
            indexList.Sort();
            indexList.Reverse();
            foreach (int index in indexList)
            {
                if(index<0||index>=scoreKeepersControls.Count)continue;
                foreach (Control control in scoreKeepersControls[index])
                {
                    layoutPanel.Controls.Remove(control);
                    control.Dispose();
                }
                scoreKeepersControls.RemoveAt(index);
                scoreKeepersCheckBoxControls.RemoveAt(index);
                scoreKeepersAccessCodeControls.RemoveAt(index);
                scoreKeepersNameControls.RemoveAt(index);
                oldScoreKeepers.RemoveAt(index);
            }
            assignScoreKeepersControlsRows();
            layoutPanel.RowCount = ScoreKeepers.Count + 2;
            checkBoxHeader.Checked = false;
        }

        private void removeScoreKeepers(ScoreKeeper[] scoreKeepers)
        {
            List<int> indices = new List<int>();
            foreach (ScoreKeeper scoreKeeper in scoreKeepers)
            {
                indices.Add(ScoreKeepers.IndexOf(scoreKeeper));
            }
            removeScoreKeepers(indices.ToArray());
        }

        private void checkBoxHeader_CheckedChanged(object sender, EventArgs e)
        {
            selectedIndices.Clear();
            if (checkBoxHeader.Checked)
            {
                for (int i = 0; i < ScoreKeepers.Count; i++)
                {
                    selectedIndices.Add(i);
                }
            }
            foreach (CheckBox chk in scoreKeepersCheckBoxControls)
            {
                chk.Checked = true;
            }
            refreshButtonEnablement();
        }
    }
}
