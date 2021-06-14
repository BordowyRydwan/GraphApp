using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace GraphApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy TextModeView.xaml
    /// </summary>
    static class ExtensionMethods
    {
        public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
                elements.SelectMany((e, i) =>
                elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }
    }
    public partial class TextModeView : UserControl
    {
        public TextModeView()
        {
            InitializeComponent();
        }

        private static bool IsConnected(Dictionary<string, List<string>> graph, string node)
        {
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            foreach (string k in graph.Keys)
            {
                visited[k] = false;
            }

            DFS(visited, graph, node);

            foreach (bool v in visited.Values)
            {
                if (!v) return false;
            }
            return true;
        }

        private static void DFS(Dictionary<string, bool> visited, Dictionary<string, List<string>> graph, string node)
        {
            if (!visited[node])
            {
                visited[node] = true;
                foreach (string neighbour in graph[node]) DFS(visited, graph, neighbour);
            }
        }

        private static List<string> DictToList(Dictionary<string, List<string>> graph)
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, List<string>> kv in graph)
            {
                foreach (string element in kv.Value)
                {
                    if (!result.Contains(element + kv.Key)) result.Add(kv.Key + element);
                }
            }

            return result;
        }

        private static Dictionary<string, List<string>> ListToDict(List<string> graph)
        {
            List<string> vertices = (from edge in graph from vertex in edge select vertex.ToString()).ToList();
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            foreach (string vertex in vertices) result[vertex] = new List<string>();
            foreach (string edge in graph)
            {
                result[edge[0].ToString()].Add(edge[1].ToString());
                result[edge[1].ToString()].Add(edge[0].ToString());
            }
            return result;
        }

        private static bool ContainsAllItems(List<string> a, List<string> b)
        {
            return !b.Except(a).Any();
        }

        public static Dictionary<int, List<List<string>>> FindCuts(Dictionary<string, List<string>> graph)
        {
            List<string> graphList = DictToList(graph);

            Dictionary<int, List<List<string>>> result = new Dictionary<int, List<List<string>>>();
            for (int i = 0; i < graphList.Count; i++)
            {
                result[i] = new List<List<string>>();
            }

            for (int i = 1; i < graphList.Count; i++)
            {
                var combs = graphList.AsEnumerable().DifferentCombinations(i).ToList();
                var combscopy = new List<IEnumerable<string>>();
                foreach (IEnumerable<string> c in combs) combscopy.Add(c);

                for (int j = 0; j < i; j++)
                {
                    foreach (List<string> used in result[j])
                    {
                        foreach (var edges in combscopy)
                        {
                            var edges_list = edges.ToList();
                            if (ContainsAllItems(edges_list, used)) combs.Remove(edges);
                        }
                        combscopy = new List<IEnumerable<string>>();
                        foreach (IEnumerable<string> c in combs) combscopy.Add(c);
                    }
                }

                foreach (var edges in combs)
                {
                    List<string> currentGraphList = DictToList(graph);
                    foreach (var edge in edges) currentGraphList.Remove(edge);
                    Dictionary<string, List<string>> currentGraphDict = ListToDict(currentGraphList);
                    if (!IsConnected(currentGraphDict, currentGraphDict.Keys.First()) || currentGraphDict.Keys.Count != graph.Keys.Count) result[i].Add(edges.ToList());
                }
            }

            return result;
        }
        public int[,] convertStringToArray(string input)
        {
            var rowArray = input.Split('\n');

            int[,] array = new int[rowArray.Length - 1, rowArray[0].Split(' ').Length];

            for (int i = 0; i < rowArray.Length - 1; i++)
            {
                var tmp = rowArray[i].Replace(" ", "");
                for (int j = 0; j < rowArray[0].Split(' ').Length; j++)
                {
                    if (tmp[j] == '0') array[i, j] = 0;
                    else if (tmp[j] == '1') array[i, j] = 1;
                }
            }

            return array;
        }
        static Dictionary<string, List<string>> convertToList(int[,] a)
        {
            int l = a.GetLength(0);
            Dictionary<string, List<string>> adjListArray = new Dictionary<string, List<string>>();
            char start = 'A';
            

            for (int i = 0; i < l; i++)
            {
                List<string> tmp = new List<string>();
                for (int j = 0; j < l; j++)
                { 
                    if (a[i, j] == 1)
                    {
                        tmp.Add(Convert.ToString(Convert.ToChar('A' + j)));
                    }
                }
                adjListArray.Add(Convert.ToString(Convert.ToChar(start+i)), tmp);
            }

            return adjListArray;
        }


        private void RunAlgorithmButton_Click(object sender, RoutedEventArgs e)
        {
            outputBox.Document.Blocks.Clear();
            string input = new TextRange(inputBox.Document.ContentStart, inputBox.Document.ContentEnd).Text;

            var convertedInput = convertStringToArray(input);

            var test = convertToList(convertedInput);

            Dictionary<int, List<List<string>>> result = FindCuts(test);
            foreach (KeyValuePair<int, List<List<string>>> kv in result)
            {
                foreach (List<string> l in kv.Value)
                {
                    foreach (string s in l) outputBox.AppendText(s + " ");
                   outputBox.AppendText("\n");
                }
                outputBox.AppendText("\n");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            outputBox.Document.Blocks.Clear();
        }
    }
}

