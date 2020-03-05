using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        public static string var;
        public static bool flag = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (flag)
                textBox1.Text = ""+textBox1.Text[textBox1.Text.Length-1];
             flag = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            textBox1.Text += button.Text;
        }

        private int Operator(string logic, int x, int y)
        {
            if (Equals(logic, "+"))
                return x + y;
            else if (Equals(logic, "-"))
                return y - x;
            else if (Equals(logic, "*"))
                return x * y;
            else if (Equals(logic, "/"))
                return y / x;
            return 0;

        }
        private void Rezultat(Stack<int> listnum, Stack<string> listop)
        {

            int op1, op2;
            if (listop.Peek() == ")")
            {
                listop.Pop();

                while (listop.Peek() != "(")
                {
                    Console.WriteLine(listop.Peek());
                    op1 = listnum.Peek();
                    listnum.Pop();
                    op2 = listnum.Peek();
                    listnum.Pop();
                    listnum.Push(Operator(listop.Peek(), op1, op2));
                    listop.Pop();

                }


                listop.Pop();
            }
            else
            {
                //Console.WriteLine(listop.Peek());
                op1 = listnum.Peek();
                listnum.Pop();
                op2 = listnum.Peek();
                listnum.Pop();
                listnum.Push(Operator(listop.Peek(), op1, op2));
                listop.Pop();
            }
        }
       
            private void button18_Click(object sender, EventArgs e)
        {

            string textbox = textBox1.Text;
            IDictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("+", 1);
            dict.Add("-", 1);
            dict.Add("*", 2);
            dict.Add("/", 2);
            dict.Add(")", 0);
            dict.Add("(", 3);
            var listnum = new Stack<int>();
            var listop = new Stack<string>();
            int i, po = 0;

            if (textbox.Length == 0)
                return;

            if (dict.ContainsKey(textbox[0].ToString()) && textbox[0] != '(')
                MessageBox.Show("ERROR", "izraz ne smije pocinjat s operatorom", MessageBoxButtons.OK);
            else if (dict.ContainsKey(textbox[textbox.Length - 1].ToString()) && textbox[textbox.Length - 1] != ')')
                MessageBox.Show("ERROR", "izraz ne smije zavrsavati s operatorom", MessageBoxButtons.OK);
            if (textbox.Count(p => p == '(') % 2 != textbox.Count(p => p == ')') % 2)

                MessageBox.Show("ERROR", "zagrade nisu dobre", MessageBoxButtons.OK);


            else
            {

                for (i = 0; i < textbox.Length; i++)
                    if (dict.ContainsKey(textbox[i].ToString()))
                    {

                        if (textbox[i] != '(' && (listop.Count == 0 || listop.Peek() != ")"))
                        {
                            try
                            {
                                listnum.Push(int.Parse(textbox.Substring(po, i - po)));
                            }
                            catch (Exception b)
                            {
                                MessageBox.Show("ERROR", "dva operatora ne smiju bit jedan iza drugoga", MessageBoxButtons.OK);
                                return;
                            }
                        }



                        if (listop.Count != 0 && dict[listop.Peek()] > dict[textbox[i].ToString()] && listop.Peek() != "(")
                            Rezultat(listnum, listop);

                        listop.Push(textbox[i].ToString());

                        po = i + 1;


                    }

                if (po != textbox.Length)
                    listnum.Push(int.Parse(textbox.Substring(po, i - po)));

                while (listop.Count != 0)
                    Rezultat(listnum, listop);

                textBox1.Text = "" + listnum.Peek();
                var = listnum.Peek().ToString();
                flag = true;
                

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += var;
        }

        
    }
}

      

       
    

