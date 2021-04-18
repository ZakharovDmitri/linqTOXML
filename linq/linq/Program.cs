using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace linqxl4
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Menu();
        }
        public static void task1()
        {
            // task 1 (#4)
            Console.WriteLine("Задание 1.  Создать XML-документ с корневым элементом root, элементами первого уровня line и элементами второго уровня word.\n");

            var text = File.ReadAllLines(@"C:\Users\Acer\OneDrive\OneDrive - НИУ Высшая школа экономики\2 КУРС\кпо\linqxl4\linqxl4\bin\Debug\linqtoxml4.txt", Encoding.Default);

            XDocument doc1 = new XDocument(
              new XDeclaration(null, "windows-1251", null),
              new XElement("root",
                text.Select(line => new XElement("line",
                line.Split(' ').Select(word => new XElement("word", word))))));

            doc1.Save("task1.xml");


            Console.WriteLine("Текст текстового файла:");
            foreach (string s in text)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\nТекст xml-документа:");
            Console.WriteLine(doc1);
            Console.WriteLine("\nЗадание 1 выполнено. Исходный текстовый файл - linqtoxml4.txt. Создан файл task1.xml.\n");
            Menu();
        }
        public static void task2()
        {
            // task 2 (#14)
            Console.WriteLine("Задание 2. Найти элементы второго уровня, имеющие дочерний текстовый узел, вывести имя элемента, значение узла, количество таких элементов.\n");

            XDocument doc2 = XDocument.Load(@"C:\Users\Acer\OneDrive\OneDrive - НИУ Высшая школа экономики\2 КУРС\кпо\linqxl4\linqxl4\bin\Debug\task2.xml");
            Console.WriteLine("Исходный файл:");
            Console.WriteLine(doc2);
            Console.WriteLine();

            XElement res = new XElement("name", doc2.Root.Elements().Select(x => x.Elements().Where(n => n.Value != "")));

            res.Elements().ToList().ForEach(e => Console.WriteLine($"Имя: " + e.Name + ", значение: " + e.Value));

            Console.WriteLine("Количество:" + res.Elements().Count().ToString());
            Console.WriteLine("Задание 2 выполнено.\n");
            Menu();
        }
        public static void task3()
        {
            // task 3 (#24)
            Console.WriteLine("Задание 3. Удалить из документа все комментарии, являющиеся узлами первого или второго уровня.\n");

            XDocument doc3 = XDocument.Load(@"C:\Users\Acer\OneDrive\OneDrive - НИУ Высшая школа экономики\2 КУРС\кпо\linqxl4\linqxl4\bin\Debug\task3.xml");
            Console.WriteLine("Исходный файл:");
            Console.WriteLine(doc3);
            XElement root3 = doc3.Root;

            
            root3.Nodes().OfType<XComment>().Remove();
            root3.Elements().Nodes().OfType<XComment>().Remove();
            doc3.Save("task3new.xml");

            Console.WriteLine("\nНовый файл:");
            Console.WriteLine(doc3);
            Console.WriteLine("\nЗадание 3 выполнено. Исходный файл - task3.xml, новый файл - task3new.xml\n");
            Menu();
        }
        public static void task4()
        {
            
            Console.WriteLine("Задание 4. Для каждого элемента первого уровня, имеющего атрибуты, добавить в конец его дочерних узлов элементы с именами, совпадающими с именами" +
                "его атрибутов, и текстовыми значениями, совпадающими со значениями соответствующих атрибутов, после чего удалить все атрибуты обрабатываемого элемента первого уровня.\n");

            XDocument doc4 = XDocument.Load(@"C:\Users\Acer\OneDrive\OneDrive - НИУ Высшая школа экономики\2 КУРС\кпо\linqxl4\linqxl4\bin\Debug\task4.xml");
            Console.WriteLine("Исходный файл:");
            Console.WriteLine(doc4);
            XElement root4 = doc4.Root;
            root4.Elements().ToList().ForEach(e => e.ReplaceAttributes(e.Attributes().Select(n => new XElement(n.Name, n.Value))));   
            doc4.Save("task4new.xml");
            Console.WriteLine("\nНовый файл:");
            Console.WriteLine(doc4);
            Console.WriteLine("\nЗадание 4 выполнено. Исходный файл - task4.xml, новый файл - task4new.xml\n");
            Menu();
        }
        public static void task5()
        {
            
            Console.WriteLine("Задание 5. Для каждого элемента перебирая всех его потомков, содержащих атрибут Name, найти минимальное значение " +
                "данного атрибута и записать это значение в новый атрибут min обрабатываемого элемента.\n");

            XDocument doc5 = XDocument.Load(@"C:\Users\Acer\OneDrive\OneDrive - НИУ Высшая школа экономики\2 КУРС\кпо\linqxl4\linqxl4\bin\Debug\task5.xml");
            Console.WriteLine("Исходный файл:");
            Console.WriteLine(doc5);
            XElement root5 = doc5.Root;
            string S = "Name";

            var result = new XElement("min", root5.Elements()
                        .Select(e => (double?)e.Attribute(S)).Min());
            root5.SetAttributeValue("min", result.Value);

            foreach (XElement element in root5.Elements())
            {
                var newres = new XElement("min", element.Elements()
                        .Select(e => (double?)e.Attribute(S)).Min());
                element.SetAttributeValue("min", newres.Value);
            }
            doc5.Save("task5new.xml");
            Console.WriteLine("\nНовый файл:");
            Console.WriteLine(doc5);
            Console.WriteLine("\nЗадание 5 выполнено. Исходный файл - task5.xml, новый файл - task5new.xml\n");
            Menu();
        }
        public static void task6()
        {
            // task 6 (#54)
            Console.WriteLine("Задание 6.  Связать с пространством имен корневого элемента все элементы первого и второго уровня.\n");

            XDocument doc6 = XDocument.Load(@"C:\Users\Acer\OneDrive\OneDrive - НИУ Высшая школа экономики\2 КУРС\кпо\linqxl4\linqxl4\bin\Debug\task6.xml");
            Console.WriteLine("Исходный файл:");
            Console.WriteLine(doc6);
            XElement root6 = doc6.Root;
            XNamespace namesp = root6.Name.Namespace;
            foreach (XElement e in root6.Elements())
            {
                // связь элементов 1 уровня
                e.Name = (XNamespace)"http://www.w3.org/1999/xlink" + e.Name.LocalName;
                foreach (XElement a in e.Elements())
                {
                    // связь элементов 2 уровня
                    a.Name = (XNamespace)"http://www.w3.org/1999/xlink" + a.Name.LocalName;
                }
            }
            doc6.Save("task6new.xml");
            Console.WriteLine("\nНовый файл:");
            Console.WriteLine(doc6);
            Console.WriteLine("\nЗадание 6 выполнено. Исходный файл - task6.xml, новый файл - task6new.xml\n");
            Menu();
        }
        
        public static void Menu()
        {
            Console.WriteLine("Введите номер задания:");
            Console.WriteLine("1. Задание 1, задача №4");
            Console.WriteLine("2. Задание 2, задача №14");
            Console.WriteLine("3. Задание 3, задача №24");
            Console.WriteLine("4. Задание 4, задача №34");
            Console.WriteLine("5. Задание 5, задача №44");
            Console.WriteLine("6. Задание 6, задача №54");
            Console.WriteLine("0. Выход");
            int select = int.Parse(Console.ReadLine());
            if (select != 0)
            {
                switch (select)
                {
                    case 1:
                        task1();
                        break;
                    case 2:
                        task2();
                        break;
                    case 3:
                        task3();
                        break;
                    case 4:
                        task4();
                        break;
                    case 5:
                        task5();
                        break;
                    case 6:
                        task6();
                        break;
                    case 0:
                        select = 0;
                        break;
                }
            }
            else
                return;
        }
    }
}
