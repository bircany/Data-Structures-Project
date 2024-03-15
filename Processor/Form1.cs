using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace KuyrukProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void StartSystem(object sender, EventArgs e)
        {
            EnableProcesses(true);
        }
        private void StopSystem(object sender, EventArgs e)
        {
            EnableProcesses(false);
        }
        private void EnableProcesses(bool enable)
        {
            Process1.Enabled = enable;
            Process2.Enabled = enable;
            Process3.Enabled = enable;
            Processor.Enabled = enable;
        }
        private void ProcessorSpeed(object sender, EventArgs e)
        {
            Processor.Interval = (trackBar1.Maximum + 1) - trackBar1.Value;
        }
        private void Process1Speed(object sender, EventArgs e)
        {
            Process1.Interval = (trackBar2.Maximum + 1) - trackBar2.Value;
        }
        private void Process2Speed(object sender, EventArgs e)
        {
            Process2.Interval = (trackBar3.Maximum + 1) - trackBar3.Value;
        }
        private void Process3Speed(object sender, EventArgs e)
        {
            Process3.Interval = (trackBar4.Maximum + 1) - trackBar4.Value;
        }
        class Queue1
        {
            public int no;
            public Queue1 next;
        }
        class Queue2
        {
            public int no;
            public Queue2 next;
        }
        class Queue3
        {
            public int no;
            public Queue3 next;
        }
        class Queue4
        {
            public int x1;
            public int x2;
            public int x3;
            public Queue4 next;
        }
        class Stack1
        {
            public int y;
            public Stack1 next;
        }
        class Stack2
        {
            public int y;
            public Stack2 next;
        }
        class Stack3
        {
            public int y;
            public Stack3 next;
        }
        Queue1 process1Head, process1Last = null;
        Queue2 process2Head, process2Last = null;
        Queue3 process3Head, process3Last = null;
        Queue4 process4Head, process4Last = null;
        Stack1 Stack1Head = null;
        Stack2 Stack2Head = null;
        Stack3 Stack3Head = null;
        Random rnd = new Random();
        int count = 0;
        private void Process1_Tick(object sender, EventArgs e)
        {
            Queue1 proses1 = new Queue1();
            //Enqueue
            proses1.no = rnd.Next(6);

            if (process1Head == null)
                process1Head = process1Last = proses1;
            else
            {
                process1Last.next = proses1;
                process1Last = proses1;
            }
           
            Queue1 temp = new Queue1();   //Kuyruk Üstünde Yazdırma
            temp = process1Head;
            textBox2.Clear();
            while (temp != null)    
            {
                textBox2.Text += "P1-" + temp.no + Environment.NewLine;
                temp = temp.next;
            }
        }
        private void Process2_Tick(object sender, EventArgs e)
        {
            Queue2 proses2 = new Queue2();
            proses2.no = rnd.Next(6);

            if (process2Head == null)  //Enqueue
                process2Head = process2Last = proses2;
            else
            {
                process2Last.next = proses2;
                process2Last = proses2;
            }
            
            Queue2 temp = new Queue2();  //Kuyruk Üstünde Yazdırma
            temp = process2Head;
            textBox3.Clear();
            while (temp != null)
            {
                textBox3.Text += "P2-" + temp.no + Environment.NewLine;
                temp = temp.next;
            }
        }
        private void Process3_Tick(object sender, EventArgs e)
        {
            Queue3 proses3 = new Queue3();
            proses3.no = rnd.Next(6);
            
            if (process3Head == null)  //Enqueue
                process3Head = process3Last = proses3;

            else
            {
                process3Last.next = proses3;
                process3Last = proses3;
            }
            
            Queue3 temp = new Queue3();  //Kuyruk Üstünde Yazdırma
            temp = process3Head;
            textBox4.Clear();
            while (temp != null)
            {
                textBox4.Text += "P3-" + temp.no + Environment.NewLine;
                temp = temp.next;
            }
        }
        private void Processor_Tick(object sender, EventArgs e)
        {
            
            if (process1Head != null && process2Head != null && process3Head != null)  //Eğer kuyruklardan gelen değer varsa içine gir.
            {
                textBox1.BackColor = Color.White;
                Queue4 process4 = new Queue4();
                EnqueueProcesses(process4);  //enqueue + kuyruk yazdırma
                importStack(); //Kuyruktan --> Yığına Aktar
                DequeueProcesses(); //dequeue
                count++;
                label9.Text = (3 * count).ToString(); //Yapılan İşlem Sayısı
            }
            else
            {
                textBox1.Text = "Lütfen işlemci hızını kısınız!";
                textBox1.BackColor = Color.Red;
            }
        }
        private Queue4 EnqueueProcesses(Queue4 process)
        {
            Queue4 process4 = new Queue4()
            {
                x1 = process1Head.no,
                x2 = process2Head.no,
                x3 = process3Head.no
            };

            
            if (process4Head == null)
                process4Head = process4Last = process4;
            else
            {
                process4Last.next = process4;
                process4Last = process4;
            }

            textBox1.Clear();
            DisplayBigQueue(); //kuyruk yazdırma

            return process4;
        }

        private void DequeueProcesses()
        {
            process1Head = process1Head.next;
            process2Head = process2Head.next;
            process3Head = process3Head.next;
            process4Head = process4Head.next;
        }
        private void importStack()
        {
            Stack1 Stack1 = new Stack1();
            Stack2 Stack2 = new Stack2();
            Stack3 Stack3 = new Stack3();

            Stack1.y = process4Head.x1;
            Stack2.y = process4Head.x2;
            Stack3.y = process4Head.x3;

            if (Stack1Head == null)
                Stack1Head  = Stack1;
            else
            {
                Stack1.next = Stack1Head;
                Stack1Head = Stack1;
            }

            if (Stack2Head == null)
                Stack2Head = Stack2;
            else
            {
                Stack2.next = Stack2Head;
                Stack2Head = Stack2;
            }

            if (Stack3Head == null)
                Stack3Head  = Stack3;
            else
            {
                Stack3.next = Stack3Head;
                Stack3Head = Stack3;
            }
        }
        private void DisplayBigQueue()
        {
            textBox1.Clear();
            Queue4 temp = process4Head;

            while (temp != null)
            {
                textBox1.Text += GetFormattedText(temp);
                temp = temp.next;
            }
        }

        private string GetFormattedText(Queue4 temp)
        {
            int[] values = { temp.x1, temp.x2, temp.x3 };
            Array.Sort(values);
            string s = $" P3-{values[0]} -> P2-{values[1]} -> P1-{values[2]} ";
            return s;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Stack1 stack1 = Stack1Head;
            Stack2 stack2 = Stack2Head;
            Stack3 stack3 = Stack3Head;

            textBox5.Clear();
            if (checkBox1.Checked == true && checkBox2.Checked == true && checkBox3.Checked == true)
            {
                while (stack1 != null || stack2 != null || stack3 != null)
                {
                    textBox5.Text += "P1-" + stack1.y + "\t      P2-" + stack2.y + "\t          P3-" + stack3.y + Environment.NewLine;
                    stack1 = stack1.next;
                    stack2 = stack2.next;
                    stack3 = stack3.next;
                }
            }
            else if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                while (stack1 != null && stack2 != null)
                {
                    textBox5.Text += "P1-" + stack1.y + "\t      P2-" + stack2.y + "\t" + Environment.NewLine;
                    stack1 = stack1.next;
                    stack2 = stack2.next;
                }
            }
            else if (checkBox2.Checked == true && checkBox3.Checked == true)
            {
                while (stack2 != null && stack3 != null)
                {
                    textBox5.Text += "\t      P2-" + stack2.y + "\t          P3-" + stack3.y + Environment.NewLine;
                    stack2 = stack2.next;
                    stack3 = stack3.next;
                }
            }
            else if (checkBox1.Checked == true && checkBox3.Checked == true)
            {
                while (stack1 != null && stack3 != null)
                {
                    textBox5.Text += "P1-" + stack1.y + "\t \t          P3-" + stack3.y + Environment.NewLine;
                    stack1 = stack1.next;
                    stack3 = stack3.next;
                }
            }
            else if (checkBox1.Checked == true)
            {
                while (stack1 != null)
                {
                    textBox5.Text += "P1-" + stack1.y + Environment.NewLine;
                    stack1 = stack1.next;
                }
            }
            else if (checkBox2.Checked == true)
            {
                while (stack2 != null)
                {
                    textBox5.Text += "\t      P2-" + stack2.y + Environment.NewLine;
                    stack2 = stack2.next;
                }
            }
            else if (checkBox3.Checked == true)
            {
                while (stack3 != null)
                {
                    textBox5.Text += "\t \t          P3-" + stack3.y + Environment.NewLine;
                    stack3 = stack3.next;
                }
            }
        }

    }
}


