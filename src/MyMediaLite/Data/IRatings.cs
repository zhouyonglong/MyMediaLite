// Copyright (C) 2011 Zeno Gantner
//
// This file is part of MyMediaLite.
//
// MyMediaLite is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// MyMediaLite is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with MyMediaLite.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;

namespace MyMediaLite.Data
{
	/// <summary>Interface for rating datasets</summary>
	public interface IRatings : IList<double>, IDataSet
	{
		/// <summary>the maximum rating in the dataset</summary>
		double MaxRating { get; }
		/// <summary>the minimum rating in the dataset</summary>
		double MinRating { get; }
		
		/// <summary>indices by user</summary>
		/// <remarks>Should be implemented as a lazy data structure</remarks>
		IList<IList<int>> ByUser { get; }
		/// <summary>indices by item</summary>
		/// <remarks>Should be implemented as a lazy data structure</remarks>
		IList<IList<int>> ByItem { get; }
		/// <summary>get a randomly ordered list of all indices</summary>
		/// <remarks>Should be implemented as a lazy data structure</remarks>
		IList<int> RandomIndex { get; }

		/// <summary>rating count by user</summary>
		/// <remarks>Should be implemented as a lazy data structure</remarks>
		IList<int> CountByUser { get; }
		/// <summary>rating count by item</summary>
		/// <remarks>Should be implemented as a lazy data structure</remarks>
		IList<int> CountByItem { get; }		
		
		/// <summary>Build the user indices</summary>
		void BuildUserIndices();
		/// <summary>Build the item indices</summary>
		void BuildItemIndices();
		/// <summary>Build the random index</summary>
		void BuildRandomIndex();

		/// <summary>average rating in the dataset</summary>
		double Average { get; }

		/// <summary>Directly access rating by user and item</summary>
		/// <param name="user_id">the user ID</param>
		/// <param name="item_id">the item ID</param>
		double this[int user_id, int item_id] { get; }

		/// <summary>Directly access rating by user and item</summary>
		/// <param name="user_id">the user ID</param>
		/// <param name="item_id">the item ID</param>
		/// <returns>the first found rating of the given item by the given user</returns>
		double Get(int user_id, int item_id);

		/// <summary>Try to retrieve a rating for a given user-item combination</summary>
		/// <param name="user_id">the user ID</param>
		/// <param name="item_id">the item ID</param>
		/// <param name="rating">will contain the first rating encountered that matches the user ID and item ID</param>
		/// <returns>true if a rating was found for the user and item</returns>
		bool TryGet(int user_id, int item_id, out double rating);

		/// <summary>Try to retrieve a rating for a given user-item combination</summary>
		/// <param name="user_id">the user ID</param>
		/// <param name="item_id">the item ID</param>
		/// <param name="indexes">the indexes to look at</param>
		/// <param name="rating">will contain the first rating encountered that matches the user ID and item ID</param>
		/// <returns>true if a rating was found for the user and item</returns>
		bool TryGet(int user_id, int item_id, ICollection<int> indexes, out double rating);

		/// <summary>Directly access rating by user and item</summary>
		/// <param name="user_id">the user ID</param>
		/// <param name="item_id">the item ID</param>
		/// <param name="indexes">the indexes to look at</param>
		/// <returns>the first rating encountered that matches the user ID and item ID</returns>
		double Get(int user_id, int item_id, ICollection<int> indexes);

		/// <summary>Add byte-valued rating to the collection</summary>
		/// <param name="user_id">the user ID</param>
		/// <param name="item_id">the item ID</param>
		/// <param name="rating">the rating</param>
		void Add(int user_id, int item_id, byte rating);

		/// <summary>Add float-valued rating to the collection</summary>
		/// <param name="user_id">the user ID</param>
		/// <param name="item_id">the item ID</param>
		/// <param name="rating">the rating</param>
		void Add(int user_id, int item_id, float rating);

		/// <summary>Add a new rating</summary>
		/// <param name="user_id">the user ID</param>
		/// <param name="item_id">the item ID</param>
		/// <param name="rating">the rating value</param>
		void Add(int user_id, int item_id, double rating);
	}
}

