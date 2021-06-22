# .Net-BinarySearchTree

Problem 2 (drzewo BST)
(A) Zaimplementuj operacje (search, insert, delete, KLP) na drzewie BST.

* Węzeł drzewa BST powinien zawierać: klucz (typu int), wskaźnik na lewe poddrzewo, wskaźnik na 
prawe poddrzewo.
* W węźle drzewa nie można umieszczać wskaźnika na ojca tego węzła.

Do testowania drzewa BST przygotuj:

(a) Wczytywanie elementów (typu int) z pliku InTest1.txt do drzewa BST:

InTest1.txt

46 43 52 46 45 765 73 5 63 45 4 65 67 65 73 56 24 53 42 34 23 465 376 93 65 8

(b) Losowanie elementów (typu int) z ustalonego zakresu i wczytywanie ich do drzewa BST (kolejność losowanych elementów powinna zostać zapisana w pliku OutTest2.txt).

(c) Wykonanie operacji w wyniku wyboru pozycji z następującego menu.

* Zapisz elementy drzewa BST (wraz z wagami umieszczonymi w nawiasach) do pliku OutTest3.txt w 
kolejności KLP.
* Dodaj element do drzewa BST.
* Usuń element z drzewa BST.
* Wypisz elementy drzewa BST.

(B) Metoda Callana polega na powtarzaniu przygotowanych zdań sformułowanych na bazie wprowadzonych  wcześniej wyrazów. Podręcznik do Callana składa się z pytań i odpowiedzi w języku angielskim, poprzedzonych „nowymi słówkami” wraz z tłumaczeniami. Autor książki jest nieco „roztargnionym” panem i  zgubił plik z „nowymi” słówkami do 8 rozdziału. Spowodowało to zastój przy tworzeniu kolejnych rozdziałów, ponieważ nie było wiadomo, które słówka są „nowe” a które nie. Przyjaciel „roztargnionego” zlitował się nad kolegą i napisał program pomocny w konstrukcji podręcznika.

Program składa się z dwóch części.

W pierwszym kroku, w celu odnalezienia „nowych” słówek, czytany jest tekst zawarty w pliku In0502.txt. Zadanie polega na sprawdzeniu, czy kolejne wyrazy znajdują się już w angielskim drzewie BST. Przy napotkaniu „nowego” wyrazu, program zapisuje go do pliku OutA0502.txt i na angielskim drzewie BST. Następnie, w celu skonstruowania (polskiego) drzewa tłumaczeń, należy ręcznie uzupełnić w pliku  OutA0502.txt polskie tłumaczenia dla zapisanych tam wyrazów angielskich.

W drugim kroku tworzone jest (polskie) drzewo tłumaczeń. Program powinien czytać kolejne wyrazy angielskie wraz z polskimi tłumaczeniami i generować polskie drzewo BST. Każdy nowo utworzony węzeł polskiego drzewa powinien być łączony wskaźnikowo z odpowiednim węzłem drzewa angielskiego. Następnie należy przejrzeć angielskie drzewo BST w porządku KLP i wraz z polskimi tłumaczeniami odpowiednio zapisać do pliku OutB0502.txt.

Po uzupełnieniu obu drzew, program powinien mieć możliwość realizacji zadań wynikających z następującego menu:

* wypisanie wyrazów angielskich (polskich) wraz z tłumaczeniami w kolejności KLP (do pliku)
* znalezienie wyrazu polskiego (angielskiego) wraz z tłumaczeniem
* dodanie nowego słowa wraz z tłumaczeniem
* usunięcie słowa wraz z tłumaczeniem

Przykład

In0502.txt

When do you think you will be ready to take the exam at the end of this book

I think I will be ready to take the exam at the end of this book in March

OutA0502.txt

when’\n’ do’\n’ you’\n’ think’\n’ will’\n’ be’\n’ ready’\n’ to’\n’ take’\n’ the’\n’ exam’\n’ at’\n’ end’\n’ of’\n’ this’\n’ book’\n’ I’\n’ in’\n’ March

OutB0502.txt
ready gotowy’\n’exam egzamin’\n’do robić’\n’be być’\n’at’ w\n’ book książka’\n’end koniec’\n’in w’\n’I ja’\n’of z’\n’ March marzec’\n’think myśleć’\n’take zdawać’\n’the ‘the’’\n’when kiedy’\n’ to do’\n’this ten’\n’you ty’\n’will wola

[Uwagi]
Należy stosować optymalne rozwiązania. Dla ułatwienia zakładamy, że:

* Wyrazy nie posiadają synonimów. W przypadku, gdy użytkownik wpisuje synonim, powinien pojawić się odpowiedni komunikat.
* Każdy zwrot (np. in order to, at the end of) złożony z kilu wyrazów jest tłumaczony jako zbiór oddzielnych słów.
* W czytanym tekście nie ma znaków interpunkcyjnych. Wyrazy oddzielone są pojedynczą spacją.
* Wyrazy, dla których nie ma tłumaczeń (np. ‘the’) należy w polskim słowniku zapisać w apostrofach ‘the'.
