using System;
using System.Text;


namespace CodeInterview {

	#region Stack class
	class Stack<Type> where Type : class, IComparable {

		/// <summary>
		/// The enlarge factor.
		/// </summary>
		private const int EnlargeFactor = 2;

		/// <summary>
		/// The standart capacity.
		/// </summary>
		private const int StandartCapacity = 16;

		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count { get; protected set; }

		/// <summary>
		/// Gets or sets the capacity.
		/// </summary>
		/// <value>The capacity.</value>
		public int Capacity { get; protected set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CodeInterview.Stack`1"/> is empty.
		/// </summary>
		/// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty { get { return this.Count == 0; } }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CodeInterview.Stack`1"/> is full.
		/// </summary>
		/// <value><c>true</c> if is full; otherwise, <c>false</c>.</value>
		public bool IsFull { get { return this.Count == this.Capacity; } }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CodeInterview.Stack`1"/> may be auto enlarged.
		/// </summary>
		/// <value><c>true</c> if auto enlarge; otherwise, <c>false</c>.</value>
		public bool AutoEnlarge { get; set; }

		/// <summary>
		/// The data container.
		/// </summary>
		protected Type[] Container;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.Stack`1"/> class.
		/// </summary>
		public Stack() : this(StandartCapacity) {
			this.AutoEnlarge = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.Stack`1"/> class.
		/// </summary>
		/// <param name="capacity">Capacity.</param>
		public Stack(int capacity) {
			this.Count = 0;
			this.Capacity = capacity;
			this.Container = new Type[this.Capacity];
			this.AutoEnlarge = false;
		}

		/// <summary>
		/// Push the specified element.
		/// Returns true if element was pushed and false otherwise.
		/// </summary>
		/// <param name="element">Element.</param>
		public bool push(Type element) {
			bool elementPushed = false;

			// auto expand stack incase its full
			if (IsFull && AutoEnlarge) {
				this.setCapacity(this.Capacity * EnlargeFactor);
			}

			if (!this.IsFull) {
				this.Container[this.Count] = element;
				this.Count++;
				elementPushed = true;
			}

			return elementPushed;
		}

		/// <summary>
		/// Pop element.
		/// Returns null if stack is empty.
		/// </summary>
		public Type pop() {
			Type elementToPop = null;
			if (!this.IsEmpty) {
				this.Count--;
				elementToPop = this.Container[this.Count];
			}

			return elementToPop;
		}

		public Type peek() {
			Type topElement = null;
			if (!this.IsEmpty) {
				topElement = this.Container[this.Count - 1];
			}

			return topElement;
		}

		/// <summary>
		/// Sets the new capacity.
		/// </summary>
		/// <returns><c>true</c>, if capacity was set, <c>false</c> otherwise.</returns>
		/// <param name="capacity">Capacity.</param>
		public bool setCapacity(int capacity) {
			bool capacityChanged = false;
			if (this.Capacity < capacity) {
				Type[] newContainer = new Type[capacity];
				this.Container.CopyTo(newContainer, 0);
				this.Container = newContainer;
				this.Capacity = capacity;
				capacityChanged = true;
			}

			return capacityChanged;
		}

		/// <summary>
		/// Contains the specified element.
		/// </summary>
		/// <param name="element">Element.</param>
		public bool Contains(Type element) {
			bool exist = false;
			if (!this.IsEmpty) {
				for (int i = 0; i < this.Count; i++) {
					if (this.Container[i].CompareTo(element) == 0) {
						exist = true;
						break;
					}
				}
			}

			return exist;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="CodeInterview.Stack`1"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="CodeInterview.Stack`1"/>.</returns>
		public override string ToString() {
			StringBuilder sb = new StringBuilder("{ ");
			for (int i = 0; i < this.Count; i++) {
				sb.Append(this.Container[i].ToString() + " ");
			}
			sb.Append("}");

			return string.Format("[Stack: Usage: {0} out of {1} elements]\n{2}\n", this.Count, this.Capacity, sb.ToString());
		}
	}
	#endregion

	#region SetOfStacks class
	class SetOfStacks<T> where T : class, IComparable {

		private const int EnlargeFactor = 2;

		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count { get; protected set; }

		/// <summary>
		/// Gets or sets the capacity of a single stack.
		/// </summary>
		/// <value>The capacity.</value>
		public int StackCapacity { get; protected set; }

		/// <summary>
		/// Gets the index of the current stack.
		/// </summary>
		/// <value>The index of the stack.</value>
		private int StackIdx { get { return Count / StackCapacity; } }

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		/// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty { get { return this.Count == 0; } }

		/// <summary>
		/// The init cotainers count.
		/// </summary>
		private const int initContainersSize = 10;

		/// <summary>
		/// The containers.
		/// </summary>
		private Stack<T>[] Containers;

		private Stack<T> CurrentStack { 
			get {
				if (StackIdx >= this.Containers.Length) {
					this.enlargeContainers();
				}

				if (this.Containers[StackIdx] == null) {
					this.Containers[StackIdx] = new Stack<T>(StackCapacity);
				}

				return this.Containers[StackIdx];
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeInterview.SetOfStacks`1"/> class.
		/// </summary>
		/// <param name="singleStackCapacity">Single stack capacity.</param>
		public SetOfStacks(int stackCapacity) {
			this.Count = 0;
			this.StackCapacity = stackCapacity;
			this.Containers = new Stack<T>[initContainersSize];
			this.Containers[0] = new Stack<T>(this.StackCapacity);
		}

		/// <summary>
		/// Push the specified element.
		/// </summary>
		/// <param name="element">Element.</param>
		public void push(T element) {
			this.Count++;
			CurrentStack.push(element);
		}

		/// <summary>
		/// Pop this instance.
		/// </summary>
		public T pop() {
			T element = CurrentStack.pop();
			this.Count--;

			return element;
		}

		/// <summary>
		/// Enlarge the containers.
		/// </summary>
		private void enlargeContainers() {
			Stack<T>[] newContainers = new Stack<T>[initContainersSize * EnlargeFactor];
			this.Containers.CopyTo(newContainers, 0);
			this.Containers = newContainers;
		}
	}
	#endregion
}

