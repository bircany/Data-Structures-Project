using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BaglıListeProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class Node
        {
            public int code;
            public string name;
            public int price;

            public Node next;
            public Node prev;
        }
        Node head = null;
        Node tail = null;
        private void Ekle(object sender, EventArgs e)
        {
            Node newNode = new Node();
            newNode.code = Convert.ToInt32(textBox1.Text);
            newNode.name = textBox2.Text;
            newNode.price = Convert.ToInt32(textBox3.Text);
            textBox1.Focus();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear(); 

            if (isExistCode(newNode.code))  //Code Control.
            {
                MessageBox.Show("Aynı kod numaralı ürün olamaz, Farklı bir kod girin.");
                return;
            }

            if (head == null)     //Listeye ilk eleman ekleme.
                head = tail = newNode;

            else
            {              
                if (head.code > newNode.code) //Başa Ekle
                {
                    newNode.next = head;
                    head.prev = newNode;
                    newNode.prev = null;
                    head = newNode;
                }
                else if (tail.code < newNode.code) //SonaEkle
                {
                    tail.next = newNode;
                    newNode.prev = tail;
                    newNode.next = null;
                    tail = newNode;
                }
                else
                {
                    for (Node temp = head;temp.next != null;temp = temp.next)
                    {
                        if (temp.next.code > newNode.code) //Araya Ekle
                        {
                            newNode.next = temp.next;
                            temp.next.prev = newNode;
                            temp.next = newNode;
                            newNode.prev = temp;
                            break;
                        }
                    }
                }
            }
        }
        private bool isExistCode(int code)
        {      
            for(Node temp = head; temp != null; temp = temp.next)
            {
                if (temp.code == code)
                    return true;
            }
            return false;
        }
        private void Listele(object sender, EventArgs e)
        {  
            dataGridView1.Rows.Clear();
            for (Node temp = head;temp!= null;temp = temp.next)  
            {
                dataGridView1.Rows.Add(temp.code, temp.name, temp.price);
            }
        }
        private void Guncelle(object sender, EventArgs e)
        {
            int search = Convert.ToInt32(textBox9.Text);
            Node temp;
            for (temp = head; temp != null; temp = temp.next)        
                if (temp.code == search)
                {
                    temp.price = Convert.ToInt32(textBox8.Text);
                    break;
                }
            
          

            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }

        private void Sil(object sender, EventArgs e)
        {
            
            int silinecek = Convert.ToInt32(textBox6.Text);
            textBox6.Clear();
            textBox4.Clear();
            textBox5.Clear();

            if (head == null)
                MessageBox.Show("Liste Boş, Silinecek bir değer yok.");
            else
            {
                if (head.code == silinecek)
                {
                    if (head == tail)
                        head = tail = null;

                    else
                    {
                        head = head.next;
                        head.prev = null;
                    }
                }
                else
                {
                    for (Node temp = head; temp != null; temp = temp.next)
                    {
                        if (temp.code == silinecek)
                        {
                            if (temp == tail)       //SondanSil
                            {
                                tail.next = null;
                                tail = tail.prev;
                            }
                            else                    //AradanSil
                            {
                                temp.prev.next = temp.next;
                                temp.next.prev = temp.prev;
                            }
                            break;
                        }
                    }
                }
            }
        }
        private void Bul(object sender, EventArgs e, TextBox searchTextBox, TextBox nameTextBox, TextBox priceTextBox)
        {
            Node temp;
            for (temp = head; temp != null; temp = temp.next)
            {
                if (temp.code == Convert.ToInt32(searchTextBox.Text))
                {
                    nameTextBox.Text = temp.name;
                    priceTextBox.Text = temp.price.ToString();
                    return;
                }
            }
            if (temp == null && tail.code == Convert.ToInt32(searchTextBox.Text))
            {
                nameTextBox.Text = tail.name;
                priceTextBox.Text = tail.price.ToString();
            }
        }
        private void SilinecekBul(object sender, EventArgs e)
        {
            Bul(sender, e, textBox6, textBox4, textBox5);
        }
        private void GüncellenecekBul(object sender, EventArgs e)
        {
            Bul(sender, e, textBox9, textBox7, textBox8);
        }
        private void VeriAlma(object sender, EventArgs e)
        {
            textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           
            textBox9.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}


