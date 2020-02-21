using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Core.Queuer
{
	/// <summary>
	/// Extended list with an events handler which gets called every time the count of list items changes
	/// </summary>
	/// <typeparam name="T">
	/// The type of the elements
	/// </typeparam>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "I want this to have a suffix of List")]
	public class ExtendedList<T> : List<T>
	{
		/// <summary>
		/// Event handler for the extended list
		/// </summary>
		#region Event(s)
		public event EventHandler<ListEventArgs> CountChanged;
		#endregion

		#region Methods
		/// <summary>
		/// Adds an item
		/// </summary>
		public new void Add(T item)
		{
			base.Add(item);

			// Copy to a temporary variable to be thread-safe (MSDN).
			EventHandler<ListEventArgs> CountChangedArgs = CountChanged;
			if (CountChangedArgs != null)
				CountChangedArgs(this, new ExtendedList<T>.ListEventArgs(this.Count));
		}

		/// <summary>
		/// Adds a range
		/// </summary>
		public new void AddRange(IEnumerable<T> collection)
		{
			base.AddRange(collection);

			// Copy to a temporary variable to be thread-safe (MSDN).
			EventHandler<ListEventArgs> CountChangedArgs = CountChanged;
			if (CountChangedArgs != null)
				CountChangedArgs(this, new ExtendedList<T>.ListEventArgs(this.Count));
		}

		/// <summary>
		/// Clears the list.
		/// </summary>
		public new void Clear()
		{
			base.Clear();

			// Copy to a temporary variable to be thread-safe (MSDN).
			EventHandler<ListEventArgs> CountChangedArgs = CountChanged;
			if (CountChangedArgs != null)
				CountChangedArgs(this, new ExtendedList<T>.ListEventArgs(this.Count));
		}

		/// <summary>
		/// Removes the first matched item.
		/// </summary>
		public new void Remove(T item)
		{
			base.Remove(item);

			// Copy to a temporary variable to be thread-safe (MSDN).
			EventHandler<ListEventArgs> CountChangedArgs = CountChanged;
			if (CountChangedArgs != null)
				CountChangedArgs(this, new ExtendedList<T>.ListEventArgs(this.Count));
		}
		#endregion

		#region Classes
		/// <summary>
		/// An arguments class for the list event hander
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
		public class ListEventArgs : EventArgs
		{
			/// <summary>
			/// Number of elements in the list
			/// </summary>
			public int Count { get; set; }
			
			/// <summary>
			/// The list event handler list argument
			/// </summary>
			/// <param name="ListCount"></param>
			public ListEventArgs(int ListCount)
			{
				this.Count = ListCount;
			}
		}
		#endregion Classes
	}
}
