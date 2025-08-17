using Microsoft.EntityFrameworkCore;
using SoftverskaRealizacijaBackend.Infrastructure;
using SoftverskaRealizacijaBackend.Models;
using static SoftverskaRealizacijaBackend.Models.Enumerations;

namespace SoftverskaRealizacijaBackend.Helpers
{
    public class MapHelper
    {
        public static Node FindNearestNode(double lat, double lon, IEnumerable<Node> allNodes)
        {
            Node nearest = null;
            double minDist = double.MaxValue;

            foreach (var node in allNodes)
            {
                var dist = GeoUtils.Haversine(lat, lon, node.Latitude, node.Longitude);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = node;
                }
            }

            return nearest;
        }

        public static (Dictionary<int, NodeHelper>, Dictionary<int, LineHelper> lineMap) BuildTree(
                                   List<Node> nodes,
                                   List<NodeConnection> connections,
                                   List<Kvar> errors)
        {
            var nodeMap = nodes.ToDictionary(n => n.Id, n => new NodeHelper
            {
                Node = n,
                HasError = errors.Any(e => e.Node == n.Id && e.StanjeKvara == Enumerations.StanjeKvara.Aktivan)
            });

            var lineMap = new Dictionary<int, LineHelper>();

            foreach (var conn in connections)
            {
                var lineHelper = new LineHelper { Connection = conn };
                lineMap[conn.Id] = lineHelper;

                var parentNode = nodeMap[conn.StartPinId];
                var childNode = nodeMap[conn.EndPinId];

                parentNode.OutgoingLines.Add(lineHelper);
                childNode.ParentLine = lineHelper;
            }

            return (nodeMap, lineMap);
        }

        public static void PropagateErrors(NodeHelper node, List<NodeHelper> allNodes)
        {
            if (node.HasError && node.ParentLine != null)
            {
                node.ParentLine.HasError = true;
            }

            // Step 2: Recurse into children first
            foreach (var line in node.OutgoingLines)
            {
                var childNode = allNodes.First(nw => nw.Node.Id == line.Connection.EndPinId);
                PropagateErrors(childNode, allNodes);
            }

            // Step 3: If all child lines have error → mark parent line
            if (node.OutgoingLines.Count > 0 &&
                node.OutgoingLines.All(l => l.HasError))
            {
                if (node.ParentLine != null)
                {
                    node.ParentLine.HasError = true;
                    foreach (var line in node.OutgoingLines)
                    {
                        line.HasError = false;
                    }
                }
            }
        }

        public static void FixLineAndDescendants(int lineId, Dictionary<int, LineHelper> lineMap,
                                                      Dictionary<int, NodeHelper> nodeMap,
                                                      CRUDContext _dbContext)
        {
            if (!lineMap.TryGetValue(lineId, out var line))
                return;

            // Fix this line
            line.Connection.HasError = false;
            _dbContext.NodeConnections.Update(line.Connection);

            var node = nodeMap[line.Connection.EndPinId];

            var endNode = nodeMap[line.Connection.EndPinId];
            var activeErrors = _dbContext.Kvarovi
                .Where(e => e.Node == endNode.Node.Id && e.StanjeKvara == StanjeKvara.Aktivan)
                .ToList();

            foreach (var err in activeErrors)
            {
                err.StanjeKvara = StanjeKvara.Popravljen;
                err.VremeOtkanjanja = DateTime.UtcNow;
                _dbContext.Kvarovi.Update(err);
            }

            // 3. Cascade downwards only if ALL outgoing lines from this node
            // lead to end-nodes that *still* have active errors
            if (endNode.OutgoingLines.Count > 0 &&
                endNode.OutgoingLines.All(l =>
                    _dbContext.Kvarovi.Any(e =>
                        e.Node == l.Connection.EndPinId && e.StanjeKvara == StanjeKvara.Aktivan)))
            {
                foreach (var childLine in endNode.OutgoingLines)
                {
                    FixLineAndDescendants(childLine.Connection.Id, lineMap, nodeMap, _dbContext);
                }
            }
        }



    }
}
