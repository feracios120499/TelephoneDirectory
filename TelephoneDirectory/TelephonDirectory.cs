using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory
{
    public class TelephonDirectory
    {
        public int ID { get; set; }//унікальний ключ
        public string City { get; set; }//Місто
        public string Address { get; set; }//Адреса
        public string FullName { get; set; }//ПІБ
        public string Phone { get; set; }//Телефон

        public override string ToString()
        {
            return $"{ID};{City};{Address};{FullName};{Phone}";
        }
    }
}
