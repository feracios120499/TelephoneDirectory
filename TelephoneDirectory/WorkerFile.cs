using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory
{
    public class WorkerFile
    {
        public string Path { get; }
        public WorkerFile(string pathFile)
        {
            if (File.Exists(Path))
            {
                throw new Exception("Файл не знайдено!");//якщо файл не знайдено то помилка
            }
            Path = pathFile;
        }

        public List<TelephonDirectory> GetPhonesDirectory()//Отримання усіх телефонів
        {
            List<TelephonDirectory> list = new List<TelephonDirectory>();

            List<string> fileStr = new List<string>();
            fileStr=File.ReadAllLines(Path).ToList();//читаємо усі рядки з файлу
            foreach(var item in fileStr)//цикл по строчкам
            {
                if (item.Count() == 0) continue;
                string[] fields=item.Split(';');//розбиваемо по ; тому що рядок виглядає як ID;City;Address;FullName;Phone

                list.Add(new TelephonDirectory
                {
                    ID = Convert.ToInt32(fields[0]),
                    City=fields[1],
                    Address=fields[2],
                    FullName=fields[3],
                    Phone=fields[4]
                });//добовляємо у список
            }
            return list;//повертаємо список телефонів
        }
        public void SaveChanges(List<TelephonDirectory> list)
        {
            File.WriteAllText(Path, string.Empty);//очищаємо старі данні
            List <string>content = new List<string>();
            foreach(var item in list)
            {
                content.Add(item.ToString());
            }
            File.WriteAllLines(Path, content);
        }
    }
}
