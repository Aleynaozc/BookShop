using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;


namespace BookShop
{
    class Book : BaseClass
    {
        

        public static List<Book> Books = new List<Book>();
        private static object book;

        public string Name { get; set; } // kitabın adı
        public double CostPrice { get; set; } // maliyet fiyati
        public BookTypeEnums BookType { get; set; }

        public int TaxPercantage { get; set; } // toplam vergiyi belirtir
        public int ProfitMargin { get; set; } // kazanç yüzedesi ör. 15 => %18
        public double Price { get; set; } // ürün fiyatı
        public int QTY { get; set; } // quantity qty -> stokdaki adedi gösterir

        //constructor

        public Book(string _name, double _costPrice, BookTypeEnums _bookTypeEnums, int _taxPercantage = 1, int _profitMargin = 10, int _qty = 1)
        {
            Name = _name;
            CostPrice = _costPrice;
            BookType = _bookTypeEnums;
            TaxPercantage = _taxPercantage;
            ProfitMargin = _profitMargin;
            QTY = _qty;
            Price = calculatePrice(_costPrice, _taxPercantage, _profitMargin);
        }
        public static double calculatePrice(double costPrice, int tax, int profitMargin)
        {
            double taxPrice = (costPrice * tax) / 100;
            double profitPrice = (costPrice * profitMargin) / 100;
            double price = costPrice + taxPrice + profitPrice;
            return price;
        }
        public static void addBook(Book book)
        {
            try
            {

                Books.Add(book);
                //ürün maliyet, hesaplanan metot sayesinde ttutar hesaplandı.
                double amount = CaseTransaction.calculateAmount(book.CostPrice, book.QTY);

                //kasa hareketleri nesnesi oluşturuldu
                CaseTransaction caseTransaction = new CaseTransaction(amount, TransactionTypeEnums.EXPENSE);

                //kasa hareketini kaydetmek için CaseTransaction sınıfındaki  save metodu çağrıldı.
                CaseTransaction.saveCaseTransaction(caseTransaction);
                addBookToFile(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata oluştu : " + ex.Message);

            }
        }
        public static void removeBook(int ID)
        {
            foreach (Book book in Books)
            {
                if (book.ID == ID)
                {
                    Books.Remove(book);
                    break;
                }
            }
        }

        public static void updateBook(int bookID)
        {
            foreach (Book ktpGuncel in Books)
            {
                if (ktpGuncel.ID == bookID)
                {
                    Console.WriteLine(ktpGuncel.ToString());
                    Console.WriteLine("Lütfen güncellemek istediğiniz kitabın ID'sini giriniz.");
                    Console.WriteLine("1 - Kitap İsmi");
                    Console.WriteLine("2 - Maliyet Fiyatı");
                    Console.WriteLine("3 - Kitap Türü");
                    Console.WriteLine("4 - Vergi Oranı");
                    Console.WriteLine("5 - Kazanç Yüzdesi");
                    Console.WriteLine("6 - Ürün fiyatı");
                    Console.WriteLine("7 - Ürün Adedi");
                    Console.WriteLine("8 - Çıkış");
                    int secim = Convert.ToInt32(Console.ReadLine());
                    switch (secim)
                    {
                        case 1:
                            Console.Write("Yeni Kitap Adı: ");
                            string bookName = Console.ReadLine();
                            ktpGuncel.Name = bookName;
                            Console.WriteLine(ktpGuncel.ToString());
                            
                            break;
                        case 2:
                            Console.Write("Yeni Maliyet Fiyatı: ");
                            int maliyetFyt = Convert.ToInt32(Console.ReadLine());
                            ktpGuncel.CostPrice = maliyetFyt;
                            double guncelFyt = Book.calculatePrice(ktpGuncel.CostPrice , ktpGuncel.TaxPercantage , ktpGuncel.ProfitMargin);
                            ktpGuncel.Price = guncelFyt;
                            Console.WriteLine(ktpGuncel.ToString());

                            break;
                        case 3:
                       
                            Console.Write("Yeni Kitap Türünü Giriniz (0-4): ");
                            int urunTyp = Convert.ToInt32(Console.ReadLine());
                            BookTypeEnums BookType = (BookTypeEnums)urunTyp;
                            Console.WriteLine(ktpGuncel.ToString());

                            break;
                        case 4:
                            Console.Write("Yeni Vergi Oranı Giriniz: ");
                            int newTax =Convert.ToInt32(Console.ReadLine());
                            ktpGuncel.TaxPercantage = newTax;
                            double taxPercantage = Book.calculatePrice(ktpGuncel.CostPrice, ktpGuncel.TaxPercantage, ktpGuncel.ProfitMargin);
                            int tax = Convert.ToInt32(taxPercantage);
                            ktpGuncel.Price = tax;
                            Console.WriteLine(ktpGuncel.ToString());

                            break;
                        case 5:
                            Console.Write("Yeni Kazanc Yüzdesini Giriniz: ");
                            int kazanc = Convert.ToInt32(Console.ReadLine());
                            ktpGuncel.ProfitMargin = kazanc;
                            double profitMargin = Book.calculatePrice(ktpGuncel.CostPrice, ktpGuncel.TaxPercantage, ktpGuncel.ProfitMargin);
                            int prmargn = Convert.ToInt32(profitMargin);
                            ktpGuncel.Price = prmargn;
                            Console.WriteLine(ktpGuncel.ToString());
                            break;
                        case 6:
                            Console.Write("Yeni Ürün Fiyatını Giriniz: ");
                            int urunFyt = Convert.ToInt32(Console.ReadLine());
                            ktpGuncel.Price = urunFyt;
                            Console.WriteLine(ktpGuncel.ToString());
                            break;
                        case 7:
                            Console.Write("Yeni Ürün Adedini Giriniz: ");
                            int urunAdet = Convert.ToInt32(Console.ReadLine());
                            ktpGuncel.QTY = urunAdet;
                            Console.WriteLine(ktpGuncel.ToString());
                            break;
                        default:
                            break;



                    }
                }
            }

        }


        //satılacak kitabın Id'si ve kaç kitap olduğu bilgisi alır.
        public static void sellBook(int bookID, int bookqty)
        {
            foreach (Book kitap in Books)
            {
                if (kitap.ID == bookID)
                {
                    if (kitap.QTY>= bookqty)
                    {


                        //Kitap adedinin satıldığında azalması 
                        kitap.QTY = kitap.QTY - bookqty;

                        //satışı kasaya kaydetme işlemleri

                        //satış tutarı hesaplandı
                        double satisTutar = CaseTransaction.calculateAmount(kitap.Price, bookqty);
                        //
                        CaseTransaction kasaHareketi = new CaseTransaction(satisTutar, TransactionTypeEnums.INCOMING);
                        CaseTransaction.saveCaseTransaction(kasaHareketi);
                    }
                }
            }
        }

        public static void searchBook(string Name)
        {
            Console.WriteLine("Lütfen aradığınız kitabın ID'sini giriniz : ");
            foreach (Book aramak in Books)
            {
                if (aramak.Name.Contains(Name))
                {
                    Console.WriteLine(aramak.ToString());
                }
                else
                {
                    Console.WriteLine("Böyle bir kitap bulunamadı.");
                }

            }
        }
        public override string ToString()
        {
            return String.Format("Id:{0} " + "Name:{1}, " + "Type:{2}, " + "Quantity:{3}, " + "Cost Price:{4}, " + "Price:{5} ", ID, Name, BookType, QTY, CostPrice, Price);
        }



        public static void addBookToFile(Book book)
        {
         FileStream fs = new FileStream(@"C:\Users\Huawei\Desktop\KitapListesi.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(book.ToString());
            sw.Close();

        }
        public static void readfile()
        {
            StreamReader sr = new StreamReader(@"C:\Users\Huawei\Desktop\KitapListesi.txt");
            Console.WriteLine(sr.ReadToEnd());
            
            Console.ReadKey();
            
        }
      
        

    }
}


    