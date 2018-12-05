// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;

// namespace Deicstra
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             int count = 7;                                       //Количество узлов
//             int start = 0;                                          //Стартовый узел
//             List<Node> nodes = new List<Node>();                 //Генерим N узлов

//             for(int i=0; i< count; i++)
//             {
//                 nodes.Add(new Node(i.ToString()));      //Создаём узел, именуя его (надо же как то их различать)
//             }

//             for (int i = 0; i < nodes.Count; i++)          //Для всех узлов
//             {
//                 for (int j = i + 1; j < nodes.Count; j++)  //По другим узлам (еще не пройденным)
//                 {
//                     nodes[i].Add(nodes[j]);                 //Добавляем узел
//                 }
//             }

//             for (int i = 0; i < nodes.Count; i++)              //Тупо вывод инфы о начальном состоянии узлов, проверялось правильно ли вообще всё присовено (Да, правильно)
//             {
//                 Console.WriteLine("Node '{0}'", nodes[i].name);
//                 for (int j = 0; j < nodes[i].neighboors.Count; j++)
//                 {
//                     Console.WriteLine("To node '{0}' = {1}", nodes[i].neighboors[j].name, nodes[i].distance[j]);
//                 }
//                 Console.WriteLine();
//             }

//             List<List<int>> ways = Deicstra(nodes[start], nodes);   //УКАЖИ ИЗ КАКОЙ ВЕРШИНЫ ИЩЕМ, Я БЫДЛО И УКАЗАЛ ТУПО ЦИФРОЙ

//             for (int i = 0; i < ways.Count; i++)            //Вывод найденных путей
//             {
//                 Console.WriteLine("Way from {0} to {1}:", ways[i].First(), ways[i].Last());
//                 for (int j = 0; j < ways[i].Count; j++)
//                 {
//                     Console.Write(ways[i][j]);
//                     if (j < ways[i].Count - 1)
//                     {
//                         Console.Write(" - ");
//                     }
//                 }
//                 Console.WriteLine();
//             }

//             Console.ReadKey();
//         }

//         static List<List<int>> Deicstra(Node start, List<Node> nodes)   //По факту можно обойтись вместо node start тупо индексу узла из списка, тогда не надо будет искать по индексу
//         {
//             double[] dist = new double[nodes.Count];  //Веса вершин - длина путей до них
//             bool[] passed = new bool[nodes.Count];      //Пройденные вершины
//             int current = nodes.IndexOf(start);           //Начальная (ДА ДА, Я ПРО ЭТО. Если передавать не узел, а его номер - можно будет тут не искать индекс, а сразу номер давать) (просто не знал как лучше сделать)

//             for (int i = 0; i < nodes.Count; i++)   //Задаём начальные значения
//             {
//                 dist[i] = -1;
//                 passed[i] = false;
//             }
//             dist[current] = 0;

//             for (int i = 0; i < nodes.Count; i++)                               //Цикл по всем вершинам 
//             {
//                 current = MinDist(dist, passed);

//                 for (int j = 0; j < nodes[current].neighboors.Count; j++)       //Для всех соседей текущей вершины
//                 {
//                     List<int> points = new List<int>();

//                     for (int k = 0; k < nodes[current].neighboors.Count; k++)   //Составляем список соседей, которых надо пройти
//                     {
//                         int neighboor = nodes.IndexOf(nodes[current].neighboors[k]);    //Индекс соседа в глобали
//                         if (nodes[current].distance[k] > 0 && !passed[neighboor])       //Если расстояние до k-того соседа есть и этот сосед не пройден
//                         {
//                             points.Add(neighboor);                                      //Добавляем его
//                         }
//                     }

//                     points = Sort(current, points, nodes);

//                     for (int k = 0; k < points.Count; k++)
//                     {
//                         double d = nodes[current].DistanceTo(nodes[points[k]]);                         //Расстояние из текущей вершины до новой из списка 
//                         if (dist[points[k]] < 0 || d + dist[current] < dist[points[k]])   //Если расстояние до вершины была бесконечна, или расстояние между вершинами + вес текущей вершины меньше веса куда смотрим
//                         {
//                             dist[points[k]] = d + dist[current];    //Значит новый путь короче (поверь, оно работает...)
//                         }
//                     }
//                 }

//                 passed[current] = true; //Помечаем текущую вершину как пройденную
//             }

//             //На данный момент мы в dist имеем из точки start все кратчайшие пути
//             //Теперь по алгоритму обратного будем искать путь как некий массив

//             List<List<int>> result = new List<List<int>>();
//             for (int i = 0; i < nodes.Count; i++)
//             {
//                 double weight;

//                 result.Add(new List<int>());
//                 result[i].Add(i);
//                 weight = dist[i];

//                 while (weight>0)
//                 {
//                     result[i].Add(FindNext(result[i], dist, nodes));

//                     weight -= nodes[result[i].Last()].DistanceTo(nodes[result[i][result[i].Count - 2]]);
//                 }
//                 result[i].Reverse();

//             }

//             return result;
//         }
        
//         static int MinDist(double[] dist, bool[] passed)    //Минимальная дистанция вершины из начала
//         {
//             int result = -1;
//             for (int i = 0; i < dist.Length; i++)
//             {
//                 if (!(dist[i] < 0) && !passed[i] && (result < 0 ||  dist[i] < dist[result])) //Если вершину еще не проходили и "Вес" вершины не "бесконечность" и "Вес" вершины текущей меньше прошлого
//                 {
//                     result = i;
//                 }
//             }

//             return result;
//         }

//         static List<int> Sort(int current, List<int> points, List<Node> nodes)  //Сортируем по весам
//         {
//             List<int> sorted = new List<int>();

//             for (int i = 0; i < points.Count; i++)
//             {
//                 int numb = -1;
//                 for (int j = 0; j < points.Count; j++)
//                 {
//                     //Если вершина на которую смотрим еще не сортирована, дистанция до текущей вершины меньше прошлой дистанции, запоминаем новую (писец условие но работает)
//                     if (!sorted.Contains(points[j]) && (numb < 0 || nodes[current].DistanceTo(nodes[points[j]]) < nodes[current].DistanceTo(nodes[points[numb]])))
//                     {
//                         numb = j;
//                     }
//                 }
//                 sorted.Add(points[numb]);   //Добавляем
//             }

//             return sorted;
//         }

//         static int FindNext(List<int> points, double[] dist, List<Node> nodes)
//         {
//             for(int i=0; i<dist.Length; i++)
//             {
//                 double d = nodes[i].DistanceTo(nodes[points.Last()]);
//                 if (!points.Contains(i) && d > 0)
//                 {
//                     if (dist[i] + d == dist[points.Last()])
//                         return i;
//                 }
//             }

//             return -1;
//         }
//     }
// }
