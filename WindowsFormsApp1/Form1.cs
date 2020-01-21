using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Label> lstFinal = new List<Label>();
        private Random random = new Random();
        bool clicked = false;
        public Form1()
        {
            InitializeComponent();
            
        }



        private void buttonStart_Click(object sender, EventArgs e)
        {
            List<String> lstNames = new List<String>()
            {
                "First Name","Last Name","Email Address","Phone Number"
            };
            Dictionary<Label, TextBox> dictAssets = new Dictionary<Label, TextBox>();


            Controls.Remove(sender as Button);
            this.SuspendLayout();
            dictAssets = beginGame(lstNames);

            Label instr = new Label();
            instr.Text = "Press Next when you have typed the result.";
            instr.Location = new Point(325, 10);
            instr.AutoSize = true;
            Controls.Add(instr);
            Button next = new Button();
            Button fin = new Button();
            next.Text = "Next?";
            fin.Text = "Finished?";
            fin.Location = new Point(400, 350);
            next.Click += new System.EventHandler(click_btnNext);
            next.Location = new Point(300, 350);
            Controls.Add(next);
            Controls.Add(fin);


            foreach (Label lbl in dictAssets.Keys)
            {
                Controls.Add(dictAssets[lbl]);
                Controls.Add(lbl);
                lbl.Invalidate();
                lbl.Update();            
                next.Invalidate();
                next.Update();

                this.ResumeLayout(false);
            }
            
        }

        private Dictionary<Label, TextBox> beginGame(List<String> lstItems)
        {
            List<String> lstFinal = new List<String>();
            Dictionary<Label, TextBox> dictAssets = new Dictionary<Label, TextBox>();

               int x = 0;
            int size = lstItems.Count;
            while (x < size)
            {
                int rndNum = random.Next(0, lstItems.Count - 1);
                lstFinal.Add(lstItems[rndNum]);
                lstItems.RemoveAt(rndNum);
                x = x + 1;
            }

            foreach(String text in lstFinal)
            {
                TextBox txt1 = new TextBox();
                Label info = new Label();
                int posX = random.Next(40, 700);
                int posY = random.Next(50, 400);

                txt1.Name = text;
                txt1.Location = new Point(posX, posY);
                info.Text = text;
                info.Location = new Point(posX, posY - 20);
                dictAssets.Add(info, txt1);
            }
            
        return dictAssets;
        }
        private void click_btnNext(object sender, EventArgs e)
        {
            clicked = true;
            List<string> lstResponse = new List<string>();
            var lstBox = GetAllControls(Form.ActiveForm).OfType<TextBox>();
            var lstLbl = GetAllControls(Form.ActiveForm).OfType<Label>();
            foreach(TextBox line in lstBox)
            {
                lstResponse.Add(String.Format("{0}|{1}", line.Text, line.Name));
                Controls.Remove(line);
            }
            foreach(Label label in lstLbl)
            {
                Controls.Remove(label);
            }

            foreach(string answer in lstResponse)
            {
                string boxName = answer.Remove(0, answer.IndexOf('|') + 1 );
                

            }
        }

        public static IEnumerable<Control> GetAllControls(Control root)
        {
            var stack = new Stack<Control>();
            stack.Push(root);

            while (stack.Any())
            {
                var next = stack.Pop();
                foreach (Control child in next.Controls)
                    stack.Push(child);

                yield return next;
            }
        }

    }
}
