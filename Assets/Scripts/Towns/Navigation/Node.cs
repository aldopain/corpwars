using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deicstra
{
    class Node
    {
        static Random rnd = new Random(System.DateTime.Now.Millisecond);    //Тупо для рандомного заполнения, мне лень придумывать граф
        const int MaxInt = 100;

        public List<Node> neighboors;   //Список соседей
        public List<double> distance;   //Список расстояний
        public string name;             //Чтобы как то их различать

        public Node(string name)
        {
            neighboors = new List<Node>();
            distance = new List<double>();
            this.name = name;               
        }

        public void Add(Node node)                      //Добавляем соседа
        {
            if (!neighboors.Contains(node))             //Если он еще не добавлен
            {
                neighboors.Add(node);                   //Добавляем
                if (!node.neighboors.Contains(this))    //Если ЭТОГО узла нет у соседа
                {
                    distance.Add(rnd.Next(MaxInt) + 1);     //Генерим рандомную дистанцию (ПОСТАВИЛ +1 ЧТО БЫ ВСЕ ПУТИ БЫЛИ НЕ 0, для тестов... По факту работает и без этого если нет вот прям отрезанных точек)
                    node.Add(this);                     //Говорим соседу, что бы добавил нас (сокращает число циклов, см основной код)
                }
                else
                {
                    int numb = node.neighboors.IndexOf(this);   //Если у соседа МЫ уже есть, находим наш номер
                    distance.Add(node.distance[numb]);          //И копируем расстояние до нас
                }
            }
        }

        public double DistanceTo(Node node) //Дистанция до узла (мне без этой функции никак, вот писец не придумал как без неё)
        {
            int numb = neighboors.IndexOf(node);    //Индекс узла среди соседей

            if (numb >= 0)
                return distance[numb];  //Если такой сосед есть - возвращаем длину
            else
                return -1;              //Иначе = -1
        }
    }
}
