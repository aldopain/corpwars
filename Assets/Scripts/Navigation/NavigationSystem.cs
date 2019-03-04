using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NavigationSystem : MonoBehaviour {

	List<NavigationNode> allPoints;
	List<NavigationWay> ways;
	public string tag;

	// Use this for initialization
	void Start () {
		allPoints = new List<NavigationNode>();
		var tmp = GameObject.FindGameObjectsWithTag(tag);
		foreach (var current in tmp)
			allPoints.Add(current.GetComponent<NavigationNode>());
		foreach(var _current in allPoints){
			_current.ways = Dijkstra(_current);
			Debug.Log(_current.name + " ways:");
			foreach (var way in _current.ways)
			{
				IO.Out(way.points);
			}
		}
	}

	List<NavigationWay> Dijkstra (NavigationNode start) {
		double[] dist = new double[allPoints.Count];  //Веса вершин - длина путей до них
		bool[] passed = new bool[allPoints.Count];      //Пройденные вершины
		int current = allPoints.IndexOf(start);           //Начальная (ДА ДА, Я ПРО ЭТО. Если передавать не узел, а его номер - можно будет тут не искать индекс, а сразу номер давать) (просто не знал как лучше сделать)

		for (int i = 0; i < allPoints.Count; i++)   //Задаём начальные значения
		{
			dist[i] = -1;
			passed[i] = false;
		}
		dist[current] = 0;

		for (int i = 0; i < allPoints.Count; i++)                               //Цикл по всем вершинам 
		{
			current = MinDist(dist, passed);
			if (current != -1) {
				for (int j = 0; j < allPoints[current].Neighbours.Count; j++)       //Для всех соседей текущей вершины
				{
					List<int> points = new List<int>();

					for (int k = 0; k < allPoints[current].Neighbours.Count; k++)   //Составляем список соседей, которых надо пройти
					{
						int neighboor = allPoints.IndexOf(allPoints[current].Neighbours[k]);    //Индекс соседа в глобали
						if (allPoints[current].Distance(allPoints[k]) > 0 && !passed[neighboor])       //Если расстояние до k-того соседа есть и этот сосед не пройден
						{
							points.Add(neighboor);                                      //Добавляем его
						}
					}

					points = Sort(current, points, allPoints);

					for (int k = 0; k < points.Count; k++)
					{
						double d = allPoints[current].Distance(allPoints[points[k]]);                         //Расстояние из текущей вершины до новой из списка 
						if (dist[points[k]] < 0 || d + dist[current] < dist[points[k]])   //Если расстояние до вершины была бесконечна, или расстояние между вершинами + вес текущей вершины меньше веса куда смотрим
						{
							dist[points[k]] = d + dist[current];    //Значит новый путь короче (поверь, оно работает...)
						}
					}
				}

				passed[current] = true;
			}
		}

		//На данный момент мы в dist имеем из точки start все кратчайшие пути
		//Теперь по алгоритму обратного будем искать путь как некий массив

		List<List<int>> result = new List<List<int>>();
		for (int i = 0; i < allPoints.Count; i++)
		{
			double weight;

			result.Add(new List<int>());
			result[i].Add(i);
			weight = dist[i];

			while (weight>0)
			{
				result[i].Add(FindNext(result[i], dist, allPoints));

				weight -= allPoints[result[i].Last()].Distance(allPoints[result[i][result[i].Count - 2]]);
			}
			result[i].Reverse();
		}

		return Convert (result);
	}
	
	List<NavigationWay> Convert (List<List<int>> p) {
		var tmp = new List<List<NavigationNode>>();
		var res = new List<NavigationWay>();
		foreach (var currentList in p) {
			var cp = new List<NavigationNode>();
			tmp.Add(cp);
			foreach (var currentIndex in currentList) {
				cp.Add(allPoints[currentIndex]);
			}
		}
		foreach (var w in tmp) {
			res.Add(new NavigationWay(w));
		}
		return res;
	}

	static int MinDist(double[] dist, bool[] passed)    //Минимальная дистанция вершины из начала
	{
		int result = -1;
		string aa = "";
		foreach (var a in dist)
			aa += a + " ";
		for (int i = 0; i < dist.Length; i++)
		{	
			if (!(dist[i] < 0) && !passed[i] && (result < 0 ||  dist[i] < dist[result])) //Если вершину еще не проходили и "Вес" вершины не "бесконечность" и "Вес" вершины текущей меньше прошлого
				result = i;
		}

		return result;
	}

	static List<int> Sort(int current, List<int> points, List<NavigationNode> nodes)  //Сортируем по весам
	{
		List<int> sorted = new List<int>();

		for (int i = 0; i < points.Count; i++)
		{
			int numb = -1;
			for (int j = 0; j < points.Count; j++)
			{
				//Если вершина на которую смотрим еще не сортирована, дистанция до текущей вершины меньше прошлой дистанции, запоминаем новую (писец условие но работает)
				if (!sorted.Contains(points[j]) && (numb < 0 || nodes[current].Distance(nodes[points[j]]) < nodes[current].Distance(nodes[points[numb]])))
				{
					numb = j;
				}
			}
			sorted.Add(points[numb]);   //Добавляем
		}

		return sorted;
	}

	static int FindNext(List<int> points, double[] dist, List<NavigationNode> nodes)
	{
		for(int i=0; i<dist.Length; i++)
		{
			double d = nodes[i].Distance(nodes[points.Last()]);
			if (!points.Contains(i) && d > 0)
			{
				if (dist[i] + d == dist[points.Last()])
					return i;
			}
		}

		return -1;
	}
}
