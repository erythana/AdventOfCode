using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    public class AdventOfCodePuzzle12 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var distinctWays = 0;
            var nodeDictionary = new Dictionary<string, Node>();

            foreach (var line in input)
            {
                var connection = line.Split('-');

                TryAddAndConnectNode(connection[0], connection[1]);
            }
            
            var startNode = nodeDictionary["start"];
            var endNode = nodeDictionary["end"];
            
            
           
            Node Travel(Node nextNode)
            {
                nextNode.VisitCounter++;
                foreach (var node in nextNode.Connections.Values.Where(x => x.VisitAllowed))
                {
                    return Travel(node);
                }
                return nextNode;
            }


            void TryAddAndConnectNode(string nodeName, string childNodeName)
            {
                if (!nodeDictionary.TryGetValue(nodeName, out var node)) //from node is NEW
                {
                    var newNode = new Node(nodeName);
                    nodeDictionary.Add(newNode.NodeName, newNode);
                }

                if (!nodeDictionary.TryGetValue(childNodeName, out node)) //from node is NEW
                {
                    var newNode = new Node(childNodeName);
                    nodeDictionary.Add(newNode.NodeName, newNode);
                }

                nodeDictionary[nodeName].Connections.Add(childNodeName, nodeDictionary[childNodeName]);
                nodeDictionary[childNodeName].Connections.Add(nodeName, nodeDictionary[nodeName]);
            }


            return null;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            throw new NotImplementedException();
        }
    }

    public class Node
    {
        private bool? canVisitOnlyOnce;

        public Node(string nodeName)
        {
            Connections = new Dictionary<string, Node>();
            NodeName = nodeName;
        }

        public string NodeName { get; private set; }

        public Dictionary<string, Node> Connections { get; set; }

        public int VisitCounter { get; set; }

        public bool VisitAllowed
        {
            get => !CanVisitOnlyOnce || VisitCounter < 1;
        }

        public bool CanVisitOnlyOnce
        {
            get => canVisitOnlyOnce ??= NodeName.Any(char.IsLower);
        }

        public void Reset()
        {
            VisitCounter = 0;
        }
    }
}