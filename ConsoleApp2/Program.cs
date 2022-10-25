using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp2
{
    class Program
    {
        //Эту функцию я тупа с инета взял, пока даже не адаптировал. Не оч понимаю, как она работает, но вроде выглядит прикольно



        static string translatePascal(string name)
        {
            string res = "";

            //Функции, которые должны быть описаны в шаблоне :
            // changeElement(value) - изменение значения элемента в текущей ячейке на value
            // memoryIndex - индекс текущей ячейки памяти
            // outputElement - вывод символа, соответствующего текущему значению ячейки памяти
            // inputElement - запись в ячейку кода введённого символа

            //Заменыв формате
            //<pascal> <C#> <cycle>
            //где :
            // cycle == 0 если конструкция не задаёт цикла
            // cycle == 1 если конструкция открывает цикл
            // cycle == 2 если конструкция закрывает цикл
            object[] replaces ={new string[]{"+","changeElement(+1);","0"},
                                new string[]{"-","changeElement(-1);","0"},
                                new string[]{">","memoryIndex++;","0"},
                                new string[]{"<","memoryIndex--;","0"},
                                new string[]{".","outputElement();","0"},
                                new string[]{",","inputElement();","0"},
                                new string[]{"[","while(getElement()!=0){","1"},
                                new string[]{"]","}","2"}};

            //Количество отступов
            int tabcount = 3;
            foreach (char chr in name)
            {
                int i = 0;
                string[] obj;

                //Поиск соответствующей символу замены
                int replaceNum = -1;
                while (replaceNum == -1)
                {
                    obj = (string[])replaces[i];
                    if (obj[0] == chr.ToString())
                    {
                        replaceNum = i;
                    }
                    i++;
                    if (i > replaces.Length)
                    {
                        Console.WriteLine("Fatal error: illegal character '" + chr.ToString() + "'");
                        Process.GetCurrentProcess().Kill();
                    }
                }

                obj = (string[])replaces[replaceNum];
                string c_sharp = obj[1];
                string cycle = obj[2];
                string tabs = "";
                int tabchange = 0;

                //Изменение отступа
                if (cycle == "1")
                {
                    //Для начала цикла отступ поменяем уже после дописывания в код цикла
                    tabchange = 1;
                }
                else
                if (cycle == "2")
                {
                    tabcount--;
                }

                //Формируем табуляции
                for (i = 1; i <= tabcount; i++)
                { tabs += "t"; }

                tabcount += tabchange;

                res += tabs + c_sharp + "\n";
            }

            return res;
        }

        static void Main()
        {
            string line;
            try
            {
                //Запрашиваем имя изначального файла
                Console.WriteLine("Введите полный путь исходного файла на языке Pascal");
                string name = Console.ReadLine();
                //Считываем первую строчку
                StreamReader sr = new StreamReader(name);
                line = sr.ReadLine();
                //Продолжаем считывать до конца файла
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    if (line == "var")
                    {
                        Console.ReadLine();
                    }
                    Console.WriteLine(' ');
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}