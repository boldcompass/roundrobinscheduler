using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SomeTechie.RoundRobinScheduler
{
    public partial class NumericTextBox : TextBox
    {
        bool _allowSpace = false;
        public bool AllowSpace
        {
            set
            {
                this._allowSpace = value;
            }

            get
            {
                return this._allowSpace;
            }
        }

        bool _allowDecimal = true;
        public bool AllowDecimal
        {
            set
            {
                this._allowDecimal = value;
            }

            get
            {
                return this._allowDecimal;
            }
        }

        bool _allowNegative = false;
        public bool AllowNegative
        {
            set
            {
                this._allowNegative = value;
            }

            get
            {
                return this._allowNegative;
            }
        }

        public NumericTextBox()
            : base()
        {
            Text = "0";
        }

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            System.Globalization.NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if ((AllowDecimal && keyInput.Equals(decimalSeparator)) || keyInput.Equals(groupSeparator) ||
             (AllowNegative && keyInput.Equals(negativeSign)))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
                if (Text.Length <= 1)
                {
                    Text = "0";
                    e.Handled = true;
                }
                
            }
            else if (this._allowSpace && e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        public int IntValue
        {
            get
            {
                int value;
                bool succeeded = int.TryParse(this.Text, out value);
                if (succeeded) return value;
                else return 0;
            }
            set
            {
                this.Text = value.ToString();
            }
        }

        public decimal DecimalValue
        {
            get
            {
                decimal value;
                bool succeeded = decimal.TryParse(this.Text, out value);
                if (succeeded) return value;
                else return 0;
            }
            set
            {
                this.Text = value.ToString();
            }
        }
    }
}
