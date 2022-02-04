using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop
{
    public abstract class BaseClass
    {
        public int ID { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        Random random = new Random();
        const int MIN_NUMS = 10000000;
        const int MAX_NUMS = 999999999;
       
        //yapcı metot
        public BaseClass()
        {
            ID = random.Next(MIN_NUMS, MAX_NUMS);
            CreatedTime = DateTime.Now;
            UpdatedTime = DateTime.Now;
        }
    }
}
