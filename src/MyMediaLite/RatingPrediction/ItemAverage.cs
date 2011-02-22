// Copyright (C) 2010 Zeno Gantner, Steffen Rendle
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
//  You should have received a copy of the GNU General Public License
//  along with MyMediaLite.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using MyMediaLite.Data;

namespace MyMediaLite.RatingPrediction
{
	/// <summary>Uses the average rating value of an item for prediction</summary>
	/// <remarks>
	/// This engine does NOT support online updates.
	/// </remarks>
	public class ItemAverage : EntityAverage
	{
		/// <inheritdoc/>
		public override void Train()
		{
			var rating_counts = new List<int>();

			foreach (RatingEvent r in Ratings.All)
			{
				if (rating_counts.Count <= r.item_id)
				{
					rating_counts.Insert(r.item_id, 0);
					entity_averages.Insert(r.item_id, 0);
				}
				rating_counts[r.item_id]++;
				entity_averages[r.item_id] += r.rating;
				global_average += r.rating;
			}

			global_average /= Ratings.All.Count;

			for (int i = 0; i < entity_averages.Count; i++)
				if (rating_counts[i] != 0)
					entity_averages[i] /= rating_counts[i];
				else
					entity_averages[i] = global_average;
		}

		/// <inheritdoc/>
		public override bool CanPredict(int user_id, int item_id)
		{
			return (item_id <= MaxItemID);
		}

		/// <inheritdoc/>
		public override double Predict(int user_id, int item_id)
		{
			if (item_id <= MaxItemID)
				return entity_averages[item_id];
			else
				return global_average;
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return "ItemAverage";
		}
	}
}