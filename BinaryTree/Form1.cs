using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace AgacProje
{
    public partial class Form1 : Form
    {
        private BinaryTree myTree;
        private int countRemove = 0;


        public Form1()
        {
            InitializeComponent();
            myTree = new BinaryTree();  //Form_load oldugunda BT nesnesini burda oluşturuyorum.
        }
        public static bool IsTextBoxEmpty(TextBox textBox)    //metin kutusunun içi boş mu kontrolü?
        {
            return string.IsNullOrEmpty(textBox.Text);    //IsNullOrEmpty boşluk mu değil mi kontrolü true dönerse  ana fonk true döner messagebox uyarı verir
        }

        private void Ekle_Click(object sender, EventArgs e)
        {
            if (IsTextBoxEmpty(textBox1))
                MessageBox.Show("Lütfen eklemek istediğiniz değeri giriniz.");

            else  //Ağaca Ekleme İşlemi
            {
                myTree.Add(int.Parse(textBox1.Text));
                label11.Text = myTree.getNodeCounter().ToString();  //BinaryTree sınıfı üzerinden getNodeCounter la toplam düğüm sayısını çekip stringe dönüştürüp,labela yazdırıyorum.
                textBox1.Clear();
                textBox1.Focus();
            }
        }

        private void Silme_Click(object sender, EventArgs e)
        {
            if (IsTextBoxEmpty(textBox2))
                MessageBox.Show("Lütfen silmek istediğiniz değeri giriniz.");
            else
            {
                myTree.Remove(int.Parse(textBox2.Text));
                countRemove++;
                label13.Text = countRemove.ToString();  //Silinenleri countRemove değişkeninde tutup burda toString formatında label'a aktarıyorum.
                textBox2.Clear();
                textBox2.Focus();
            }
        }

        private void Bulma_Click(object sender, EventArgs e)
        {
            if (IsTextBoxEmpty(textBox3))
                MessageBox.Show("Lütfen aramak istediğiniz değeri giriniz.");
            
            else
            {
                BinaryTree foundNode = myTree.Search(int.Parse(textBox3.Text));  //chain code prensibiyle, myTree ikiliağaç türündeki nesnemin nonstatic fonksiyonuna erişim sağlayarak ilgili aranan dugumu arıyorum.

                if (foundNode != null)
                {
                    MessageBox.Show($"Değer bulundu: {foundNode.getValue()}");
                    label16.Text = $"Düğüm Düzeyi: {myTree.FindNodeLevel(foundNode.getValue())}";
                }
                else
                    MessageBox.Show("Değer bulunamadı.");
                
                textBox3.Clear();
                textBox3.Focus();
            }
        }
        private void Agac_Click(object sender, EventArgs e)
        {
            textBox6.Text = myTree.Traverse(BinaryTree.TraversalType.Preorder); //PreOrder  (KÖK-SOL-SAĞ)
            textBox7.Text = myTree.Traverse(BinaryTree.TraversalType.Inorder);  //Inorder    (SOL-KÖK-SAĞ)
            textBox8.Text = myTree.Traverse(BinaryTree.TraversalType.Postorder);  //PostOrder  (SOL-SAĞ-KÖK)
            textBox9.Text = myTree.LeavesString();   
            textBox11.Text = myTree.getNodeCounter().ToString(); //Toplam Düğüm Sayısı
            textBox12.Text = myTree.Height().ToString();  //Ağacın Yüksekliği
            textBox4.Text = myTree.Max().ToString();  
            textBox5.Text = myTree.Min().ToString();  

        }
        private void DisplayTreeNodes(BinaryTree current, TreeNode parent)  //AĞAÇ  GOSTERME ISLEMI
        {
            if (current != null) { //current BT dugumu ilgili örneğin rastgele bir <15> değeri için sorgu yapıyor null mu değil mi diye 
                TreeNode node;  

                if (parent == null)  //eğer parent dugumse ve altında bir dugum yoksa(null gösteriyorsa) 
                {
                    node = new TreeNode(current.getValue().ToString());  //yeni bir dugum oluşturup , Add fonksiyonu ile eklenir.
                    treeView1.Nodes.Add(node);
                }
                else   
                {
                    node = new TreeNode(current.getValue().ToString()); //eğer parent node değilse parent node olarak eklenir.
                    parent.Nodes.Add(node);
                }

                DisplayTreeNodes(current.getLeft(), node);      //eğer null değerse solunu veya sağına gidip tekrar aynı islemleri uyguluyor.(recursive)
                DisplayTreeNodes(current.getRight(), node); 
            }
        }

        private void agacekle_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            DisplayTreeNodes(myTree.getRoot(), null);  //myTree.getRoot() ile ilk root değeri ve bu değerin hiçbir alt değeri olmadığı için null parametrelerini gönderiyorum.
            treeView1.ExpandAll();
        }

     
    }
}
 
