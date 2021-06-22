using System;
using System.IO;

class Node
{
    int key;
    Node left, right;

    public int Key
    {
        get => key;
        set => key = value;
    }
    public Node Left
    {
        get => left;
        set => left = value;
    }
    public Node Right
    {
        get => right;
        set => right = value;
    }
    public Node()
    {
        //key = null;
        left = null;
        right = null;
    }
    public Node(int value_)
    {
        key = value_;
        left = null;
        right = null;
    }

}
class BST_Tree
{
    Node root;
    public Node Root
    {
        get => root;
        set => root = value;
    }
    public void Flush_Savefile() //reset pliku wyjsciowego
    {
        StreamWriter writer = new StreamWriter("OutTest3.txt", false);
        writer.Close();
    }
    public void Save_KLP(Node node, string filename) //zapis w kolejnosci KLP
    {
        if (!(node is null))
        {
            StreamWriter sw = new StreamWriter(filename + ".txt", true);
            sw.Write(node.Key + " ");
            sw.Close();
            Save_KLP(node.Left, filename);
            Save_KLP(node.Right, filename);
        }
    }
    public void Print_KLP(Node node) //wypisanie w kolejnosci KLP do konsoli
    {
        if (!(node is null))
        {
            Console.Write(node.Key + " ");
            Print_KLP(node.Left);
            Print_KLP(node.Right);
        }
    }
    public bool insert(int key)
    {
        Node search=root;
        if (search is null) //brak korzenia - wstawienie elementu w korzeniu
        {
            root= new Node(key);
            return true;
        }

        while (!(search is null))
        {
            if (key == search.Key) //jesli klucz znaleziono - false - brak wstawienia
            {
                return false;
            }
            if (key < search.Key)
            {
                if (search.Left is null) //element wstawiany mniejszy od przegladanego wezla i lewy wezel - null
                {
                    search.Left = new Node(key);    //wstawienie w lewej galezi search
                    return true;
                }
                else
                    search = search.Left; //przejscie do lewej galezi
            }
            else
            {
                if (search.Right is null) //element wstawiany wiekszy od search i prawy wezel - null
                {
                    search.Right = new Node(key); //wstawienie w prawej galezi search
                    return true;
                }
                else
                    search = search.Right; //przejscie do prawej galezi
            }
        }
        return false; //brak wstawienia/nie powiodlo sie, 
    }
    public Node search(int key)
    {
        if (Root is null) //jesli nie ma korzenia - false
        {
            return null;
        }

        Node search=Root;

        while (!(search is null))
        {
            if (key == search.Key) //jesli znaleziono - true
            {
                return search;
            }
            if (key < search.Key)       
                search = search.Left;   //element szukany mniejszy - przejscie do lewej galezi
            else
                search = search.Right;  //element szukany wiekszy - prawa galaz
        }
        return null;
    }
    
    public bool delete(int key)
    {
        //usuwanie dzieje się z poziomu ojca elementu
        Node search = root; //poczatek przeszukiwania
        Node left = null; //do zapamietania lewej i prawej galezi usuwanego elementu
        Node right = null; //^ w celu przypisania ich do podstawianego obiektu
        int delete=-1;   // 0 - usuwany search.left,   1- usuwany search.right

        if (key == search.Key)  //jesli usuwany jest korzen
        {
            left = search.Left; //zapis lewego i prawego syna korzenia
            right = search.Right;
            if (search.Left is null)
            {
                if (search.Right is null) //brak poddrzew w korzeniu - usun korzen
                {
                    root = null;
                }
                else //tylko prawe poddrzewo w korzeniu
                {
                    root = right;
                }
            }
            else
            {
                if (search.Right is null) //tylko lewe poddrzewo w korzeniu
                {
                    root = left;
                }
                else // 2 poddrzewa w korzeniu
                {
                    Node next = right; //prawe poddrzewo korzenia
                    Node next_pred = null;    //poprzednik nastepnika korzenia
                    if (next.Left is null)      //jesli nastepnik jest bezposrednio prawym synem korzenia
                    {
                        root = next;            //prawy syn nowym korzeniem
                        root.Left = left;       //przypisanie lewego poddrzewa starego korzenia
                        return true;
                    }
                    else
                    {
                        while (!(next.Left is null)) //znalezienie najmniejszego el. w prawym poddrzewie korzenia
                        {
                            next_pred = next;
                            next = next.Left;
                        }
                        root = next;
                        next_pred.Left = root.Right; //podstawienie prawego poddrzewa z zabranego nastepnika w jego miejsce
                        root.Right = right;
                        root.Left = left;
                        return true;
                    }
                }
            }
        }
       
        //jesli szukany element nie byl korzeniem
        while (!(search is null))
        {
            if (key < search.Key) //jesli element jest mniejszy
            {
                if (!(search.Left is null)) //jesli istnieje element w lewym poddrzewie
                {
                    if (search.Left.Key == key) //jesli element z lewego poddrzewa jest rowny szukanemu kluczowi
                    {
                        left = search.Left.Left;    //zapis poddrzew elementu
                        right = search.Left.Right;
                        delete = 0;
                        break;
                    }
                    else
                        search = search.Left; //jesli klucz nie jest rowny szukanemu przejscie do poddrzewa
                }
                else
                {
                    return false; //nie znaleziono elementu
                }
            }
            else
            {
                if (!(search.Right is null)) //prawe poddrzewo sprawdzane analogicznie
                {
                    if (search.Right.Key == key)
                    {
                        left = search.Right.Left;
                        right = search.Right.Right;
                        delete = 1;
                        break;
                    }
                    else
                        search = search.Right;
                }
                else
                {
                    return false;
                }
            }
        }
        
        if (search is null)
            return false;

        //delete==0, kiedy usuniety ma byc search.left
        //delete==1, kiedy usuniety ma byc search.right
        if (delete==0)
        {
            if (left is null)
            {
                if (right is null) //brak nastepnikow
                    search.Left = null;
                else
                {
                    search.Left = null;
                    search.Left = right;
                }
            }
            else
            {
                if (right is null)
                {
                    search.Left = null;
                    search.Left = left;
                }
                else //jesli usuwany element ma prawy i lewy korzen
                {
                    Node next = right; //znalezienie nastepnika usuwanego elementu
                    Node next_pred = null;

                    if (next.Left is null)      
                    {
                        search.Left = next;           
                        search.Left.Left = left;      
                        return true;
                    }
                    else
                    {
                        while (!(next.Left is null)) //znalezienie najmniejszego el. w prawym poddrzewie 
                        {
                            next_pred = next;
                            next = next.Left;
                        }
                        search.Left = next;
                        next_pred.Left = search.Left.Right; //podstawienie prawego poddrzewa z zabranego nastepnika w jego miejsce
                        search.Left.Right = right;
                        search.Left.Left = left;
                        return true;
                    }
                }
            }

        }
        else
        {
            if (left is null)
            {
                if (right is null) //brak nastepnikow
                    search.Right = null;
                else
                {
                    search.Right = null;
                    search.Right = right;
                }
            }
            else
            {
                if (right is null)
                {
                    search.Right = null;
                    search.Right = left;
                }
                else //jesli usuwany element ma prawy i lewy korzen
                {
                    Node next = right; //znalezienie nastepnika usuwanego elementu
                    Node next_pred = null;

                    if (next.Left is null)
                    {
                        search.Right = next;
                        search.Right.Left = left;
                        return true;
                    }
                    else
                    {
                        while (!(next.Left is null)) //znalezienie najmniejszego el. w prawym poddrzewie 
                        {
                            next_pred = next;
                            next = next.Left;
                        }
                        search.Right = next;
                        next_pred.Left = search.Right.Right; //podstawienie prawego poddrzewa z zabranego nastepnika w jego miejsce
                        search.Right.Right = right;
                        search.Right.Left = left;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public BST_Tree()
    {
        root = null;
    }
}

class Zadanie_A
{
    BST_Tree BST;
    string menu;

    public void Insert_File()
    {
        using (StreamReader sr = new StreamReader("InTest1.txt"))
        {
            string[] split;
            string read;
            read = sr.ReadLine();
            split = read.Split();

            for(int i=0;i<split.Length;i++)
            {
                BST.insert(Int32.Parse(split[i]));
            }
        }
    }
    public void Insert_Random(int amount, int min, int max)
    {
        Random rand = new Random();
        int a;
        using (StreamWriter sw=new StreamWriter("OutTest2.txt"))
        {
            for(int i=0; i<amount; i++)
            {
                a=rand.Next(min, max);
                sw.Write(a+", ");
                BST.insert(a);
            }
        }
    }
    public void Menu()
    {
        int key;
        int ile, min, maks;
        int a=0;
        while (this.menu != "0")
        {
            Console.WriteLine("1: Wypisz i zapisz drzewo do pliku OutTest3.txt");
            Console.WriteLine("2: Dodaj element");
            Console.WriteLine("3: Usuń element");
            Console.WriteLine("4: Znajdź element");
            Console.WriteLine("5: Wstaw losowo");
            Console.WriteLine("5: Usuń losowo");
            Console.WriteLine("0: Wyjście");
            menu = Console.ReadLine();
            switch (this.menu)
            {
                case "0": break;
                case "1":
                    BST.Flush_Savefile(); //reset pliku wyjsciowego
                    BST.Save_KLP(BST.Root, "OutTest3"); //zapis do pliku
                    Console.WriteLine("Drzewo BST(KLP):");
                    BST.Print_KLP(BST.Root); //wypisanie pliku w konsoli
                    Console.WriteLine("\n");
                    break;
                case "2":
                    Console.WriteLine("Podaj element do wstawienia: ");
                    key=Int32.Parse(Console.ReadLine());
                    BST.insert(key);
                    break;
                case "3":
                    Console.WriteLine("Podaj element do usunięcia: ");
                    key = Int32.Parse(Console.ReadLine());
                    BST.delete(key);
                    break;
                case "4":
                    Console.WriteLine("Podaj element do wyszukania: ");
                    key = Int32.Parse(Console.ReadLine());
                    if (BST.search(key) is null)
                        Console.WriteLine("Element nie występuje w drzewie.");
                    else
                        Console.WriteLine("Element występuje w drzewie.");
                    break;
                case "5":

                    Console.WriteLine("Podaj ilosc: ");
                    ile = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Podaj minimum: ");
                    min = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Podaj maksimum: ");
                    maks = Int32.Parse(Console.ReadLine());
                    Insert_Random(ile, min, maks);
                    break;
                case "6":
                    //int ile, min, maks;
                    Console.WriteLine("Podaj ilosc: ");
                    ile = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Podaj minimum: ");
                    min = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Podaj maksimum: ");
                    maks = Int32.Parse(Console.ReadLine());
                    Random rand = new Random();
                    
                    for (int i = 0; i < ile; i++)
                    {
                    a = rand.Next(min, maks);
                    //sw.Write(a + ", ");
                    BST.delete(a);
                    }
                    
                    break;
                default: Console.WriteLine("Nieznana opcja"); break;
            }
        }
    }

    public Zadanie_A()
    {
        BST = new BST_Tree();
        menu = "-1";
    }
}

class Node_Text
{
    string key;
    Node_Text left, right; //wezly lewy prawy
    Node_Text translation; //powiazanie do drugiego slownika

    public string Key
    {
        get => key;
        set => key = value;
    }
    public Node_Text Left
    {
        get => left;
        set => left = value;
    }
    public Node_Text Right
    {
        get => right;
        set => right = value;
    }
    public Node_Text Translation
    {
        get => translation;
        set => translation = value;
    }
    public Node_Text()
    {
        left = null;
        right = null;
    }
    public Node_Text(string value_)
    {
        key = value_;
        left = null;
        right = null;
    }
}

//BST tekstowe sortowane 
class BST_Text 
{
    Node_Text root;
    public Node_Text Root
    {
        get => root;
        set => root = value;
    }
    public void Flush_Savefile(string filename) //czysczenie pliku wynikowego
    {
        StreamWriter writer = new StreamWriter(filename+".txt", false);
        writer.Close();
    }
    public void Save_KLP(Node_Text node, string filename) //zapis w formacie klp
    {
        if (!(node is null))
        {
            StreamWriter sw = new StreamWriter(filename + ".txt", true);
            sw.Write(node.Key + " ");
            sw.Close();
            Save_KLP(node.Left, filename);
            Save_KLP(node.Right, filename);
        }
    }
    public void Print_KLP(Node_Text node) //wypisanie w klp do konsoli
    {
        if (!(node is null))
        {
            Console.Write(node.Key + " ");
            Print_KLP(node.Left);
            Print_KLP(node.Right);
        }
    }
    public Node_Text Insert(string key)
    {
        Node_Text search = root;
        if (search is null)
        {
            root = new Node_Text(key);
            return root;
        }

        while (!(search is null))
        {
            if ((string.Compare(key, search.Key, true) ==0))
            {
                return null;
            }
            if (string.Compare(key, search.Key, true) <0)
            {
                if (search.Left is null)
                {
                    search.Left = new Node_Text(key);
                    return search.Left;
                }
                else
                    search = search.Left;
            }
            else
            {
                if (search.Right is null)
                {
                    search.Right = new Node_Text(key);
                    return search.Right;
                }
                else
                    search = search.Right;
            }
        }
        return null;
    }
    public Node_Text Search(string key)
    {
        if (Root is null)
            return Root; //null

        Node_Text search = Root;
        Node_Text result = null;

        while (!(search is null))
        {
            if ((string.Compare(key, search.Key, true) == 0)) //StringComparison.OrdinalIgnoreCase lub / true
            {
                result = search;
                break;
            }
            if (string.Compare(key, search.Key, true) < 0)
                search = search.Left;
            else
                search = search.Right;
        }
        return result;
    }

    public Node_Text Delete(string key)
    {
        Node_Text search = root;
        Node_Text output = null; //do usuwania tlumaczenia w drugim drzewie
        Node_Text left = null;
        Node_Text right = null;
        int delete = 0;

        if ((string.Compare(key, search.Key, true) == 0)) //jeśli szukany obiekt jest korzeniem
        {
            output = search.Translation;
            left = search.Left;
            right = search.Right;
            if (search.Left is null)
            {
                if (search.Right is null)
                {
                    root = null;
                }
                else
                {
                    root = right;
                }
            }
            else
            {
                if (search.Right is null)
                {
                    root = left;
                }
                else
                {
                    Node_Text next = search.Right;
                    Node_Text next_pred = null;
                    if(next.Left is null)
                    {
                        root = next;
                        root.Left = left;
                    }
                    else
                    {
                        while (!(next.Left is null)) //znalezienie najmniejszego el. w prawym poddrzewie korzenia
                        {
                            next_pred = next;
                            next = next.Left;
                        }
                        root = next;
                        next_pred.Left = root.Right; //podstawienie prawego poddrzewa z zabranego nastepnika w jego miejsce
                        root.Right = right;
                        root.Left = left;
                    }
                }
            }
            return output; //output do usuniecia w drugim drzewie
        }

        //jesli element nie byl w korzeniu
        while (!(search is null))
        {
            if (string.Compare(key, search.Key, true) < 0)
            {
                if (!(search.Left is null))
                {
                    if (search.Left.Key == key)
                    {
                        left = search.Left.Left;
                        right = search.Left.Right;
                        output = search.Left.Translation;
                        delete = 0;
                        break;
                    }
                    else
                        search = search.Left;
                }
                else
                    return null;
            }
            else
            {
                if (!(search.Right is null))
                {
                    if (search.Right.Key == key)
                    {
                        left = search.Right.Left;
                        right = search.Right.Right;
                        output = search.Right.Translation;
                        delete = 1;
                        break;
                    }
                    else
                        search = search.Right;
                }
                else
                    return null;
            }
        }

        if (search is null)
            return search;

        //delete==0, kiedy usuniety ma byc search.left
        //delete==1, kiedy usuniety ma byc search.right
        if (delete == 0)
        {
            if (left is null)
            {
                if (right is null)
                {
                    search.Left = null;
                }
                else
                {
                    search.Left = null;
                    search.Left = right;
                }
            }
            else
            {
                if (right is null)
                {
                    search.Left = null;
                    search.Left = left;
                }
                else
                {
                    Node_Text next = search.Left.Right;
                    Node_Text next_pred = null;

                    if (next.Left is null)
                    {
                        search.Left = next;
                        search.Left.Left = left;
                    }
                    else
                    {
                        while (!(next.Left is null)) //znalezienie najmniejszego el. w prawym poddrzewie 
                        {
                            next_pred = next;
                            next = next.Left;
                        }
                        search.Left = next;
                        next_pred.Left = search.Left.Right; //podstawienie prawego poddrzewa z zabranego nastepnika w jego miejsce
                        search.Left.Right = right;
                        search.Left.Left = left;
                    }

                }
            }
        }
        else
        {
            if (left is null)
            {
                if (right is null)
                {
                    search.Right = null;
                }
                else
                {
                    search.Right = null;
                    search.Right = right;
                }
            }
            else
            {
                if (right is null)
                {
                    search.Right = null;
                    search.Right = left;
                }
                else //jesli usuwany element ma prawy i lewy korzen
                {
                    Node_Text next = right; //znalezienie nastepnika usuwanego elementu
                    Node_Text next_pred = null;

                    if (next.Left is null)
                    {
                        search.Right = next;
                        search.Right.Left = left;
                    }
                    else
                    {
                        while (!(next.Left is null)) //znalezienie najmniejszego el. w prawym poddrzewie 
                        {
                            next_pred = next;
                            next = next.Left;
                        }
                        search.Right = next;
                        next_pred.Left = search.Right.Right; //podstawienie prawego poddrzewa z zabranego nastepnika w jego miejsce
                        search.Right.Right = right;
                        search.Right.Left = left;
                    }
                }
            }
        }
        return output;
    }
    public BST_Text()
    {
        root = null;
    }
}

class Zadanie_B
{
    BST_Text english, polish;
    string menu;

    public BST_Text English
    {
        get => english;
    }
    public BST_Text Polish
    {
        get => polish;
    }
    public void Read_File(string filename)
    {
        using (StreamReader sr = new StreamReader(filename + ".txt"))
        {
            StreamWriter sw = new StreamWriter("OutA0502.txt");
            string[] split;
            string read;
            while (!sr.EndOfStream)
            {
                read = sr.ReadLine();
                split = read.Split();

                for (int i = 0; i < split.Length; i++)
                {
                    if (!(english.Insert(split[i]) is null))
                        sw.WriteLine(split[i]);
                }
            }
            sw.Close();
        }
    }
    public void Translate_English(string filename)
    {
        Console.WriteLine("Po wprowadzenie tłumaczeń do pliku OutA0502.txt, naciśnij klawisz Enter");
        Console.ReadLine();

        using (StreamReader sr = new StreamReader(filename + ".txt"))
        {
            string[] split;
            string read;
            Node_Text word_english, word_polish;
            while (!sr.EndOfStream)
            {
                read = sr.ReadLine();
                split = read.Split();

                word_english = english.Search(split[0]); //slowa angielskie sa juz w slowniku

                if (split.Length > 1) //jesli jest wpisane tlumaczenie
                {
                    word_polish = polish.Search(split[1]);
                    if (word_polish is null) //jesli nie istnieje juz tlumaczenie
                    {
                        word_polish = polish.Insert(split[1]);
                        word_english.Translation = word_polish; //przypisanie tlumaczen do siebie
                        word_polish.Translation = word_english;
                    }
                    else //jesli istnieje juz takie tlumaczenie
                    {
                        Console.WriteLine("Słowo już wystapiło w słowniku: " + word_polish.Key);
                        //wstawienie w tlumaczenia polskiego w apostrofach
                        word_polish = polish.Insert("'" + split[0] + "'"); 
                        word_english.Translation = word_polish;
                        word_polish.Translation = word_english;
                        //alternatywnie usuniecie wyrazu angielskiego
                        //english.Delete(split[0]);
                    }
                }
                else
                {
                    word_polish = polish.Insert("'" + split[0] + "'"); //nie wystąpi powtórzenie słów, które znalazłyby się w apostrofach
                    word_english.Translation = word_polish;
                    word_polish.Translation = word_english;
                }

            }
        }
    }
    public void Print_Node(Node_Text node, string filename) //wypisanie klp
    {
        if (!(node is null))
        {
            StreamWriter sw = new StreamWriter(filename + ".txt", true);
            sw.WriteLine(node.Key + " "+node.Translation.Key);
            sw.Close();
            Print_Node(node.Left, filename);
            Print_Node(node.Right, filename);
        }
    }
    public void Print_Dictionary(string filename, BST_Text tree) //wypisanie drzewa
    {
        File.WriteAllText(filename+".txt", "");
        Print_Node(tree.Root, filename);
    }
    public void Menu()
    {
        string key;
        string[] split;
        Node_Text search;
        while (this.menu != "0")
        {
            Console.WriteLine("1: Wypisz słownik angielski w porządku KLP");
            Console.WriteLine("2: Wypisz słownik polski w porządku KLP");
            Console.WriteLine("3: Znajdź słowo angielskie");
            Console.WriteLine("4: Znajdź słowo polskie");
            Console.WriteLine("5: Dodaj słowo z tłumaczeniem");
            Console.WriteLine("6: Usuń słowo angielskie");
            Console.WriteLine("7: Usuń słowo polskie");
            Console.WriteLine("0: Wyjście");

            menu = Console.ReadLine();
            switch (this.menu)
            {
                case "0": break;
                case "1":
                    Print_Dictionary("English_KLP", English);
                    break;
                case "2":
                    Print_Dictionary("Polish_KLP", Polish);
                    break;
                case "3":
                    Console.WriteLine("Podaj szukane słowo: ");
                    key = Console.ReadLine();
                    search=English.Search(key);
                    if (!(search is null))
                        Console.WriteLine(key + " - " + search.Translation.Key);
                    else
                        Console.WriteLine("[!] Słowo nie wystąpiło w słowniku.");
                    break;
                case "4":
                    Console.WriteLine("Podaj szukane słowo: ");
                    key = Console.ReadLine();
                    search = Polish.Search(key);
                    if (!(search is null))
                        Console.WriteLine(key + " - " + search.Translation.Key);
                    else
                        Console.WriteLine("[!] Słowo nie wystąpiło w słowniku.");
                    break;
                case "5":
                    Console.WriteLine("Podaj słowo angielskie do dodania i jego tłumaczenie oddzielone spacją: ");
                    key = Console.ReadLine();
                    split = key.Split();
                    Node_Text word_english;

                    if (split.Length != 2)
                    {
                        Console.WriteLine("[!] Poprawna liczba argumentów to 2.");
                        break;
                    }
                    if (polish.Search(split[1]) is null) // polish nie zawiera słowa ze split[1]
                    {
                        word_english = english.Insert(split[0]);
                        if (!(word_english is null)) // english nie zawierał split[0] - poprawne wstawienie
                        {
                            word_english.Translation = polish.Insert(split[1]); // wstawienie split[1] do polish
                            word_english.Translation.Translation = word_english;
                        }
                        else
                        {
                            Console.WriteLine("[!] Słownik zawiera co najmniej jedno z podanych słów.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("[!] Słownik zawiera co najmniej jedno z podanych słów.");
                        break;
                    }
                    break;
                case "6":
                    Console.WriteLine("Podaj słowo do usunięcia: ");
                    key = Console.ReadLine();
                    search = English.Delete(key);
                    if (!(search is null))
                        if (!(Polish.Delete(search.Key) is null))
                        {
                            Console.WriteLine("Usunięto");
                            break;
                        }
                    Console.WriteLine("[!] Usuwanie nie powiodło się");
                    break;
                case "7":
                    Console.WriteLine("Podaj słowo do usunięcia: ");
                    key = Console.ReadLine();
                    search = Polish.Delete(key);
                    if (!(search is null))
                        if (!(English.Delete(search.Key) is null))
                        {
                            Console.WriteLine("Usunięto");
                            break;
                        }
                    Console.WriteLine("[!] Usuwanie nie powiodło się");
                    break;
                default: Console.WriteLine("[!] Nieznana opcja"); break;
            }
        }
    }
    public Zadanie_B()
    {
        english = new BST_Text();
        polish = new BST_Text();
        menu = "-1";
    }
}
namespace ASD0910
{
    class Program
    {
        static void Main(string[] args)
        {
            Zadanie_A zadanie_a = new Zadanie_A();
            zadanie_a.Insert_File();
            //zadanie_a.Insert_Random(5, 5, 100);
            zadanie_a.Menu();


            Zadanie_B zadanie_b = new Zadanie_B();
            zadanie_b.Read_File("In0502");
            //zadanie_b.Translate_English("OutA0502 - tlumaczenia"); //plik z gotowymi tlumaczeniami
            zadanie_b.Translate_English("OutA0502"); //plik, gdzie trzeba otworzyć i dopisać tłumaczenia
            zadanie_b.Print_Dictionary("OutB0502", zadanie_b.English);
            zadanie_b.Menu();
        }
    }
}
