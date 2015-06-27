using System;
using System.Text;
using C5;


namespace CodeInterview {

	#region Node Class
	public class Node<Type> : IComparable where Type : IComparable {

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public Type Value { get; internal protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.Node`1"/> class.
		/// </summary>
		public Node(Type value) {
			this.Value = value;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="CodeInterview.Node`1"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="CodeInterview.Node`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="CodeInterview.Node`1"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj) {
			bool isEqual = false;
			Node<Type> node;
			if ((node = obj as Node<Type>) != null) {
				isEqual = this.Value.CompareTo(node.Value) == 0;
			}

			return isEqual;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="CodeInterview.Node`1"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="CodeInterview.Node`1"/>.</returns>
		public override string ToString() {
			return string.Format("[Node: Value={0}]", Value);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="CodeInterview.Node`1"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode() {
			return base.GetHashCode();
		}

		/// <param name="firstNode">First node.</param>
		/// <param name="otherNode">Other node.</param>
		public static bool operator <(Node<Type> firstNode, Node<Type> otherNode) {
			return firstNode.CompareTo(otherNode) < 0;
		}

		/// <param name="firstNode">First node.</param>
		/// <param name="otherNode">Other node.</param>
		public static bool operator >(Node<Type> firstNode, Node<Type> otherNode) {
			return firstNode.CompareTo(otherNode) > 0;
		}

		#region IComparable implementation

		/// <summary>
		/// Compares to.
		/// </summary>
		/// <returns>The to.</returns>
		/// <param name="obj">Object.</param>
		public int CompareTo(object obj) {
			Node<Type> node = obj as Node<Type>;
			if (node == null) {
				throw new ArgumentException(string.Format("Given object is not of type Node<{0}>", typeof(Type)));
			}

			return this.Value.CompareTo(node.Value);
		}

		#endregion
	}
	#endregion

	#region Binary Tree Node Class
	public class BinaryTreeNode<Type> : Node<Type> where Type : IComparable {

		/// <summary>
		/// Gets or sets the parent.
		/// </summary>
		/// <value>The parent.</value>
		public BinaryTreeNode<Type> Parent { get; internal protected set; }

		/// <summary>
		/// Gets or sets the right.
		/// </summary>
		/// <value>The right.</value>
		public BinaryTreeNode<Type> Right { get; internal protected set; }

		/// <summary>
		/// Gets or sets the left.
		/// </summary>
		/// <value>The left.</value>
		public BinaryTreeNode<Type> Left { get; internal protected set; }

		/// <summary>
		/// Gets a value indicating whether this instance has right child.
		/// </summary>
		/// <value><c>true</c> if this instance has right child; otherwise, <c>false</c>.</value>
		public bool HasRightChild { get { return this.Right != null; } }

		/// <summary>
		/// Gets a value indicating whether this instance has left child.
		/// </summary>
		/// <value><c>true</c> if this instance has left child; otherwise, <c>false</c>.</value>
		public bool HasLeftChild { get { return this.Left != null; } }

		/// <summary>
		/// Gets a value indicating whether this instance has parent.
		/// </summary>
		/// <value><c>true</c> if this instance has parent; otherwise, <c>false</c>.</value>
		public bool HasParent { get { return this.Parent != null; } }

		/// <summary>
		/// Gets the childs count.
		/// </summary>
		/// <value>The childs count.</value>
		public int ChildsCount { 
			get {
				int count = 0;
				count = (this.Right == null) ? count : count + 1;
				count = (this.Left == null) ? count : count + 1;

				return count;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is leaf.
		/// </summary>
		/// <value><c>true</c> if this instance is leaf; otherwise, <c>false</c>.</value>
		public bool IsLeaf { get { return this.ChildsCount == 0; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.BinaryTreeNode`1"/> class.
		/// </summary>
		/// <param name="value">Value.</param>
		public BinaryTreeNode(Type value) : this(value, null) {
		}

		public BinaryTreeNode(Type value, BinaryTreeNode<Type> parent) : base(value) {
			this.Right = null;
			this.Left = null;
			this.Parent = parent;
		}
	}
	#endregion

	#region Augmented Node Class
	public class AugmentedBinaryTreeNode<Type> : BinaryTreeNode<Type> where Type : IComparable {

		private int _Depth { get; set; }

		/// <summary>
		/// Gets the depth (distance from root).
		/// </summary>
		/// <value>The depth.</value>
		public int Depth {
			get {
				if (this._Depth == 0 && this.HasParent && this.Parent is AugmentedBinaryTreeNode<Type>) {
					this._Depth = ((AugmentedBinaryTreeNode<Type>)this.Parent).Depth + 1;
				}

				return this._Depth;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.AugmentedNode`1"/> class.
		/// </summary>
		/// <param name="value">Value.</param>
		public AugmentedBinaryTreeNode(Type value) : this(value, null) {
			_Depth = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.AugmentedBinaryTreeNode`1"/> class.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="parent">Parent.</param>
		public AugmentedBinaryTreeNode(Type value, AugmentedBinaryTreeNode<Type> parent) : base(value, parent) {
			this._Depth = parent.Depth + 1;
		}
	}
	#endregion

	#region Binary Search Tree Class
	public class BinarySearchTree<Type> where Type : IComparable {

		/// <summary>
		/// Gets the root.
		/// </summary>
		/// <value>The root.</value>
		public BinaryTreeNode<Type> Root { get; protected set; }

		/// <summary>
		/// Gets the size of the tree including the root.
		/// </summary>
		/// <value>The size.</value>
		public int Size { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// An empty binary tree is a tree with root only.
		/// </summary>
		/// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty { get { return this.Size == 1; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.BinaryTree`1"/> class.
		/// </summary>
		/// <param name="rootValue">Root value.</param>
		public BinarySearchTree(Type rootValue) {
			this.Root = new BinaryTreeNode<Type>(rootValue);
			this.Size = 1;
		}

		/// <summary>
		/// Find the specified node with the given value.
		/// </summary>
		/// <param name="value">Value.</param>
		public BinaryTreeNode<Type> find(Type value) {
			BinaryTreeNode<Type> currentNode = this.Root, searchedNode = null;
			while (currentNode != null) {
				if (currentNode.Value.CompareTo(value) == 0) {
					searchedNode = currentNode;
					break;
				} else if (currentNode.Value.CompareTo(value) > 0) {
					currentNode = currentNode.Left;
				} else {
					currentNode = currentNode.Right;
				}
			}

			return searchedNode;
		}

		/// <summary>
		/// Successor the specified node.
		/// </summary>
		/// <param name="node">Node.</param>
		public BinaryTreeNode<Type> successor(BinaryTreeNode<Type> node) {
			BinaryTreeNode<Type> succNode = node.Right;
			if (succNode == null) {
				BinaryTreeNode<Type> currentNode = node, parentNode = node.Parent;
				while (currentNode != parentNode.Left) {
					currentNode = parentNode;
					parentNode = currentNode.Parent;
				}

				succNode = parentNode;
			} else {
				BinaryTreeNode<Type> currentNode = succNode;
				while (currentNode.Left != null) {
					currentNode = currentNode.Left;
				}

				succNode = currentNode;
			}

			return succNode;
		}

		/// <summary>
		/// Predecessor the specified node.
		/// </summary>
		/// <param name="node">Node.</param>
		public BinaryTreeNode<Type> predecessor(BinaryTreeNode<Type> node) {
			BinaryTreeNode<Type> predNode = node.Left;
			if (predNode == null) {
				BinaryTreeNode<Type> currentNode = node, parentNode = node.Parent;
				while (currentNode != parentNode.Right) {
					currentNode = parentNode;
					parentNode = currentNode.Parent;
				}

				predNode = parentNode;
			} else {
				BinaryTreeNode<Type> currentNode = predNode;
				while (currentNode.Right != null) {
					currentNode = currentNode.Right;
				}

				predNode = currentNode;
			}

			return predNode;
		}

		/// <summary>
		/// Insert the specified node.
		/// </summary>
		/// <param name="node">Node.</param>
		public void insert(Type value) {
			BinaryTreeNode<Type> currentNode = this.Root, parentNode = null;
			while (currentNode != null) {
				parentNode = currentNode;
				if (value.CompareTo(currentNode.Value) < 0) {
					currentNode = currentNode.Left;
				} else {
					currentNode = currentNode.Right;
				}
			}

			if (value.CompareTo(parentNode.Value) < 0) {
				parentNode.Left = new BinaryTreeNode<Type>(value, parentNode);
			} else {
				parentNode.Right = new BinaryTreeNode<Type>(value, parentNode);
			}

			this.Size++;
		}

		/// <summary>
		/// Remove the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public Node<Type> remove(Type value) {
			BinaryTreeNode<Type> theNode = this.find(value);
			if (theNode != null) {
				BinaryTreeNode<Type> parentNode = theNode.Parent;
				switch (theNode.ChildsCount) {
				case 0:
					{
						theNode.Parent = null;
						if (parentNode.Right == theNode) {
							parentNode.Right = null;
						} else {
							parentNode.Left = null;
						}
						this.Size--;
						break;
					}
				case 1:
					{
						BinaryTreeNode<Type> theChild = (theNode.Right == null) ? theNode.Left : theNode.Right;
						theChild.Parent = parentNode;
						if (parentNode.Right == theNode) {
							parentNode.Right = theChild;
						} else {
							parentNode.Left = theChild;
						}
						this.Size--;
						break;
					}
				case 2:
					{
						Node<Type> succNode = this.successor(theNode);
						Type succValue = succNode.Value;
						this.remove(succNode.Value);
						theNode.Value = succValue;
						break;
					}
				}
			}

			return theNode;
		}
	}
	#endregion

	#region Trees Static Tools
	public class Tree<Type> where Type : IComparable {

		/// <summary>
		/// Traverses the tree in-order.
		/// </summary>
		/// <returns>The order.</returns>
		/// <param name="bst">Bst.</param>
		public static string inOrder(BinarySearchTree<Type> bst) {
			StringBuilder sb = new StringBuilder();
			Stack<BinaryTreeNode<Type>> stack = new Stack<BinaryTreeNode<Type>>(bst.Size);

			BinaryTreeNode<Type> currentNode = bst.Root;
			while (currentNode != null) {
				stack.push(currentNode);
				currentNode = currentNode.Left;
			}

			while (!stack.IsEmpty) {
				currentNode = stack.pop();
				sb.Append(currentNode.ToString() + "\n");

				if (currentNode.HasRightChild) {
					currentNode = currentNode.Right;
					while (currentNode != null) {
						stack.push(currentNode);
						currentNode = currentNode.Left;
					}
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Pres the order.
		/// </summary>
		/// <returns>The order.</returns>
		/// <param name="bst">Bst.</param>
		public static string preOrder(BinarySearchTree<Type> bst) {
			StringBuilder sb = new StringBuilder();

			Stack<BinaryTreeNode<Type>> stack = new Stack<BinaryTreeNode<Type>>(bst.Size);
			BinaryTreeNode<Type> currentNode = bst.Root;
			while (currentNode != null) {
				sb.Append(currentNode.ToString() + "\n");
				stack.push(currentNode);
				currentNode = currentNode.Left;
			}

			while (!stack.IsEmpty) {
				currentNode = stack.pop();
				if (currentNode.HasRightChild) {
					currentNode = currentNode.Right;
					while (currentNode != null) {
						sb.Append(currentNode.ToString() + "\n");
						stack.push(currentNode);
						currentNode = currentNode.Left;
					}
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Posts the order.
		/// </summary>
		/// <returns>The order.</returns>
		/// <param name="bst">Bst.</param>
		public static string postOrder(BinarySearchTree<Type> bst) {
			StringBuilder sb = new StringBuilder();

			Stack<BinaryTreeNode<Type>> stack = new Stack<BinaryTreeNode<Type>>(bst.Size);
			BinaryTreeNode<Type> currentNode = bst.Root;
			do {
				while (currentNode != null) {
					if (currentNode.HasRightChild) {
						stack.push(currentNode.Right);
					}
					stack.push(currentNode);
					currentNode = currentNode.Left;
				}
				
				currentNode = stack.pop();
				if (currentNode.HasRightChild && currentNode.Right.Equals(stack.peek())) {
					BinaryTreeNode<Type> temp = stack.pop();
					stack.push(currentNode);
					currentNode = temp;
				} else {
					sb.Append(currentNode.ToString() + "\n");
					currentNode = null;
				}
			} while (!stack.IsEmpty);

			return sb.ToString();
		}

		/* Under construction
		 * 
		/// <summary>
		/// Ises the balanced.
		/// </summary>
		/// <returns><c>true</c>, if balanced was ised, <c>false</c> otherwise.</returns>
		/// <param name="bst">Bst.</param>
		public static bool isBalanced(BinarySearchTree<CodeInterview.AugmentedBinaryTreeNode<Type>> bst) {
			bool isBalanced = true;
			Stack<AugmentedBinaryTreeNode<Type>> stack = new Stack<AugmentedBinaryTreeNode<Type>>(bst.Size);
			AugmentedBinaryTreeNode<Type> currentNode = bst.Root;
			int min = Int32.MaxValue, max = Int32.MinValue;
			while (currentNode != null) {
				stack.push(currentNode);
				currentNode = currentNode.Left;
			}

			while (!stack.IsEmpty) {
				currentNode = stack.pop();
				if (currentNode.IsLeaf) {
					min = Math.Min(min, currentNode.Depth);
					max = Math.Max(max, currentNode.Depth);
					if (max - min > 1) {
						isBalanced = false;
						break;
					}
				} else if (currentNode.HasRightChild) {
					currentNode = currentNode.Right;
					while (currentNode != null) {
						stack.push(currentNode);
						currentNode = currentNode.Left;
					}
				}
			}
		
			return isBalanced;
		}
		*/
	}
	#endregion

	#region Vertex Class (Graph Node)
	public class Vertex<Type> : Node<Type> where Type : IComparable {

		/// <summary>
		/// Gets or sets the siblings.
		/// </summary>
		/// <value>The siblings.</value>
		public ArrayList<Vertex<Type>> Siblings { get; protected set; }

		/// <summary>
		/// Gets the <see cref="CodeInterview.Vertex`1"/> with the specified idx.
		/// </summary>
		/// <param name="idx">Index.</param>
		public Vertex<Type> this[int idx] { get { return this.Siblings[idx]; } }

		/// <summary>
		/// Gets a value indicating whether this <see cref="CodeInterview.Vertex`1"/> siblings count.
		/// </summary>
		/// <value><c>true</c> if siblings count; otherwise, <c>false</c>.</value>
		public int SiblingsCount { get { return this.Siblings.Count; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.Vertex`1"/> class.
		/// </summary>
		/// <param name="value">Value.</param>
		public Vertex(Type value) : base(value) {
			this.Siblings = new ArrayList<Vertex<Type>>();
		}

		/// <summary>
		/// Hases the sibling.
		/// </summary>
		/// <returns><c>true</c>, if sibling was hased, <c>false</c> otherwise.</returns>
		/// <param name="sibling">Sibling.</param>
		public bool hasSibling(Vertex<Type> sibling) {
			return this.Siblings.Contains(sibling);
		}

		/// <summary>
		/// Adds the sibling.
		/// </summary>
		/// <returns><c>true</c>, if sibling was added, <c>false</c> otherwise.</returns>
		/// <param name="sibling">Sibling.</param>
		public bool addSibling(Vertex<Type> sibling) {
			return this.Siblings.Add(sibling);
		}

		/// <summary>
		/// Removes the sibling.
		/// </summary>
		/// <param name="sibling">Sibling.</param>
		public bool removeSibling(Vertex<Type> sibling) {
			return this.Siblings.Remove(sibling);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="CodeInterview.Vertex`1"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="CodeInterview.Vertex`1"/>.</returns>
		public override string ToString() {
			return string.Format("[Vertex] Value={0}, Siblings Count={1}", base.Value, this.SiblingsCount);
		}
	}
	#endregion

	#region Graph Class
	public class Graph<Type> where Type : IComparable {

		/// <summary>
		/// Gets or sets the vertices.
		/// </summary>
		/// <value>The vertices.</value>
		public ArrayList<Vertex<Type>> Vertices { get; private set; }

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count { get { return this.Vertices.Count; } }

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		/// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty { get { return this.Count == 0; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.Graph`1"/> class.
		/// </summary>
		public Graph() {
			this.Vertices = new ArrayList<Vertex<Type>>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.Graph`1"/> class.
		/// </summary>
		/// <param name="array">Array.</param>
		public Graph(params Vertex<Type>[] vertices) : this() {
			foreach (Vertex<Type> vertex in vertices) {
				this.add(vertex);
			}
		}

		/// <summary>
		/// Add the specified vertex.
		/// </summary>
		/// <param name="vertex">Vertex.</param>
		public bool add(Vertex<Type> vertex) {
			return this.Vertices.Add((vertex));
		}

		/// <summary>
		/// Remove the specified vertex.
		/// </summary>
		/// <param name="theVertex">The vertex.</param>
		public bool remove(Vertex<Type> vertex) {
			bool wasRemoved = false;
			foreach (Vertex<Type> someVertex in this.Vertices) {
				if (someVertex.hasSibling(vertex)) {
					someVertex.removeSibling(vertex);
				}
			}

			return wasRemoved;
		}
	}
	#endregion

	#region Graphs Static Tool
	public static class Graphs<Type> where Type : IComparable {
		public static string dfs(Vertex<Type> vertex) {
			StringBuilder sb = new StringBuilder();
			Stack<Vertex<Type>> markedVertices = new Stack<Vertex<Type>>();
			Stack<Vertex<Type>> workingStack = new Stack<Vertex<Type>>();

			workingStack.push(vertex);
			Vertex<Type> currentVertex;
			while (!workingStack.IsEmpty) {
				currentVertex = vertex;

			}

			while (!markedVertices.IsEmpty) {
				sb.Insert(0, string.Format("{0}\n", markedVertices.pop().ToString()));
			}

			return sb.ToString();
		}
	}
	#endregion
}
