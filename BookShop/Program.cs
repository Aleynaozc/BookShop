using System;

namespace BookShop
{
    class Program
    {
        /// <summary>
        /// Kitap Mağazası Uygulaması
        ///
        /// Kitap, Kasa
        ///
        /// 1 - kitap kayıt edebilmeyi
        ///     -- kayıt esnasında kitap adi, adedi, maliyet fiyati, vergisi, kazanç miktari vs.
        ///     -- ürün fiyati maliyet fiyati, vergi ve kazanç miktarina bağlı olarak hesaplanır
        /// 2 - kitap silebilme
        ///     -- kita silme fonksiyonu seçilirse girilen adet kadar kitap silinecektir
        /// 3 - kitap güncelleme
        /// 4 - kitap satış
        ///     -- satılan kitap fiyatı kasaya gelir olarak giriş yapılır
        ///     -- satılan kitap kitap listemden eksiltilir
        /// 5 - kitap listesi
        /// 6 - kitap listesinden arama kabiliyeti
        ///
        ///  Kitap -> id, adi, tür'ü (enum kullanacağız), maliyet fiyati, toplam vergi, stok adedi, kayit tarihi, güncelleme tarihi
        ///  Kasa işlemi -> id, tür (gelir , gider (enum kullanacağım)), tutar, kayit tarihi
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Book.readfile();
            
            int secim = 0;
            while (secim != 8)
            {

                Console.WriteLine("1 - Kitap Ekle");
                Console.WriteLine("2 - Kitap Silme");
                Console.WriteLine("3 - Kitap Güncelleme");
                Console.WriteLine("4 - Kitap Satış");
                Console.WriteLine("5 - Kitap Listeleme");
                Console.WriteLine("6 - Kitap Ara");
                Console.WriteLine("7 - Kasa Hareketleri");
                Console.WriteLine("8 - Çıkış");

                secim = Convert.ToInt32(Console.ReadLine());
                switch (secim)
                {
                    case 1:
                        kitapEkleCase();

                        break;
                    case 2:
                        kitapSilme();
                        break;
                    case 3:
                        ktpGuncel();
                        break;
                    case 4:
                        kitapSatis();
                        break;
                    case 5:
                        kitaplariListele();
                        break;
                    case 6:
                        kitapArama();
                        break;
                    case 7:
                        CaseTransaction.kasaHareketleriniListele();
                        break;

                    default:
                        break;

                }

            }

            //int sayac = 0;
            //Random rastgele = new Random();
            //while (sayac < 50)
            //{
            //    Book book = new Book("Kitap " + (sayac + 1), rastgele.Next(35, 51), (BookTypeEnums)rastgele.Next(0, 5), rastgele.Next(1, 21), rastgele.Next(10, 31),
            //        rastgele.Next(1, 16));

            //    Book.addBook(book);
            //    sayac++;
            //}





            Console.WriteLine("Hesap Hareketleri");
            Console.WriteLine("--------------------------");
            foreach (CaseTransaction caseTransaction in CaseTransaction.CaseTransactions)

            {
                Console.WriteLine(caseTransaction.ToString());
            }

        }
        public static void kitapEkleCase()
        {
            //kitap ekleme
            //adı,maliyet fiyatı,türü,tax,kazanç miktarı,adet

            // kitap adı
            Console.Write("Kitap Adı:");
            string bookName = Console.ReadLine();

            //kitap maliyeti
            Console.Write("Maliyet:");
            double costPrice = Convert.ToDouble(Console.ReadLine());

            //kitap türü
            Console.Write("Kitap Türü (0-4):");
            BookTypeEnums bookType = (BookTypeEnums)Convert.ToInt32(Console.ReadLine());

            //vergi
            Console.Write("Vergi Oranı :");
            int TaxPercantage = Convert.ToInt32(Console.ReadLine());

            //kazanç miktarı
            Console.Write("Kazanç Miktarı:");
            int profitMargin = Convert.ToInt32(Console.ReadLine());
            //adet
            Console.Write("Adet:");
            int qty = Convert.ToInt32(Console.ReadLine());

            Book newBook = new Book(bookName, costPrice, bookType, TaxPercantage, profitMargin, qty);
            Book.addBook(newBook);
        }
        public static void kitaplariListele()

        {
            Console.WriteLine("Kitap Listeleri");
            Console.WriteLine("--------------------------");
            foreach (Book item in Book.Books)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public static void kitapSilme()
        {
            kitaplariListele();
            Console.Write("Silmek istediğiniz kitap ID: ");
            int bookID = Convert.ToInt32(Console.ReadLine());
            Book.removeBook(bookID);
        }
        public static void  kitapSatis()
        {
            kitaplariListele();
            Console.WriteLine("Kitap ID'sini giriniz: ");
            int satilankitapId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Kitap Miktarını giriniz: ");
            int kitapAdeti = Convert.ToInt32(Console.ReadLine());
            Book.sellBook(satilankitapId, kitapAdeti);
            kitaplariListele();
        }
        public static void kitapArama()
        {
            Console.WriteLine("Lütfen Kitabın İsmini Giriniz");
            string Name = Console.ReadLine();
            Book.searchBook(Name);
        }
        public static void ktpGuncel()
        {
            kitaplariListele();
            Console.WriteLine("Lütfen Değiştirmek istediğiniz Kitap ID giriniz :");
            int ktpGuncel =Convert.ToInt32(Console.ReadLine());
            Book.updateBook(ktpGuncel);
           
        }

       
        }
    }





