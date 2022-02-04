using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop
{
    class CaseTransaction : BaseClass
    {
        public static List<CaseTransaction> CaseTransactions = new List<CaseTransaction>();
        public double Amount { get; set; } //tutar
        public TransactionTypeEnums TransactionType { get; set; }

        //caseTransaction sınıfının consturctor yapıcı metod

        public CaseTransaction(double _amount, TransactionTypeEnums _transactionType)
        {
            Amount = _amount;
            TransactionType = _transactionType;
        }
        //kasa işlemi kaydetme metodu
        public static void saveCaseTransaction(CaseTransaction caseTransaction)
        {
            CaseTransactions.Add(caseTransaction);
        }
        public static double calculateAmount(double Price, int qty)
        {
            return Price * qty;
        }
        public static void kasaHareketleriniListele()
        {
            Console.WriteLine("KASA HAREKETLERİ");
            double kasaToplam = 0;

            foreach (CaseTransaction kasaHareketi in CaseTransactions)
            {
                Console.WriteLine("-------------------------");
                if (kasaHareketi.TransactionType == TransactionTypeEnums.EXPENSE)
                {
                    kasaToplam -= kasaHareketi.Amount;
                }
                else
                {
                    kasaToplam += kasaHareketi.Amount;
                }
                Console.WriteLine(kasaHareketi.ToString());
            }
            Console.WriteLine("Kasa Toplam Tutarı : " + kasaToplam);
        }
        public override string ToString()
        {
            return string.Format(" Id:{0} - Type: {1} - " + "Amount:{2} - Created Time: {3}", ID, TransactionType, Amount, CreatedTime);
        }
    }
}
