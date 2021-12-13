using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    public class AdventOfCodePuzzle12 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var nodeDictionary = new Dictionary<string, Node>();
            var visited = new Queue<List<Node>>();

            foreach (var line in input)
            {
                var connection = line.Split('-');
                TryAddAndConnectNode(connection[0], connection[1]);
            }
            
            var startNode = nodeDictionary["start"];
            var endNode = nodeDictionary["end"];

            var singleSmallCaveCount = 0;
            visited.Enqueue(new List<Node>{startNode});
            do
            {
                var stepsList = visited.Dequeue();
                var currentNode = stepsList[^1];
                if (currentNode != endNode)
                {
                    foreach (var node in nodeDictionary.Values)
                    {
                        node.Reset();
                        if (stepsList.Contains(node) && node.CanVisitOnce)
                            node.VisitCounter = 1;
                    }

                    currentNode.VisitCounter++;
                    foreach (var subNode in currentNode.Connections.Where(x => x.Value.CanVisit))
                    {
                        var tmpList = stepsList.ToList();
                        tmpList.Add(subNode.Value);
                        visited.Enqueue(tmpList);
                    }
                }
                else
                {
                    ++singleSmallCaveCount;
                }
            } while (visited.Any());

            return singleSmallCaveCount;
            
            void TryAddAndConnectNode(string nodeName, string childNodeName)
            {
                if (!nodeDictionary.ContainsKey(nodeName))
                {
                    var newNode = new Node(nodeName);
                    nodeDictionary.Add(newNode.NodeName, newNode);
                }
                if (!nodeDictionary.ContainsKey(childNodeName))
                {
                    var newNode = new Node(childNodeName);
                    nodeDictionary.Add(newNode.NodeName, newNode);
                }

                nodeDictionary[nodeName].Connections.Add(childNodeName, nodeDictionary[childNodeName]);
                nodeDictionary[childNodeName].Connections.Add(nodeName, nodeDictionary[nodeName]);
            }
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var nodeDictionary = new Dictionary<string, Node>();
            var visited = new Queue<(bool revisitSmallAllowed, List<Node> path)>();

            foreach (var line in input)
            {
                var connection = line.Split('-');
                TryAddAndConnectNode(connection[0], connection[1]);
            }
            
            var startNode = nodeDictionary["start"];
            var endNode = nodeDictionary["end"];

            var singleSmallCaveCount = 0;
            visited.Enqueue((true, new List<Node>{startNode}));
            do
            {
                var stepsList = visited.Dequeue();
                var currentNode = stepsList.path[^1];
                if (currentNode != endNode)
                {
                    foreach (var node in nodeDictionary.Values)
                    {
                        //Performance improvements to remember the revisitSmall-State. The following loop still hits hard - should state be shared accross all nodes? (rev/val)
                        node.Reset();
                        if (stepsList.path.Contains(node) && node.CanVisitOnce && stepsList.revisitSmallAllowed)
                            node.VisitCounter = 0;
                        else if (stepsList.path.Contains(node) && node.CanVisitOnce)
                            node.VisitCounter = 1;
                    }

                    currentNode.VisitCounter++;
                    foreach (var subNode in currentNode.Connections.Where(x => x.Value.CanVisit))
                    {
                        var tmpList = stepsList.path.ToList();
                        tmpList.Add(subNode.Value);

                        var result = stepsList.revisitSmallAllowed;
                        if (subNode.Value.CanVisitOnce && stepsList.path.Contains(subNode.Value) )
                            result = false;
                        
                        visited.Enqueue((result, tmpList));
                    }
                }
                else
                {
                    ++singleSmallCaveCount;
                }
            } while (visited.Any());

            return singleSmallCaveCount;
            
            void TryAddAndConnectNode(string nodeName, string childNodeName)
            {
                if (!nodeDictionary.ContainsKey(nodeName))
                {
                    var newNode = new Node(nodeName);
                    nodeDictionary.Add(newNode.NodeName, newNode);
                }
                if (!nodeDictionary.ContainsKey(childNodeName))
                {
                    var newNode = new Node(childNodeName);
                    nodeDictionary.Add(newNode.NodeName, newNode);
                }

                nodeDictionary[nodeName].Connections.Add(childNodeName, nodeDictionary[childNodeName]);
                nodeDictionary[childNodeName].Connections.Add(nodeName, nodeDictionary[nodeName]);
            }
        }
    }

    public class Node
    {
        private bool? canVisitOnlyOnce;
        private bool preventVisit;

        public Node(string nodeName)
        {
            Connections = new Dictionary<string, Node>();
            NodeName = nodeName;
            preventVisit = nodeName == "start";
        }

        public string NodeName { get; }

        public Dictionary<string, Node> Connections { get;}

        public int VisitCounter { get; set; }

        public bool CanVisit => !CanVisitOnce || !preventVisit && VisitCounter < 1;

        public bool CanVisitOnce => canVisitOnlyOnce ??= NodeName.Any(char.IsLower);

        public void Reset() => VisitCounter = 0;
    }
}