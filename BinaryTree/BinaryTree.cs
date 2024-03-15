using System;
using System.Collections.Generic;
using System.Text;

namespace AgacProje
{
    public class BinaryTree
    {
        private int value;    
        private int level;
        private BinaryTree left;
        private BinaryTree right;
        private BinaryTree back;
        private static BinaryTree root;
        private int nodeCounter = 0;
        //private attribute'ları getter ve setter kullanarak encapsulation uyguladım.
        public BinaryTree getRoot()
        {
            return root;
        }
        public int getNodeCounter()
        {
            return nodeCounter;
        }
        public int getValue()
        {
            return value;
        }
        public void setValue(int value)
        {
            this.value = value;
        }
        public BinaryTree getLeft()
        {
            return left;
        }
        public void setLeft(BinaryTree left)
        {
            this.left = left;
        }
        public BinaryTree getRight()
        {
            return right;
        }
        public void setRight(BinaryTree right)
        {
            this.right = right;
        }
        public int getLevel()
        {
            return level;
        }
        public void setLevel(int level)
        {
            this.level = level;
        }
        public void setBack(BinaryTree back)
        {
            this.back = back;
        }
        public BinaryTree getBack()
        {
            return back;
        }
        

        private BinaryTree(int value, BinaryTree left, BinaryTree right, int level, BinaryTree back)
        {
            this.value = value;
            this.left = left;
            this.right = right;
            this.level = level;
            this.back = back;          //Her Yeni Düğüm nesnesi için değer ataması with ctor
        }
        public BinaryTree()
        {
            // ilk değer  ataması (null,0,0.0 vs.)
        }
        public void Add(int value) { root = AddRecursive(root, value, null, 1); }
        public BinaryTree Search(int value) { return SearchRecursive(root, value); }
        public int FindNodeLevel(int value) { return FindNodeLevelRecursive(root, value, 1); }
        public void Remove(int value) { root = RemoveRecursive(root, value); }
        public int Min() { return FindExtremeNode(root, isMaximum: false); }  //findExtremeNode metodu çağrılıp root,ve isMaximum false parametresi gönderdim ki false olursa min değeri döndürsün ortak bir metotta işlem yapalım , true gonderınce de max degerı gerı dondureyım.
        public int Max() { return FindExtremeNode(root, isMaximum: true); }
        public int Height() { return Height(root); }

        private BinaryTree AddRecursive(BinaryTree current, int value, BinaryTree parent, int level)  //Ağaç Ekleme İslemi
        {
            if (current == null)      //bulunan dugum dugumun degeri, parent dugumu ve duzeyi parametre olarak gönderilip , bulunan dugum null mu kontrolu yapılır eger nullsa zaten altına deger eklenemsi gerekir.
            {
                nodeCounter++;
                return new BinaryTree(value, null, null, level, parent);  //1 tane binarytree nesnesi eklenir ve dugumcoounter 1 tane arttırılır. BT nesnesi aslında dugumsu agactır. dugume eleman eklenince agac olacak.
            }

            if (value < current.value)                 //eğer current dugum null degılse buraya gelicek ve  current dugumun degerıyle eklenecek degerın karsılastırması yapılacak duruma gore fonksıyon tekrar cagrılacak
                current.left = AddRecursive(current.left, value, current, level + 1);
            else if (value > current.value)
                current.right = AddRecursive(current.right, value, current, level + 1);

            return current;
        }
        private BinaryTree SearchRecursive(BinaryTree current, int value)   //ılgılı dugum ve dugumun degerı gonderılıp dugumun null olup olmadıgı kontrol edıldıkten sonra , deger kucukse soldan tekrar arama buyukse sagdan tekrar arma yapılr.
        {
            if (current == null || current.value == value)
                return current;
            if (value < current.value)
                return SearchRecursive(current.left, value);
            return SearchRecursive(current.right, value);
        }

        private int FindNodeLevelRecursive(BinaryTree currentNode, int value, int level)  //ilgili dügümün düzeyini bulma.
        {
            if (currentNode == null)         //aranan dugumun degerıyle current deger karsılastırılır eslestıgınde duzey degerı gonderır..
                return 0;
            if (value == currentNode.getValue())
                return level;
            if (value < currentNode.getValue())
                return FindNodeLevelRecursive(currentNode.getLeft(), value, level + 1);
            return FindNodeLevelRecursive(currentNode.getRight(), value, level + 1);
        }
        private BinaryTree RemoveRecursive(BinaryTree current, int value) //AğactanDugumSılme ****
        {
            if (current == null)                               //current dugum ve deger gonderilir eger dugum null sa dırek null donulur cunkı sılınecek bır deger yoktur, yani silinirse agac silinir.
                return null;

            if (value == current.value)             //sonrasında current.value sıyle sılınmek ıstenen deger esıtse içerisinde solu ve sağının null olup olmaması karsılastırılır eger solu null sa sağ değeri sağı null sa solundaki degeri siler.
            {
                if (current.left == null)
                    return current.right;

                else if (current.right == null)
                    return current.left;

                current.value = FindExtremeNode(current.right, isMaximum: false);        //sonrasında currentValue degerını min'e atar.
                current.right = RemoveRecursive(current.right, current.value);           //current.right'ı da removeRecursive de tekrar cagırır.
            }
            else if (value < current.value)                                     //eğer bizim silecegimiz deger current degerden dusukse soldaki deger sılınır. buyukse sağ daki değer sılınır.
                current.left = RemoveRecursive(current.left, value);
            else
                current.right = RemoveRecursive(current.right, value);
            return current;
        }
       
        private int FindExtremeNode(BinaryTree node, bool isMaximum)  //while(case 1 || case2) ikisininden herhangi birinin sonucu 1 oldugunda ternary calısır isMaximumun değerine göre de min yada max degeri geri döndürür.
        {
            while ((isMaximum && node.right != null) || (!isMaximum && node.left != null))
            {
                node = isMaximum ? node.right : node.left;
            }
            return node.value;
        }

        //derinlik -1 , height +1
        private int Height(BinaryTree node)   //Heigh fonk'da recursive çalışıyor eğer dugum null ise yukseklıgı olmamıs oluyor, eger null degılse sol ve sagdan yukseklıklerıne bakılıyor.
        { //Math.max fonk ile sol ve sağ değerleri alınıp hangisi daha yüksekse o deger veriliyor ve degere 1 ekleniyor.(yukseklık bulurken +1 cunku root da var)
            if (node == null)
                return 0;

            int leftHeight = Height(node.left);
            int rightHeight = Height(node.right);
            return Math.Max(leftHeight, rightHeight) + 1;
        }

        public enum TraversalType
        {
            Preorder,
            Inorder,
            Postorder
        }

        public string Traverse(TraversalType traversalType)
        { 
            StringBuilder result = new StringBuilder();   //String nesnesine append ile ekleme yapıyoruz assagıda.
            Traverse(root, traversalType, result);
            return result.ToString().Trim();
        }

        private void Traverse(BinaryTree node, TraversalType traversalType, StringBuilder result)
        {
            if (node != null)
            { 
                switch (traversalType)  //enumlar switch-case içerisinde yazılabiliyor.
                {
                    case TraversalType.Preorder:           //preorder case için 
                        result.Append($"{node.value} ");                    //kök -sol - sağ  //herbir kök için , sol için , sağ için Traverse üzerinde gezilir
                        Traverse(node.left, traversalType, result);        
                        Traverse(node.right, traversalType, result);       
                        break;


                    case TraversalType.Inorder:                 //ınorder case için
                        Traverse(node.left, traversalType, result);  //sol-kök-sağ
                        result.Append($"{node.value} ");                
                        Traverse(node.right, traversalType, result);
                        break;

                    case TraversalType.Postorder:                       //postorder case için sol-sağ-kök
                        Traverse(node.left, traversalType, result);
                        Traverse(node.right, traversalType, result);
                        result.Append($"{node.value} ");
                        break;

                    default:
                        throw new ArgumentException("Invalid traversal type");
                }
            }
        }
        public string LeavesString()
        {
            StringBuilder result = new StringBuilder();  //StringBuilder la String nesnesi üretiyorum ve ilgili yaprakları bu string nesnesine ekliyorum.
            LeavesString(root, result);
            return result.ToString().Trim();
        }
        private void LeavesString(BinaryTree node, StringBuilder result)  //ilgili düğüm ve string nesnesi parametre olarak geliyor node değeri null değilse ve dugumun solu ve sagı null ise bu yapraktır dıyıp string nesnesine ekleme yapıyoruz.
        {               //bu islem recursive calısıyor. eger solu ve sagı varsa solu ve sagı ıcın aynı fonksiyon tekrar cagrılıp karsılastırma yapılıyor.
            if (node != null)
            {
                if (node.left == null && node.right == null)
                    result.Append($"{node.value} ");
                
                LeavesString(node.left, result);
                LeavesString(node.right, result);
            }
        }
    }
}
