using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DoubleBinary
{
    class Program
    {
        public delegate double function(double x);
        public static double F1(double x)
        {
            return x * x - 50 * x + 10;
        }

        public static double F2(double x)
        {
            return x * x - 10 * x + 50;
        }
        public static void SaveFunc(string fileName, double a, double b, double h, function F)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(F(x));
                x += h;// x=x+h;
            }
            bw.Close();
            fs.Close();
        }
        public static double Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }
        static void Main(string[] args)
        {
            function[] F = { F1, F2 };
            Console.WriteLine("Сделайте выбор: 1 - функция F1, 2 - функция F2");
            int index = int.Parse(Console.ReadLine());
            SaveFunc("data.bin", -100, 100, 0.5, F[index - 1]);
            Console.WriteLine(Load("data.bin"));
            Console.ReadKey();
        }
    }
}

//Пропустил много вебинаров, приходится наверстывать упущенное с помощью гугла. 
// В перерывах между первым и вторым интенсивом буду персматривать вебинары и делать ДЗ сугубо для себя. 
//Остальные ДЗ хоть и смог нагуглить, но не стал делать, чтобы не копипастить и я особо не понимаю методы их решения.
