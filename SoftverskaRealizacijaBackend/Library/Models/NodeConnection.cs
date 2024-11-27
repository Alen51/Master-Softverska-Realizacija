using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class NodeConnection
    {
        public int Id { get; set; }
        public Node TopNode { get; set; }
        public List<Node> Nodes { get; set; }
}
